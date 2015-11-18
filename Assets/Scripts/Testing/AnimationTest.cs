using UnityEngine;
using System.Collections;
using System.Linq;

public class AnimationTest : MonoBehaviour {

	public CharacterMain.CharacterSides side = CharacterMain.CharacterSides.Crew;
	public CharacterMain.CharacterTypes type = CharacterMain.CharacterTypes.Doctor;
	public enum Sides{ Down = 0, Left = 1, Right = 2, Up = 3 };
	public Sides currentProjection = Sides.Up;
	private Animator cAnimator;
	public enum Animation{ Dead = 1, Unconsitious = 2, Attack = 3, HealHimself = 4, Navigate = 5, Repair = 6, 
		Extinguish = 7, HealOther = 8, Eat = 9, Sleep = 10, EliminateDeadBody = 11, TakeWoundedBody = 12, 
		Work = 13, Rest = 14, Defend = 15, CleanRadiation = 16, CleanChemistry = 17 };
	public Animation currentState = Animation.Dead;
	public int subState = 1;

	private RuntimeAnimatorController controller;

	void Awake ()  {
		// Get Door collider object
		cAnimator   = gameObject.GetComponent<Animator>();
		string localSide = System.Enum.GetName (typeof(CharacterMain.CharacterSides), side);
		string localType = System.Enum.GetName (typeof(CharacterMain.CharacterTypes), type);
		string path = "Characters/Controllers/" + localSide + "/" + localType + "/Controller";
		controller = Resources.Load (path) as RuntimeAnimatorController;
		cAnimator.runtimeAnimatorController = controller;
	}

	// Update is called once per frame
	void Update () {
		cAnimator.SetInteger("State", (int)currentState);
		cAnimator.SetInteger("SubState", subState);
		switch (currentProjection)
		{
			case Sides.Up:
				cAnimator.SetFloat("ProjectionX", 0f);
				cAnimator.SetFloat("ProjectionY", 1f);
				break;
			case Sides.Down:
				cAnimator.SetFloat("ProjectionX", 0f);
				cAnimator.SetFloat("ProjectionY", -1f);
				break;
			case Sides.Left:
				cAnimator.SetFloat("ProjectionX", -1f);
				cAnimator.SetFloat("ProjectionY", 0f);
				break;
			case Sides.Right:
				cAnimator.SetFloat("ProjectionX", 1f);
				cAnimator.SetFloat("ProjectionY", 0f);
				break;
		}
	}

}


