using UnityEngine;
using System.Collections;
using System;

public class CharacterAIHandler : ICharacterAIHandler {

	private System.Func<Room, bool>[] aiPrioriteis;
	private IReaction[] aiReactions;
	private IReaction currentReaction = null;
	private ICharacter character;

	// Initialize
	public CharacterAIHandler (CharacterMain actualChar, string[] priorities)
	{
		character = actualChar;
		aiPrioriteis = new Func<Room, bool>[priorities.Length];
		aiReactions = new IReaction[priorities.Length];
		for (int i = 0; i < priorities.Length; i++)
		{
			System.Reflection.MethodInfo method = typeof(CharacterAIHandler).GetMethod(priorities[i]);
			aiPrioriteis[i] = (Func<Room, bool>) Delegate.CreateDelegate(typeof(Func<Room, bool>), this, method);
			aiReactions[i] = (IReaction)Activator.CreateInstance(Type.GetType(priorities[i] + "Reaction")); 
		}
	}
	
	public void React()
	{
		IReaction newReaction = DetectReaction ();
		if (newReaction != currentReaction) currentReaction = newReaction;
	}

	private IReaction DetectReaction ()
	{
		for (int i = 0; i < aiPrioriteis.Length; i++) 
		{
			if (aiPrioriteis[i].Invoke(character.Movement.CurrentRoom)) return aiReactions[i];
		}
		return null;
	}

	// Fight conditions
	public bool Attack (Room room)
	{
		bool result = false;
//		if (room.ContainsHostile(!npc.stats.isHostile, false)) result = true;
		return result;
	}
	
	// Heal conditions
	public bool Heal (Room room)
	{
		bool result = false;
//		if (npc.stats.health < npc.stats.healthTreshold) result = true;
		return result;
	}
	
	// Repair conditions
	public bool Repair (Room room)
	{
		bool result = false;
//		if (room.currentDurability < room.maxDurability) result = true;
		return result;
	}
	
	// Fire Extinguish conditions
	public bool Extinguish (Room room)
	{
		bool result = false;
//		if (room.roomStatus["fire"] > 0f) result = true;
		return result;
	}
	
	// Take body away condition
	public bool TakeBody (Room room)
	{
		bool result = false;
//		if (room.ContainsUnconscious()) result = true;
		return result;
	}
	
	// Go to eat condition
	public bool Eat (Room room)
	{
		bool result = false;
//		if (npc.stats.fatigue < npc.stats.fatigueTreshold || npc.currentTask == CharacterTasks.Tasks.Eat) result = true;
		return result;
	}
	
	// Go to sleep condition
	public bool Sleep (Room room)
	{
		bool result = false;
//		if (npc.stats.sanity < npc.stats.sanityTreshold || npc.currentTask == CharacterTasks.Tasks.Sleep) result = true;
		return result;
	}
	
	// Eliminate body condition
	public bool EliminateDeadBody (Room room)
	{
		bool result = false;
//		if (room.ContainsDead()) result = true;
		return result;
	}
	
	// Heal other character condition
	public bool HealOther (Room room)
	{
		bool result = false;
//		if (room.ContainsWounded()) result = true;
		return result;
	}
	
	// Navigate to room condition
	public bool Navigate (Room room)
	{
		bool result = false;
//		if (npc.currentTask == CharacterTasks.Tasks.Navigate) result = true;
		return result;
	}

	// Navigate to room condition
	public bool Work (Room room)
	{
		bool result = false;
		//		if (npc.currentTask == CharacterTasks.Tasks.Navigate) result = true;
		return result;
	}

	// Navigate to room condition
	public bool Rest (Room room)
	{
		bool result = false;
		//		if (npc.currentTask == CharacterTasks.Tasks.Navigate) result = true;
		return result;
	}

}
