using UnityEngine;
using System.Collections;

public class DefendState : StateBase {
	
	private int stateIndex = 15;
	private ICharacter enemy = null;
	
	public DefendState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () 
	{ 
		base.Actualize (); 

		enemy = character.Movement.CurrentRoom.ContainsHostile(character);
		NavigateTo(enemy);
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
