using UnityEngine;
using System.Collections;
using System;

public class CharacterAIHandler : ICharacterAIHandler
{
	private string[] priorities;
	private IState[] aiStates;
	private IState currentState = null;
	private AiStateParams stateParams;

	public IState CurrentState { get { return currentState; } }
	public IState[] AiStates { get { return aiStates; } }
	public event StateChangeHandler OnStateChange;

	// Initialize
	public CharacterAIHandler (string[] newPriorities, AiStateParams newParams)
	{
		stateParams = newParams;
		ChangeStateChain (newPriorities);
		Broadcaster.Instance.tickEvent += React;
	}

	public void ChangeState (string currentReaction, string newReaction)
	{
		int idx = Array.IndexOf (priorities, currentReaction);
		priorities [idx] = newReaction;
		aiStates [idx] = (IState)Activator.CreateInstance (Type.GetType (priorities [idx] + "State"), this, stateParams); 
	}

	public void ChangeStateChain (string[] newPriorities)
	{
		priorities = newPriorities;
		aiStates = new IState[priorities.Length];
		for (int i = 0; i < priorities.Length; i++) {
			aiStates [i] = (IState)Activator.CreateInstance (Type.GetType (priorities [i] + "State"), this, stateParams); 
		}
	}

	public void PurgeState ()
	{
		currentState = null;
	}

	public T GetState<T> ()
	{
		for (int i = 0; i < aiStates.Length; i++) { 
			if (aiStates [i].GetType () == typeof(T)) return (T)aiStates [i];
		}
		return default (T);
	}

	public IState GetState (System.Type type)
	{
		for (int i = 0; i < aiStates.Length; i++) { 
			if (aiStates [i].GetType () == type) return (IState)aiStates [i];
		}
		return null;
	}

	public void ForceState<T> ()
	{
		for (int i = 0; i < aiStates.Length; i++) { 
			if (aiStates [i].GetType () == typeof(T)) {
				ChageState (aiStates [i]);
			}
		}
	}

	public void ForceState (IState state)
	{
		ChageState (state);
	}

	public void React ()
	{
		IState newState = DetectState ();
		if (currentState != newState)
			ChageState (newState);
		currentState.Execute ();
	}

	private void Purge ()
	{
		if (currentState != null) currentState.Purge ();
		PurgeState ();
	}

	private void ChageState (IState newState)
	{
		Purge ();
		currentState = newState;
		if (OnStateChange != null) OnStateChange (newState);
		currentState.Actualize ();
	}

	private IState DetectState ()
	{
		for (int i = 0; i < aiStates.Length; i++) {
			if (aiStates [i] == currentState || aiStates [i].EnableCondition ()) 
				return aiStates [i];
		}
		return currentState;
	}

}
