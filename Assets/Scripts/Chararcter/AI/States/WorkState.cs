using UnityEngine;
using System.Collections;
using System.Linq;

public class WorkState : StateBase {
	
	private int stateIndex = 13;
	private int tick;
	private System.Func<Room, bool>[] responsabilities;
	
	public WorkState (CharacterMain character) : base(character) 
	{ 
		responsabilities = new System.Func<Room, bool>[character.Stats.WorkTasks.Length];
		for (int i = 0; i < character.Stats.WorkTasks.Length; i++)
		{
			System.Reflection.MethodInfo method = typeof(CharacterAIHandler).GetMethod(character.Stats.WorkTasks[i]);
			responsabilities[i] = (System.Func<Room, bool>) System.Delegate.CreateDelegate(
				typeof(System.Func<Room, bool>), 
				(CharacterAIHandler)character.AiHandler, 
				method);
		}
	}
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Stats.BasicRoom);
		tick = Random.Range(7, 10);
	}

	public override void ExecuteStateActions () 
	{
		if (character.Movement.CurrentRoom == character.Stats.BasicRoom) 
		{
			tick -= 1;
			CheckRelatedEvents();
		}
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (tick <= 0)
			character.PurgeActions();
	}

	private void CheckRelatedEvents()
	{
		Room[] rooms = ShipState.allRooms.Values.ToArray();
		for (int i = 0; i < responsabilities.Length; i++)
		{
			for (int j = 0; j < rooms.Length; j++)
			{
				if (responsabilities[i].Invoke(rooms[j])) character.Navigate(rooms[j]);
			}
		}
	}
}
