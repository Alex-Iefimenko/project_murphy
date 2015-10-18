using UnityEngine;
using System.Collections;

public class CharacterView : MonoBehaviour, ICharacterView {

	private Animator characterAnimator;

	// Use this for initialization
	void Awake () {
		characterAnimator = gameObject.GetComponent<Animator>();
	}

	public void SetState (int newState)
	{
		if (characterAnimator.GetInteger("State") != newState) characterAnimator.SetInteger("State", newState);
	}

	public void SetSubState (int newSubState)
	{
		if (characterAnimator.GetInteger("SubState") != newSubState) characterAnimator.SetInteger("SubState", newSubState);
	}

}
