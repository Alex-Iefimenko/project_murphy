using UnityEngine;
using System.Collections;
using System.Linq;

public class WorkState : StateBase {
	
	private int stateIndex = 13;
	private int tick;
	private System.Func<Room, bool>[] responsabilities;
	
	public WorkState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		bool restState = (character.AiHandler.CurrentState != null && character.AiHandler.CurrentState.StateKind == 14);
		return (!restState && UnityEngine.Random.value > character.Stats.RestProbability);
	}

	public override void Actualize () { 
		base.Actualize (); 
		if (responsabilities == null) CreateWorkDelegates ();
		NavigateTo(character.Stats.BasicRoom);
		tick = Random.Range(7, 10);
	}

	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false)
		{
			character.View.SetSubState(1);
			tick -= 1;
			CheckRelatedEvents();
		}
		if (tick <= 0)
			character.PurgeActions();
	}

	private void CreateWorkDelegates ()
	{
		responsabilities = new System.Func<Room, bool>[character.Stats.WorkTasks.Length];
		for (int i = 0; i < character.AiHandler.AiStates.Length; i++)
		{
			string state = character.AiHandler.AiStates[i].GetType().ToString().Replace("State", "");
			if (character.Stats.WorkTasks.Contains(state))
			{
				System.Reflection.MethodInfo method = System.Type.GetType(state + "State").GetMethod("CheckCondition");
				responsabilities[System.Array.IndexOf(character.Stats.WorkTasks, state)] =
					(System.Func<Room, bool>) System.Delegate.CreateDelegate(
						typeof(System.Func<Room, bool>), 
						character.AiHandler.AiStates[i],
						method);
			}
		}

	}

	private void CheckRelatedEvents()
	{
		Room[] rooms = ShipState.Inst.allRooms;
		for (int i = 0; i < responsabilities.Length; i++)
		{
			for (int j = 0; j < rooms.Length; j++)
			{
				if (responsabilities[i].Invoke(rooms[j])) character.Navigate(rooms[j], false);
			}
		}
	}
}
