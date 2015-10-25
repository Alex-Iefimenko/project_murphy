using UnityEngine;
using System.Collections;
using System.Linq;

public class AnimationTest : MonoBehaviour {

	public CharacterMain.CharacterSides side = CharacterMain.CharacterSides.Crew;
	public CharacterMain.CharacterTypes type = CharacterMain.CharacterTypes.Doctor;
//	public enum Subtype{ One = 1, Two = 2, Three = 3};
//	public Subtype subtype = Subtype.One;
	public enum Sides{ Down = 0, Left = 1, Right = 2, Up = 3 };
	public Sides currentSide = Sides.Up;
	private Animator cAnimator;
	public enum Animation{ Dead, Unconsitious, Attack, HealHimself, Navigate, Repair, Extinguish, HealOther, Eat, Sleep, EliminateDeadBody, TakeWoundedBody, Work, Rest };
	public Animation currentState = Animation.Dead;
	public int subState = 1;

	private RuntimeAnimatorController controller;

	void Awake ()  {
		// Get Door collider object
		cAnimator   = gameObject.GetComponent<Animator>();
		string localSide = System.Enum.GetName (typeof(CharacterMain.CharacterSides), side);
		string localType = System.Enum.GetName (typeof(CharacterMain.CharacterTypes), type);
//		int localSubtype = (int)subtype;
		string path = "Characters/Controllers/" + localSide + "/" + localType + "/Controller";
		controller = Resources.Load (path) as RuntimeAnimatorController;
		cAnimator.runtimeAnimatorController = controller;
	}

	// Update is called once per frame
	void Update () {
		cAnimator.SetInteger("Projection", (int)currentSide);
		cAnimator.SetInteger("State", (int)currentState);
		cAnimator.SetInteger("SubState", subState);
	}

}


