using UnityEngine;
using System.Collections;
using System;

public class CharacterAIHandler : ICharacterAIHandler {

	private System.Func<Room, bool>[] aiPrioriteis;
	private IState[] aiStates;
	private IState currentState = null;
	private ICharacter character;

	// Initialize
	public CharacterAIHandler (CharacterMain actualChar, string[] priorities)
	{
		character = actualChar;
		aiPrioriteis = new Func<Room, bool>[priorities.Length];
		aiStates = new IState[priorities.Length];
		for (int i = 0; i < priorities.Length; i++)
		{
			System.Reflection.MethodInfo method = typeof(CharacterAIHandler).GetMethod(priorities[i]);
			aiPrioriteis[i] = (Func<Room, bool>) Delegate.CreateDelegate(typeof(Func<Room, bool>), this, method);
			aiStates[i] = (IState)Activator.CreateInstance(Type.GetType(priorities[i] + "State")); 
		}
	}

	public void Purge () 
	{
		currentState = null;
	}

	public void React()
	{
		IState newState = DetectState ();
		if (currentState != newState) ChageState(newState);
		currentState.ExecuteStateActions();
	}

	public void ForceState<T>()
	{
		for (int i = 0; i < aiStates.Length; i++) 
		{ 
			if (aiStates[i].GetType() == typeof(T)) { ChageState(aiStates[i]); }
		}
	}

	private void ChageState(IState newState)
	{
		currentState = newState;
		currentState.Actualize();
	}

	private IState DetectState ()
	{
		int checksDepth = currentState == null ? aiPrioriteis.Length : Array.IndexOf(aiPrioriteis, currentState);
		for (int i = 0; i < checksDepth; i++) 
		{
			if (aiPrioriteis[i].Invoke(character.Movement.CurrentRoom)) return aiStates[i];
		}
		return currentState;
	}

	// Dead Condition
	public bool Dead (Room room)
	{
		return character.Stats.IsDead();
	}

	// Dead Condition
	public bool Unconsitious (Room room)
	{
		return character.Stats.IsUnconscious();
	}

	//	 Fight conditions
	public bool Attack (Room room)
	{
		return room.ContainsHostile(character) != null;
	}
	
	// Heal conditions
	public bool HealHimself (Room room)
	{
		return character.Stats.Health < character.Stats.HealthThreshold;
	}
	
	// Repair conditions
	public bool Repair (Room room)
	{
		return room.State.IsBroken();
	}
	
	// Fire Extinguish conditions
	public bool Extinguish (Room room)
	{
		return room.State.IsOnFire();
	}
	
	// Take body away condition
	public bool TakeWoundedBody (Room room)
	{
		return room.ContainsUnconscious() != null;
	}
	
	// Go to eat condition
	public bool Eat (Room room)
	{
		return character.Stats.Fatigue <= character.Stats.FatigueThreshold;;
	}
	
	// Go to sleep condition
	public bool Sleep (Room room)
	{
		return character.Stats.Sanity <= character.Stats.SanityThreshold;
	}
	
	// Eliminate body condition
	public bool EliminateDeadBody (Room room)
	{
		return room.ContainsDead() != null;
	}
	
	// Heal other character condition
	public bool HealOther (Room room)
	{
		return room.ContainsWounded(character) != null;
	}
	
	// Navigate to room condition
	public bool Navigate (Room room)
	{
		return currentState.StateKind == 5;
	}

	// Navigate to room condition
	public bool Work (Room room)
	{
		return UnityEngine.Random.value > character.Stats.RestProbability;
	}

	// Navigate to room condition
	public bool Rest (Room room)
	{
		return true;
	}

}
