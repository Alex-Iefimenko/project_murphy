using UnityEngine;
using System.Collections;

public class ViewObserver {

	private ICharacterView view;
	private IMovement movement;
	private ICharacterAIHandler aiHandler;
	private IState currentState;

	public ViewObserver (ICharacterView newView, ICharacterAIHandler newAiHandler, IMovement newMovement)
	{
		view = newView;
		aiHandler = newAiHandler;
		movement = newMovement;
		movement.LookOn += TurnTo;
		aiHandler.OnStateChange += ChangeState;
	}

	private void TurnTo (Vector3 point)
	{
		view.RotateTowards (point);
	}

	private void ChangeState (IState newState)
	{
		if (currentState != null) UnsubscribeFromState (currentState);
		currentState = newState;
		view.SetState (currentState.StateKind);
		currentState.SetCustomBool += CustomBoolChangeChange;
		currentState.SubStateChange += SubStateChange;
	}

	private void UnsubscribeFromState (IState state)
	{
		state.SetCustomBool -= CustomBoolChangeChange;
		state.SubStateChange -= SubStateChange;
	}

	private void SubStateChange (int subState)
	{
		view.SetSubState (subState);
	}

	private void CustomBoolChangeChange (string name, bool state)
	{
		view.SetCustomBool (name, state);
	}
}
