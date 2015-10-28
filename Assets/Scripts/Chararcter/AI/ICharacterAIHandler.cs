using System.Collections;

public interface ICharacterAIHandler {

	void React ();

	void Purge ();

	void ForceState<T>();
}
