using UnityEngine;
using System.Collections;
using System.Linq;

public class WorkState : StateBase {
	
	private new int stateIndex = 13;
	private int tick;
	private System.Func<Room, bool>[] responsabilities;

	public override int StateKind { get { return this.stateIndex; } }

	public WorkState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		bool restState = (aiHandler.CurrentState != null && aiHandler.CurrentState.StateKind == 14);
		return (!restState && UnityEngine.Random.value > stats.RestProbability);
	}

	public override void Actualize () { 
		base.Actualize (); 
		if (responsabilities == null) CreateWorkDelegates ();
		movement.Walk ().ToFurniture(stats.BasicRoom, "Random");
		tick = Random.Range (7, 10);
	}

	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			OnSubStateChange (1);
			tick -= 1;
			CheckRelatedEvents();
		}
	}
	
	public override bool DisableCondition () 
	{
		return tick <= 0;
	}

	private void CreateWorkDelegates ()
	{
		responsabilities = new System.Func<Room, bool>[stats.WorkTasks.Length];
		for (int i = 0; i < stats.WorkTasks.Length; i++)
		{
			System.Type type = System.Type.GetType(stats.WorkTasks[i] + "State");
			System.Reflection.MethodInfo method = type.GetMethod("EnableCondition", new System.Type[] { typeof(Room) });
			responsabilities[i] = (System.Func<Room, bool>) System.Delegate.CreateDelegate(
				typeof(System.Func<Room, bool>), 
				aiHandler.GetState(type),
				method);
		}
	}

	private void CheckRelatedEvents()
	{
		Room[] rooms = ShipState.Inst.allRooms;
		for (int i = 0; i < responsabilities.Length; i++)
		{
			for (int j = 0; j < rooms.Length; j++)
			{
				if (responsabilities[i].Invoke(rooms[j])) ForceNavigate (rooms[j]);
			}
		}
	}

	private void ForceNavigate (Room room)
	{
		NavigateState nav = aiHandler.GetState<NavigateState> ();
		nav.TargetRoom = room;
		aiHandler.ForceState (nav);
	}
}
