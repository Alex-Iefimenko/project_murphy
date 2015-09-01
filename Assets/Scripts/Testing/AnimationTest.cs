using UnityEngine;
using System.Collections;
using System.Linq;

public class AnimationTest : MonoBehaviour {

	public enum Sides{ Down, Left, Right, Up };
	public Sides currentSide = Sides.Up;
	private Animator cAnimator;
 	public enum Animation{ Idle, Walk, Repair, Extinguish, Iteract, Punch, Shot, Sleep, Pull, Unconscious, Dead };
	public Animation currentAnimation = Animation.Idle;

	private RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];

	void Awake ()  {
		// Get Door collider object
		cAnimator   = gameObject.GetComponent<Animator>();
		controllers = Resources.LoadAll ("NPC/Crew/").Cast<RuntimeAnimatorController>().ToArray();

	}

	// Update is called once per frame
	void Update () {
		UpdateAnimator((int)currentSide);
		cAnimator.SetInteger("State", (int)currentAnimation);
	}

	private void UpdateAnimator (int direction)
	{
		cAnimator.runtimeAnimatorController = controllers[direction];
	}

}
