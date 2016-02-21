using UnityEngine;
using System.Collections;
using System;

public class CharacterAIHandler : ICharacterAIHandler {

	private IState[] aiStates;
	private IState currentState = null;
	private ICharacter character;

	public IState CurrentState { get { return currentState; } }
	public IState[] AiStates { get { return aiStates; } }

	// Initialize
	public CharacterAIHandler (CharacterMain actualChar, string[] priorities)
	{
		character = actualChar;
		aiStates = new IState[priorities.Length];
		for (int i = 0; i < priorities.Length; i++)
		{
			aiStates[i] = (IState)Activator.CreateInstance(Type.GetType(priorities[i] + "State"), character); 
		}
	}

	public void Purge () 
	{
		currentState = null;
	}

	public T GetState<T> ()
	{
		for (int i = 0; i < aiStates.Length; i++) 
		{ 
			if (aiStates[i].GetType() == typeof(T)) return (T)aiStates[i];
		}
		return default (T);
	}

	public void ForceState<T>()
	{
		for (int i = 0; i < aiStates.Length; i++) 
		{ 
			if (aiStates[i].GetType() == typeof(T)) { ChageState(aiStates[i]); }
		}
	}

	public void ForceState(IState state)
	{
		ChageState(state);
	}

	public void React()
	{
		IState newState = DetectState ();
		if (currentState != newState) ChageState(newState);
		currentState.ExecuteStateActions();
	}

	private void ChageState(IState newState)
	{
		character.PurgeActions();
		currentState = newState;
		currentState.Actualize();
	}

	private IState DetectState ()
	{
		for (int i = 0; i < aiStates.Length; i++) 
		{
			if (aiStates[i] == currentState || aiStates[i].CheckCondition(character.Movement.CurrentRoom)) 
				return aiStates[i];
		}
		return currentState;
	}
}
