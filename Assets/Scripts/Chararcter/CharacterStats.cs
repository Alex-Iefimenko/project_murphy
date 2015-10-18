//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//
//public class CharacterStats : MonoBehaviour {
//
//	//Basic room
//	[HideInInspector] public Room basicRoom;
//
//	//IsHostlie
//	public bool isHostile = false;
//
//	//IsInsane
//	public bool isInsane = false;
//
//	//Traits
//	public enum Traits{Hungry, Pessimist, IronWill, Puny, Big, SlowRecovery, Depresive, Bully, Dweeb, KungFu, Muff, Lazy, 
//		HardWorker, Slowpoke, Fast, Psychopath, Proffi, Masochist };
//	public Traits traitOne;
//	public Traits traitTwo;
//	
//	// NPC max stats
//	[HideInInspector] public float maxHealth;
//	[HideInInspector] public float maxFatigue;
//	[HideInInspector] public float maxSanity;
//	
//	// NPC stats
//	public float health;
//	public float fatigue;
//	public float sanity;
//	
//	// Stats trasholds
//	[HideInInspector] public float healthTreshold;
//	[HideInInspector] public float fatigueTreshold;
//	[HideInInspector] public float sanityTreshold;
//	
//	// Stats reduction
//	[HideInInspector] public float healthReduction = 0f;
//	[HideInInspector] public float fatigueReduction = 0.5f;
//	[HideInInspector] public float sanityReduction = 0.5f;
//
//	// Stats fullfilment
//	[HideInInspector] public float healthIncrease = 5f;
//	[HideInInspector] public float fatigueIncrease = 5f;
//	[HideInInspector] public float sanityIncrease = 5f;
//
//	// Rest probability
//	public float restProbalility = 0.25f;
//
//	// Attack related
//	public float damage = 5f;
//	public float attackRate = 1f;
//	[HideInInspector] public float attackCoolDown = 0f;
//	public bool  ableDistantAttack = false;
//
//	// HealCapability
//	public float healOther = 2f;
//
//	// Repair capability
//	public float repairAmount = 1f;
//
//	//Fire Extinguish capability
//	public float fireExtinguish = 1f;
//
//	// Components
//	private CharacterTasks npc;
//	private CharacterAI aiHandler;
//	private ActionNotificator notificator;
//	private CharacterTasks.Tasks[] npcTypeTasks;
//
//	// Update method
//	void Update () {
//		if (attackCoolDown >= 0f) attackCoolDown -= Time.deltaTime;
//	}
//
//
//	// Constructor
//	public void GenerateCharacterStats (CharacterTasks currnetNPC)
//	{
//		npc = currnetNPC;
//		aiHandler = npc.npcAI;
//		notificator = npc.notificator;
//
//		// Traits generation
//		ArrayList traitsValues = new ArrayList(System.Enum.GetValues (typeof(Traits)));
//		traitOne = (Traits)Helpers.GetRandomArrayValue<int>(traitsValues);
//		traitsValues.Remove (traitOne);
//		traitTwo = (Traits)Helpers.GetRandomArrayValue<int>(traitsValues);
//
//		healthTreshold = Random.Range (10, 25);
//		fatigueTreshold = Random.Range (10, 25);
//		sanityTreshold = Random.Range (10, 25);
//
//		maxHealth  = Random.Range (60, 80);
//		maxFatigue = Random.Range (60, 80);
//		maxSanity  = Random.Range (60, 80);
//
//		ApplyTraits ();
//
//		health  = maxHealth; 
//		fatigue = maxFatigue;
//		sanity  = maxSanity;
//		
//		GetBasicRoom (npc);
//		npcTypeTasks = (CharacterTasks.Tasks[])aiHandler.GetNPCTypeTasks (npc).ToArray (typeof(CharacterTasks.Tasks));
//
//	}
//
//	// Getbasic room
//	public void GetBasicRoom (CharacterTasks npc)
//	{
//		Dictionary<CharacterTasks.NPCTypes, Room.RoomTypes> professionRelatedRoom;
//		professionRelatedRoom = new Dictionary<CharacterTasks.NPCTypes, Room.RoomTypes> ()
//		{
//			{ CharacterTasks.NPCTypes.Engineer     , Room.RoomTypes.Engineering },
//			{ CharacterTasks.NPCTypes.Doctor       , Room.RoomTypes.MedBay },
//			{ CharacterTasks.NPCTypes.Safety       , Room.RoomTypes.Safety },
//			{ CharacterTasks.NPCTypes.Scientist    , Room.RoomTypes.Science },
//		};
//		basicRoom = ShipState.allRooms[(int)professionRelatedRoom[npc.npcType]].GetComponent<Room>();
//	}
//
//	// Applies traits inuence on NPC
//	void ApplyTraits ()
//	{
//		// Create object with stats related traits
//		string[] statsRelatedTraits = new string[15] 
//		{"fatigueReduction", "maxSanity", "maxSanity", "maxHealth", "maxHealth", "healthIncrease", "fatigueIncrease", "damage", "damage", 
//			"attackRate", "attackRate", "restProbalility", "restProbalility", "speed", "speed"};
//		float[] statsInfluenceOfTraits = new float[15]
//		{2f, 0.9f, 1.1f, 0.9f, 1.1f, 0.5f, 0.5f, 1.5f, 0.5f, 0.7f, 1.3f, 0.6f, 1.4f, 0.5f, 1.5f};
//
//		int[] statsRelated = GetStatsRelatedTraits (statsRelatedTraits);
//
//		if (statsRelated.Length > 0) 
//		{
//			foreach (int trait in statsRelated)
//			{
//				object context = (statsRelatedTraits[trait] == "speed") ? (object)this.npc.movement : this;
//				FieldInfo field = context.GetType ().GetField (statsRelatedTraits[trait]);
//				float currentValue = (float)field.GetValue (context);
//				field.SetValue(context, currentValue  * statsInfluenceOfTraits[trait]);
//			}
//		}
//		// NPCType related stats
//		if (HaveTrait (CharacterStats.Traits.HardWorker)) 
//		{
//			switch (npc.npcType)
//			{
//				case(CharacterTasks.NPCTypes.Engineer):
//					repairAmount *= 1.5f;
//					break;
//				case(CharacterTasks.NPCTypes.Doctor):
//					healOther *= 1.5f;
//					break;
//				case(CharacterTasks.NPCTypes.Safety):
//					fireExtinguish *= 1.5f;
//					break;
//			}
//		} 
//	}        
//
//	// Helper method for stats related traits detection
//	int[] GetStatsRelatedTraits (string[] stRelTraits)
//	{
//		ArrayList traitsValues = new ArrayList();
//		int trait1 = (int)traitOne;
//		int trait2 = (int)traitTwo;
//
//		if (stRelTraits.Length > trait1) traitsValues.Add(trait1);
//		if (stRelTraits.Length > trait2) traitsValues.Add(trait2);
//
//		return (int[]) traitsValues.ToArray (typeof(int));
//	}
//
//	public bool HaveTrait (CharacterStats.Traits trait)
//	{
//		bool result = false;
//		if (traitOne == trait || traitTwo == trait) result = true;
//		return result;
//	}
//
//	// IsResponsibleFor method
//	public bool IsResponsibleFor (Room room)
//	{
//		bool result = false;
//		foreach (CharacterTasks.Tasks respTask in npcTypeTasks) 
//		{
//			if (aiHandler.conditionMethods.ContainsKey(respTask) && aiHandler.conditionMethods[respTask].Invoke(npc, room))
//				result = true;
//		}
//		return result;
//	}
//
//	// Updates stats reduction
//	public void UpdateStats ()
//	{
//		if (healthReduction <= 0f && fatigue <= 0f) healthReduction = 1f;
//		else if (healthReduction != 0f) healthReduction = 0f;
//
//		health  -= healthReduction;
//		fatigue -= fatigueReduction;
//		sanity  -= sanityReduction;
//
//		// Check State change
//		CheckConscious ();
//
//		// Psychopath Trait
//		if (HaveTrait (CharacterStats.Traits.Psychopath) && !isInsane && sanity <= 0) 
//		{
//			isInsane = true;
//			isHostile = !isHostile;
//		}
//
//		Mathf.Clamp (health, 0f, maxHealth);
//		Mathf.Clamp (fatigue, 0f, maxFatigue);
//		Mathf.Clamp (sanity, 0f, maxSanity);
//	}
//
//	void CheckConscious ()
//	{
//
//
//		if (health <= healthTreshold && npc.currentState != CharacterTasks.States.Unconscious && npc.currentState != CharacterTasks.States.Dead)
//		{
//			npc.ClearTaskAims();
//			npc.currentState = CharacterTasks.States.Unconscious;
//			notificator.Notify(CharacterTasks.States.Unconscious.ToString());
//		}
//		else if (health <= 0f && npc.currentState != CharacterTasks.States.Dead) 
//		{
//			npc.ClearTaskAims();
//			npc.currentState = CharacterTasks.States.Dead;
//			notificator.Notify(CharacterTasks.States.Dead.ToString());
//		}
//		else if ((npc.currentState == CharacterTasks.States.Dead || npc.currentState == CharacterTasks.States.Unconscious) && health > healthTreshold)
//			npc.currentState = CharacterTasks.States.Idle;
//	}
//
//	// Damage
//	public void Damage (float amount) 
//	{
//		health -= amount;
//		if (HaveTrait (CharacterStats.Traits.Masochist)) sanity += 2f;
//	}
//}
