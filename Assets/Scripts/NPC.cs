using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

	//Profession
	public enum NPCTypes{Engineer, Safety, Doctor, Scientist};
	public NPCTypes npcType;

	//Current action
	public enum States{ Idle, Walk, Repair, Extinguish, Iteract, Fight, Shot, Sleep, Pull, Unconscious, Dead };
	[HideInInspector] public States currentState = States.Idle;

	//Current task
	public enum Tasks{ Idle, Navigate, Repair, Extinguish, Attack, Heal, TakeBody, Eat, Sleep, HealOther, EliminateDeadBody, Work, Rest }
	[HideInInspector] public Tasks currentTask = Tasks.Idle;

	// Hash with Action methods
	public Dictionary<NPC.Tasks, System.Action> actionMethods;

	//Task adress
	private Room taskAdress;
	// Task NPC
	private NPC taskNPC;
	// Task Length
	private float taskLength;

	//							//
	//  NPC Related components	//
	//							//
	//AILogic handler
	[HideInInspector] public AIHandler npcAI;
	// Movement Component
	[HideInInspector] public Movement movement;
	// Stats components
	[HideInInspector] public CharacterStats stats;

	// Use this for initialization
	void Awake () {
		// Generate stats
		stats = this.GetComponent<CharacterStats> ();
		npcAI = new AIHandler(this);
		movement = this.GetComponent<Movement> ();
		CreateActionDictionary ();
	}

	// Creation of Hash with Action methods
	void CreateActionDictionary () {
		actionMethods = new Dictionary<NPC.Tasks, System.Action> ()
		{
			{ Tasks.Attack            , Attack },
			{ Tasks.Heal              , Heal },
			{ Tasks.Repair            , Repair },
			{ Tasks.Extinguish        , Extinguish }, 
			{ Tasks.TakeBody          , TakeBody },
			{ Tasks.Eat               , Eat },
			{ Tasks.Sleep             , Sleep },
			{ Tasks.HealOther         , HealOther },
			{ Tasks.EliminateDeadBody , Eliminate },
			{ Tasks.Work              , Work },
			{ Tasks.Rest              , Rest },
			{ Tasks.Navigate          , Navigate }
		}; 
	}

	// Part of initialization. Generates random Character stats
	public void InitGenerateCharacterStats ()
	{
		stats.GenerateCharacterStats (this);
	}

	// Tick method. Ingame timeEvent for recalculating NPC tasks: period 0.5f s
	public void Tick () {
		// Recalculate stats
		stats.UpdateStats ();
		// Next action chose
		PerformRelevantAction ();
	}

	// Decides what next action would be taken and calls it
	void PerformRelevantAction ()
	{
		Tasks nextAction = npcAI.GetCurrentTask (this);
		// Check if lowpriority tasks were complete to end
		if ( ( taskLength <= 0f || (nextAction != Tasks.Work && nextAction != Tasks.Rest) ) )
			currentTask = nextAction;
		//print (currentTask);
		actionMethods[currentTask].Invoke();
		ChangeState ();
	}

	// Change state for relevant action
	void ChangeState () 
	{
		// Create task-to-state transition matrix
		Dictionary<NPC.Tasks, NPC.States> stateTaskTransition = new Dictionary<NPC.Tasks, NPC.States> ()
		{
			{ Tasks.Heal              , States.Iteract },
			{ Tasks.Repair            , States.Repair },
			{ Tasks.Extinguish        , States.Extinguish }, 
			{ Tasks.Eat               , States.Iteract },
			{ Tasks.Sleep             , States.Sleep },
			{ Tasks.HealOther         , States.Iteract },
			{ Tasks.Work              , States.Iteract },
			{ Tasks.Rest              , States.Iteract }
		}; 
		// Change state
		if (movement.IsStaying () && stateTaskTransition.ContainsKey(currentTask)) 
		{
			if (currentState != stateTaskTransition[currentTask]) currentState = stateTaskTransition[currentTask];
		}
	}

	// Clears Task's related fields
	void ClearTaskAims ()
	{
		taskAdress = null;
		taskNPC = null;
		currentTask = Tasks.Idle;
		currentState = States.Idle;
		movement.targetRoomObject = null;
		movement.targetRoom = null;
		taskLength = 0f;
	}

	// Updates taskNPC if it was setted as new
	public void SetTaskNPC (NPC npc)
	{
		if (taskNPC != npc)	taskNPC = npc;
	}

	// CreatesNew movement Path to room or object
	public void SetTaskAdress(Room room, bool navigateToRoomObject)
	{
		if (taskAdress != room) taskAdress = room;
		if (movement.currentRoom != taskAdress || movement.targetRoom != taskAdress) 
			movement.NewMovementPath(taskAdress, navigateToRoomObject);
	}

	// Adds object point added to movement path
	public void SetTaskAdress(GameObject tObject)
	{
		if (movement.targetRoomObject != tObject) movement.NewMovementPath(tObject);
	}

	// Fight action
	void Attack () 
	{
		SetTaskNPC(movement.currentRoom.ContainsHostile(!stats.isHostile, false));
		if (!stats.ableDistantAttack) SetTaskAdress(taskNPC.gameObject);
	}

	// Range combat
	void Shot () 
	{
		if (stats.attackCoolDown <= 0f) 
		{
			stats.attackCoolDown = stats.attackRate;
			currentState = States.Shot;
			taskNPC.Damage (stats.damage);
		}
	}

	// Close combat
	void Fight () 
	{
		if (stats.attackCoolDown <= 0f || movement.IsNearObject(taskNPC.gameObject))  
		{
			stats.attackCoolDown = stats.attackRate;
			currentState = States.Fight;
			taskNPC.Damage (stats.damage);
		}
	}

	// Heal action
	void Heal () 
	{
		Room medbay = ShipState.allRooms [(int)Room.RoomTypes.MedBay].GetComponent<Room> ();
		SetTaskAdress(medbay, true);
		if (movement.currentRoom == medbay) stats.health += stats.healthIncrease;
		if (stats.health >= stats.maxHealth) ClearTaskAims ();
	}

	// Repair action
	void Repair () 
	{
		// Navigate to RoomObject
		SetTaskAdress(movement.currentRoom, true);
		movement.currentRoom.currentDurability += stats.repairAmount;
		if (taskAdress.currentDurability >= taskAdress.maxDurability) ClearTaskAims ();
	}

	// Fight action
	void Extinguish () 
	{
		// Navigate to RoomObject
		//
		movement.currentRoom.roomStatus["fire"] -= stats.fireExtinguish;
		if (taskAdress.roomStatus["fire"] <= 0f) ClearTaskAims ();
	}

	// TakeBody action
	void TakeBody () 
	{
		SetTaskNPC(movement.currentRoom.ContainsUnconscious());
		if (movement.IsNearObject (taskNPC.gameObject)) 
		{
			SetTaskAdress(ShipState.allRooms[(int)Room.RoomTypes.MedBay]);
			taskNPC.SetTaskAdress(ShipState.allRooms[(int)Room.RoomTypes.MedBay]);
			currentState = States.Pull;
		}
		else 
		{
			SetTaskAdress (taskNPC.gameObject);
		}
		if (taskNPC.movement.currentRoom == ShipState.allRooms[(int)Room.RoomTypes.MedBay]) ClearTaskAims ();
	}

	// Eat action
	void Eat () 
	{
		Room dinnery = ShipState.allRooms[(int)Room.RoomTypes.Dinnery].GetComponent<Room> ();
		SetTaskAdress(dinnery, true);
		if (movement.currentRoom == dinnery) stats.fatigue += stats.fatigueIncrease;
		if (stats.fatigue >= stats.maxFatigue) ClearTaskAims ();
	}

	// Sleep action
	void Sleep () 
	{
		Room quarters = ShipState.allRooms [(int)Room.RoomTypes.LivingQuarters].GetComponent<Room> ();
		SetTaskAdress(quarters, true);
		if (movement.currentRoom == taskAdress) stats.sanity += stats.sanityIncrease;
		if (stats.sanity >= stats.maxSanity) ClearTaskAims ();
	}

	// HealOther action
	void HealOther () 
	{
		SetTaskNPC(movement.currentRoom.ContainsWounded());
		if (movement.IsNearObject (taskNPC.gameObject))
			taskNPC.stats.health += stats.healOther;
		else
			SetTaskAdress (taskNPC.gameObject);
		if (taskNPC.stats.health >= taskNPC.stats.maxHealth) ClearTaskAims ();
	}

	// Eliminate action
	void Eliminate () 
	{
		SetTaskNPC(movement.currentRoom.ContainsDead());
		if (movement.IsNearObject (taskNPC.gameObject)) 
		{
			SetTaskAdress(ShipState.allRooms[(int)Room.RoomTypes.Disposal]);
			taskNPC.SetTaskAdress(ShipState.allRooms[(int)Room.RoomTypes.Disposal]);
			currentState = States.Pull;
		}
		else
		{
			SetTaskAdress(taskNPC.gameObject);
		}
		if (taskNPC.movement.currentRoom == ShipState.allRooms[(int)Room.RoomTypes.Disposal]) ClearTaskAims ();
	}

	// Work Action
	void Work ()
	{
		if (taskLength <= 0f) taskLength = 7f;
		if (movement.currentRoom == stats.basicRoom) 
		{
			CheckResponsobility ();
			taskLength -= 0.5f;
		}
		else
		{
			SetTaskAdress(stats.basicRoom, true);
		}
		if (taskLength == 0f) ClearTaskAims ();
	}

	// Work helper method
	void CheckResponsobility ()
	{
		foreach (GameObject room in ShipState.allRooms.Values)
		{
			if (stats.IsResponsibleFor(room.GetComponent<Room>())) 
			{
				ClearTaskAims ();
				taskAdress = room.GetComponent<Room>();
				Navigate ();
			}
		}
	}
		
	// Navigate to room action
	void Navigate ()
	{
		if (currentTask != Tasks.Navigate) currentTask = Tasks.Navigate;
		SetTaskAdress (taskAdress, false);
		if (movement.IsStaying () && movement.currentRoom == taskAdress) ClearTaskAims ();
	}

	// Rest Action
	void Rest ()
	{
		if (taskLength <= 0f) taskLength = 5f;
		Room quarters = ShipState.allRooms [(int)Room.RoomTypes.LivingQuarters].GetComponent<Room> ();
		SetTaskAdress(quarters, true);
		if (movement.IsStaying()) taskLength -= 0.5f;
		if (taskLength == 0f) ClearTaskAims ();
	}

	// Update is called once per frame
	void Update () {
		ApplyAction ();
	}
	
	// Way to decide what Action take
	void ApplyAction ()
	{
		switch (currentTask)
		{
		case Tasks.Attack:
			if (stats.ableDistantAttack) Shot ();
			if (!stats.ableDistantAttack) Fight ();
			if (taskNPC.currentState == NPC.States.Unconscious || taskNPC.currentState == NPC.States.Dead) 	ClearTaskAims ();
			break;
		}
	}
	
	// Damage
	void Damage (float amount) 
	{
		stats.health -= amount;
		if (stats.HaveTrait (CharacterStats.Traits.Masochist)) stats.sanity += 2f;
		if (stats.health <= stats.healthTreshold) currentState = States.Unconscious;
		if (stats.health <= 0f) currentState = States.Dead;
	}

}
