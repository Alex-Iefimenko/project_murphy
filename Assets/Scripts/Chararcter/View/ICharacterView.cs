
public interface ICharacterView {

	void SetState (int newState);

	void SetSubState (int newState); 

	void SetCustomBool (string name, bool param);

	void RotateTowards (UnityEngine.Vector3 nextPoint);

	void Purge ();
}
