using System.Collections;

public interface ICharacterAIHandler {

	IState CurrentState { get; }

	IState[] AiStates { get; }

	void React ();

	void Purge ();

	void ForceState<T>();
}
