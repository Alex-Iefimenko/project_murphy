using UnityEngine;
using System.Collections;

public class PutExplosiveState : StateBase {
	
	private new int stateIndex = 103;
	private int tick;
	private bool deployed = false;
	private IRoom target = null;
	private GameObject bomb = null;

	public override int StateKind { get { return this.stateIndex; } }

	public PutExplosiveState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param)
	{
		bomb = Resources.Load ("DynamicPrefabs/Bomb") as GameObject;
	}
	
	public override bool EnableCondition (IRoom room) 
	{
		return !deployed && !coordinator.Done;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		target = FetchSharedGoal ();
		movement.Walk ().ToFurniture (target, "Random");
		tick = Random.Range(7, 10);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			tick -= 1;
			OnSubStateChange (1);
		}
	}

	public override bool DisableCondition () 
	{
		return tick <= 0;
	}

	public override void Purge ()
	{
		base.Purge ();
		Vector3 place = movement.GObject.transform.position;
		GameObject newBomb = GameObject.Instantiate(bomb, place, Quaternion.identity) as GameObject;
		newBomb.GetComponent<Bomb>().Room = target;	
		deployed = true;
		coordinator.Done = true;
	}

	private IRoom FetchSharedGoal ()
	{
		if (coordinator.Target == null) coordinator.Target = Ship.Inst.RandomNamedRoom().GObject;
//		if (character.Coordinator.Target == null) character.Coordinator.Target = ShipState.Inst.specRooms[Enums.RoomTypes.Disposal].gameObject;
		IRoom targetRoom = coordinator.Target.GetComponent<Room>() as IRoom;
		return targetRoom;
	}
}