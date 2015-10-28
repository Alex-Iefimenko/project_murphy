using UnityEngine;
using System.Collections;

public class DefendState : StateBase {
	
	private int stateIndex = 15;
	
	public DefendState (CharacterMain character) : base(character) { }
	
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
