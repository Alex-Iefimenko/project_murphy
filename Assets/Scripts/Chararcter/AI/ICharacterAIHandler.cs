using System.Collections;

public interface ICharacterAIHandler {

	IState CurrentState { get; }

	IState[] AiStates { get; }

	void ChangeReaction (string currentReaction, string newReaction);

	void React ();

	void Purge ();

	T GetState<T> ();

	void ForceState<T>();

	void ForceState (IState state);

}
