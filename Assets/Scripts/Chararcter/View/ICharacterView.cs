
public interface ICharacterView {

	void SetState (int newState);

	void SetSubState (int newState); 

	void RotateTowards (UnityEngine.Vector3 nextPoint);

	void Purge ();
}
