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
		bomb = Resources.Load ("DynamicPrefabs/Bomb") as GameObject;
	}
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		bool condition = 
			(!deployed && character.Coordinator == null) 
			|| (character.Coordinator != null && !deployed && !character.Coordinator.Done);
		return condition;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		target = (character.Coordinator != null) ? FetchSharedGoal () : ShipState.Inst.RandomNamedRoom();
		character.Movement.Walk().ToFurniture(target, "Random");
		tick = Random.Range(7, 10);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (character.Movement.IsMoving == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
		}
	}

	public override bool DisableCondition () 
	{
		return tick <= 0;
	}

	public override void Purge ()
	{
		base.Purge ();
		Vector3 place = character.GObject.transform.position;
		GameObject newBomb = GameObject.Instantiate(bomb, place, Quaternion.identity) as GameObject;
		newBomb.GetComponent<Bomb>().Room = target;	
		deployed = true;
		if (character.Coordinator != null) character.Coordinator.Done = true;
		character.PurgeActions();
	}

	private Room FetchSharedGoal ()
	{
		if (character.Coordinator.Target == null) character.Coordinator.Target = ShipState.Inst.RandomNamedRoom().gameObject;
//		if (character.Coordinator.Target == null) character.Coordinator.Target = ShipState.Inst.specRooms[Enums.RoomTypes.Disposal].gameObject;
		return character.Coordinator.Target.GetComponent<Room>();
	}
}