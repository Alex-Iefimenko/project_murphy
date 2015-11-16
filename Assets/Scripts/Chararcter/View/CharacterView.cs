using UnityEngine;
using System.Collections;

public class CharacterView : MonoBehaviour, ICharacterView {

	private enum Projections{ Down = 0, Left = 1, Right = 2, Up = 3 };
	private ICharacter character;
	private Animator characterAnimator;

	// Use this for initialization
	void Awake () {
		character = (ICharacter)gameObject.GetComponent<CharacterMain>();
		characterAnimator = gameObject.GetComponent<Animator>();
		string localSide = System.Enum.GetName (typeof(CharacterMain.CharacterSides), character.Side);
		string localType = System.Enum.GetName (typeof(CharacterMain.CharacterTypes), character.Type);
		string path = "Characters/Controllers/" + localSide + "/" + localType + "/Controller";
		RuntimeAnimatorController controller = Resources.Load (path) as RuntimeAnimatorController;
		characterAnimator.runtimeAnimatorController = controller;
		UpdateProjection (Projections.Down);
	}

	public void SetState (int newState)
	{
		if (characterAnimator.GetInteger("State") != newState) characterAnimator.SetInteger("State", newState);
	}

	public void SetSubState (int newSubState)
	{
		if (characterAnimator.GetInteger("SubState") != newSubState) characterAnimator.SetInteger("SubState", newSubState);
	}

	public void SetCustomBool (string name, bool param)
	{
		if (characterAnimator.GetBool(name) != param) characterAnimator.SetBool(name, param);
	}

	// Method for handle sprite Character representation update
	public void RotateTowards (Vector3 nextPoint)
	{
		Vector3 p1 = transform.position;
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
		characterAnimator.SetInteger("SubState", -1);
		characterAnimator.SetInteger("State", -1);
	}

	private void UpdateProjection (Projections projection)
	{
		switch (projection)
		{
		case Projections.Up:
			characterAnimator.SetFloat("ProjectionX", 0f);
			characterAnimator.SetFloat("ProjectionY", 1f);
			break;
		case Projections.Down:
			characterAnimator.SetFloat("ProjectionX", 0f);
			characterAnimator.SetFloat("ProjectionY", -1f);
			break;
		case Projections.Left:
			characterAnimator.SetFloat("ProjectionX", -1f);
			characterAnimator.SetFloat("ProjectionY", 0f);
			break;
		case Projections.Right:
			characterAnimator.SetFloat("ProjectionX", 1f);
			characterAnimator.SetFloat("ProjectionY", 0f);
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
