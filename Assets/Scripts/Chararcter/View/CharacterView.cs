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

	public void Purge ()
	{
		characterAnimator.SetInteger("SubState", -1);
		characterAnimator.SetInteger("State", -1);
	}

}

//	// Chat notification start
//	public void isChat ()
//	{
//		if (chatting && timer <= 0f)
//		{
//			chatting = false;
//			timer = Random.Range(1f, 2f);
//		}
////		else if (!chatting && movement.currentRoom.npcCollidercs.Count > 1 && timer <= 0f)
//		{
//			chatting = true;
//			sRenderer.sprite = actions["Speak"];
//			timer = Random.Range(1f, 2f);
//		}
//	}
//}
