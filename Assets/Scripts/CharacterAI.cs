using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAI {

	// NPC Task priority list
	public CharacterTasks.Tasks[] taskPriority = new CharacterTasks.Tasks[0];

	// Basic tasks for NPC
	public ArrayList criticalTasks  = new ArrayList () {CharacterTasks.Tasks.Heal, CharacterTasks.Tasks.Attack, CharacterTasks.Tasks.Navigate};
	public ArrayList importantTasks = new ArrayList () {CharacterTasks.Tasks.Repair, CharacterTasks.Tasks.Extinguish, CharacterTasks.Tasks.TakeBody};
	public ArrayList statsTasks     = new ArrayList () {CharacterTasks.Tasks.Eat, CharacterTasks.Tasks.Sleep};
	public CharacterTasks.Tasks[] usualTasks     = new CharacterTasks.Tasks[2] {CharacterTasks.Tasks.Work, CharacterTasks.Tasks.Rest};

	// NPC Type tasks
	public ArrayList engineerTasks  = new ArrayList () {CharacterTasks.Tasks.Repair};
	public ArrayList doctorTasks    = new ArrayList () {CharacterTasks.Tasks.HealOther, CharacterTasks.Tasks.EliminateDeadBody};
	public ArrayList safetyTasks    = new ArrayList () {CharacterTasks.Tasks.Attack, CharacterTasks.Tasks.Extinguish};
	public ArrayList scientistTasks = new ArrayList () {};

	// Hash with Condition methods
	public Dictionary<CharacterTasks.Tasks, System.Func<CharacterTasks, Room, bool>> conditionMethods;
	
	// Generate AI Handler object. Creates list with all priorities
	public CharacterAI (CharacterTasks npc)
	{
		ArrayList npcTypeTasks = GetNPCTypeTasks (npc);
		
		foreach (CharacterTasks.Tasks action in npcTypeTasks) 
			if (importantTasks.Contains(action)) importantTasks.Remove(action);
		
		ArrayList taskPriorityArray = new ArrayList();
		
		taskPriorityArray.AddRange (criticalTasks);
		taskPriorityArray.AddRange (npcTypeTasks);
		taskPriorityArray.AddRange (importantTasks);
		taskPriorityArray.AddRange (statsTasks);
		
		taskPriority =  (CharacterTasks.Tasks[]) taskPriorityArray.ToArray (typeof(CharacterTasks.Tasks));
		
		CreateConditionDictionary ();
	}

	public ArrayList GetNPCTypeTasks (CharacterTasks npc)
	{
		string typeTasks = System.Enum.GetName (typeof(CharacterTasks.NPCTypes), npc.npcType).ToLower() + "Tasks";
		ArrayList npcTypeTasks = this.GetType ().GetField (typeTasks).GetValue (this) as ArrayList;
		return npcTypeTasks;
	}

	// Create Dictionary with Delegates for Condition checks
	public void CreateConditionDictionary () 
	{
		conditionMethods = new Dictionary<CharacterTasks.Tasks, System.Func<CharacterTasks, Room, bool>> ()
		{
			{ CharacterTasks.Tasks.Attack            , AttackCondition },
			{ CharacterTasks.Tasks.Heal              , HealCondition },
			{ CharacterTasks.Tasks.Repair            , RepairCondition },
			{ CharacterTasks.Tasks.Extinguish        , ExtinguishCondition }, 
			{ CharacterTasks.Tasks.TakeBody          , TakeBodyCondition },
			{ CharacterTasks.Tasks.Eat               , EatCondition },
			{ CharacterTasks.Tasks.Sleep             , SleepCondition },
			{ CharacterTasks.Tasks.HealOther         , HealOtherCondition },
			{ CharacterTasks.Tasks.EliminateDeadBody , EliminateDeadBodyCondition },
			{ CharacterTasks.Tasks.Navigate          , NavigateCondition }
		}; 
	}

	// Iterate over task conditions and returns current task
	public CharacterTasks.Tasks GetCurrentTask (CharacterTasks npc) {
		Room currentRoom = npc.movement.currentRoom;
		foreach (CharacterTasks.Tasks action in taskPriority) 
		{
			if (conditionMethods.ContainsKey(action) && conditionMethods[action].Invoke(npc, currentRoom)) 
				return action;
		}
		return (Random.value > npc.stats.restProbalility) ? usualTasks[0] : usualTasks[1];
	}

	// Fight conditions
	public bool AttackCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.ContainsHostile(!npc.stats.isHostile, false)) result = true;
		return result;
	}

	// Heal conditions
	public bool HealCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (npc.stats.health < npc.stats.healthTreshold) result = true;
		return result;
	}

	// Repair conditions
	public bool RepairCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.currentDurability < room.maxDurability) result = true;
		return result;
	}

	// Fire Extinguish conditions
	public bool ExtinguishCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.roomStatus["fire"] > 0f) result = true;
		return result;
	}

	// Take body away condition
	public bool TakeBodyCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.ContainsUnconscious()) result = true;
		return result;
	}

	// Go to eat condition
	public bool EatCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (npc.stats.fatigue < npc.stats.fatigueTreshold || npc.currentTask == CharacterTasks.Tasks.Eat) result = true;
		return result;
	}

	// Go to sleep condition
	public bool SleepCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (npc.stats.sanity < npc.stats.sanityTreshold || npc.currentTask == CharacterTasks.Tasks.Sleep) result = true;
		return result;
	}

	// Eliminate body condition
	public bool EliminateDeadBodyCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.ContainsDead()) result = true;
		return result;
	}

	// Heal other character condition
	public bool HealOtherCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (room.ContainsWounded()) result = true;
		return result;
	}

	// Navigate to room condition
	public bool NavigateCondition (CharacterTasks npc, Room room)
	{
		bool result = false;
		if (npc.currentTask == CharacterTasks.Tasks.Navigate) result = true;
		return result;
	}
}
