using System.Collections;

public interface ICharacterAIHandler {

	IState CurrentState { get; }

	IState[] AiStates { get; }

	void React ();

	void Purge ();

	T GetState<T> ();

	void ForceState<T>();

	void ForceState (IState state);

}
