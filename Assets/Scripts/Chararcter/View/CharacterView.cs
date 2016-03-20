using UnityEngine;
using System.Collections;

public class CharacterView : ICharacterView {

	private enum Projections{ Down = 0, Left = 1, Right = 2, Up = 3 };
	private Transform characterTransaform;
	private Animator animator;

	public CharacterView (GameObject character, RuntimeAnimatorController controller)
	{
		characterTransaform = character.transform;
		animator = character.GetComponent<Animator>();
		animator.runtimeAnimatorController = controller;
		UpdateProjection (Projections.Down);
	}

	public void SetState (int newState)
	{
		if (animator.GetInteger("State") != newState) animator.SetInteger("State", newState);
	}

	public void SetSubState (int newSubState)
	{
		if (animator.GetInteger("SubState") != newSubState) animator.SetInteger("SubState", newSubState);
	}

	public void SetCustomBool (string name, bool param)
	{
		if (animator.GetBool(name) != param) animator.SetBool(name, param);
	}

	// Method for handle sprite Character representation update
	public void RotateTowards (Vector3 nextPoint)
	{
		Vector3 p1 = characterTransaform.position;
		Vector3 p2 = nextPoint;
		float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
		if ( 45f <= angle && angle <= 135f ) 
			UpdateProjection (Projections.Up);
		else if ( -135f <= angle && angle <= -45f )
			UpdateProjection (Projections.Down);
		else if ( -45f <= angle && angle <= 45f )
			UpdateProjection (Projections.Right);
		else if ( 135f <= angle || angle <= -135f )
			UpdateProjection (Projections.Left);
	}

	public void Purge ()
	{
		animator.SetInteger("SubState", -1);
		animator.SetInteger("State", -1);
	}

	private void UpdateProjection (Projections projection)
	{
		switch (projection)
		{
		case Projections.Up:
			animator.SetFloat("ProjectionX", 0f);
			animator.SetFloat("ProjectionY", 1f);
			break;
		case Projections.Down:
			animator.SetFloat("ProjectionX", 0f);
			animator.SetFloat("ProjectionY", -1f);
			break;
		case Projections.Left:
			animator.SetFloat("ProjectionX", -1f);
			animator.SetFloat("ProjectionY", 0f);
			break;
		case Projections.Right:
			animator.SetFloat("ProjectionX", 1f);
			animator.SetFloat("ProjectionY", 0f);
			break;
		}
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
