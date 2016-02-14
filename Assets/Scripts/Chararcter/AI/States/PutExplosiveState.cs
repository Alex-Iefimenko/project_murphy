using UnityEngine;
using System.Collections;

public class PutExplosiveState : StateBase {
	
	private int stateIndex = 103;
	private int tick;
	private bool deployed = false;
	private Room target = null;
	private GameObject bomb = null;
	
	public PutExplosiveState (CharacterMain character) : base(character) 
	{
		// TODO: Updated to for shared aim
		target = ShipState.Inst.RandomRoom();
		bomb = Resources.Load ("DynamicPrefabs/Bomb") as GameObject;
	}
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return !deployed;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		NavigateTo(target);
		tick = Random.Range(7, 10);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
		}
		if (tick <= 0)
		{
			Vector3 place = character.GObject.transform.position;
			GameObject newBomb = GameObject.Instantiate(bomb, place, Quaternion.identity) as GameObject;
			newBomb.GetComponent<Bomb>().Room = target;	
			deployed = true;
			character.PurgeActions();
		}
	}
}