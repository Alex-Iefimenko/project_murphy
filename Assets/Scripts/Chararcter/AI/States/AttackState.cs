using UnityEngine;
using System.Collections;

public class AttackState : StateBase {
	
	private int stateIndex = 3;

	public AttackState (CharacterMain character) : base(character) { }

	public override int StateKind { get { return stateIndex; } }

	public override void Actualize () 
	{ 
		base.Actualize (); 
	}

	public override void ExecuteStateActions () 
	{
		Debug.Log ("SATE IS NOT IMPLEMENTED YET");
	}
	//	// Fight action
	//	void Attack () 
	//	{
	//		SetTaskNPC(movement.currentRoom.ContainsHostile(!stats.isHostile, false));
	//		if (!stats.ableDistantAttack) SetTaskAdress(taskNPC.gameObject);
	//	}

}
