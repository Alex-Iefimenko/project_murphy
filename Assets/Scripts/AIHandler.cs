using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIHandler {

	// NPC Task priority list
	public NPC.Tasks[] taskPriority = new NPC.Tasks[0];

	// Basic tasks for NPC
	public ArrayList criticalTasks  = new ArrayList () {NPC.Tasks.Heal, NPC.Tasks.Attack, NPC.Tasks.Navigate};
	public ArrayList importantTasks = new ArrayList () {NPC.Tasks.Repair, NPC.Tasks.Extinguish, NPC.Tasks.TakeBody};
	public ArrayList statsTasks     = new ArrayList () {NPC.Tasks.Eat, NPC.Tasks.Sleep};
	public NPC.Tasks[] usualTasks     = new NPC.Tasks[2] {NPC.Tasks.Work, NPC.Tasks.Rest};

	// NPC Type tasks
	public ArrayList engineerTasks  = new ArrayList () {NPC.Tasks.Repair};
	public ArrayList doctorTasks    = new ArrayList () {NPC.Tasks.HealOther, NPC.Tasks.EliminateDeadBody};
	public ArrayList safetyTasks    = new ArrayList () {NPC.Tasks.Attack, NPC.Tasks.Extinguish};
	public ArrayList scientistTasks = new ArrayList () {};

	// Hash with Condition methods
	public Dictionary<NPC.Tasks, System.Func<NPC, Room, bool>> conditionMethods;
	
	public ArrayList GetNPCTypeTasks (NPC npc)
	{
		string typeTasks = System.Enum.GetName (typeof(NPC.NPCTypes), npc.npcType).ToLower() + "Tasks";
		ArrayList npcTypeTasks = this.GetType ().GetField (typeTasks).GetValue (this) as ArrayList;
		return npcTypeTasks;
	}

	// Generate AI Handler object. Creates list with all priorities
	public AIHandler (NPC npc)
	{
		ArrayList npcTypeTasks = GetNPCTypeTasks (npc);

		foreach (NPC.Tasks action in npcTypeTasks) 
			if (importantTasks.Contains(action)) importantTasks.Remove(action);

		ArrayList taskPriorityArray = new ArrayList();

		taskPriorityArray.AddRange (criticalTasks);
		taskPriorityArray.AddRange (npcTypeTasks);
		taskPriorityArray.AddRange (importantTasks);
		taskPriorityArray.AddRange (statsTasks);

		taskPriority =  (NPC.Tasks[]) taskPriorityArray.ToArray (typeof(NPC.Tasks));

		CreateConditionDictionary ();
	}

	// Create Dictionary with Delegates for Condition checks
	public void CreateConditionDictionary () 
	{
		conditionMethods = new Dictionary<NPC.Tasks, System.Func<NPC, Room, bool>> ()
		{
			{ NPC.Tasks.Attack            , AttackCondition },
			{ NPC.Tasks.Heal              , HealCondition },
			{ NPC.Tasks.Repair            , RepairCondition },
			{ NPC.Tasks.Extinguish        , ExtinguishCondition }, 
			{ NPC.Tasks.TakeBody          , TakeBodyCondition },
			{ NPC.Tasks.Eat               , EatCondition },
			{ NPC.Tasks.Sleep             , SleepCondition },
			{ NPC.Tasks.HealOther         , HealOtherCondition },
			{ NPC.Tasks.EliminateDeadBody , EliminateDeadBodyCondition },
			{ NPC.Tasks.Navigate          , NavigateCondition }
		}; 
	}

	// Iterate over task conditions and returns current task
	public NPC.Tasks GetCurrentTask (NPC npc) {
		Room currentRoom = npc.movement.currentRoom;
		foreach (NPC.Tasks action in taskPriority) 
		{
			if (conditionMethods.ContainsKey(action) && conditionMethods[action].Invoke(npc, currentRoom)) 
				return action;
		}
		return (Random.value > npc.stats.restProbalility) ? usualTasks[0] : usualTasks[1];
	}

	// Fight conditions
	public bool AttackCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.ContainsHostile(!npc.stats.isHostile, false)) result = true;
		return result;
	}

	// Heal conditions
	public bool HealCondition (NPC npc, Room room)
	{
		bool result = false;
		if (npc.stats.health < npc.stats.healthTreshold) result = true;
		return result;
	}

	// Repair conditions
	public bool RepairCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.currentDurability < room.maxDurability) result = true;
		return result;
	}

	// Fire Extinguish conditions
	public bool ExtinguishCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.roomStatus["fire"] > 0f) result = true;
		return result;
	}

	// Take body away condition
	public bool TakeBodyCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.ContainsUnconscious()) result = true;
		return result;
	}

	// Go to eat condition
	public bool EatCondition (NPC npc, Room room)
	{
		bool result = false;
		if (npc.stats.fatigue < npc.stats.fatigueTreshold || npc.currentTask == NPC.Tasks.Eat) result = true;
		return result;
	}

	// Go to sleep condition
	public bool SleepCondition (NPC npc, Room room)
	{
		bool result = false;
		if (npc.stats.sanity < npc.stats.sanityTreshold || npc.currentTask == NPC.Tasks.Sleep) result = true;
		return result;
	}

	// Eliminate body condition
	public bool EliminateDeadBodyCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.ContainsDead()) result = true;
		return result;
	}

	// Heal other character condition
	public bool HealOtherCondition (NPC npc, Room room)
	{
		bool result = false;
		if (room.ContainsWounded()) result = true;
		return result;
	}

	// Navigate to room condition
	public bool NavigateCondition (NPC npc, Room room)
	{
		bool result = false;
		if (npc.currentTask == NPC.Tasks.Navigate) result = true;
		return result;
	}
}
