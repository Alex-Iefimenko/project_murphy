using System.Collections;

public delegate void StateChangeHandler (IState newState);

public interface ICharacterAIHandler {

	IState CurrentState { get; }

	IState[] AiStates { get; }

	event StateChangeHandler OnStateChange;

	void ChangeState (string currentReaction, string newReaction);

	void ChangeStateChain (string[] newReactionChain);

	void React ();

	void PurgeState ();

	T GetState<T> ();

	IState GetState (System.Type type);

	void ForceState<T>();

	void ForceState (IState state);

}
