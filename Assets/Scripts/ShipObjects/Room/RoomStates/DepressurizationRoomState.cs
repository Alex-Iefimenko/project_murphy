using UnityEngine;
using System.Collections;
using System.Linq;

public class DepressurizationRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsDepressurized();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool destroyed = CurrentRoom.Stats.Durability <= 0f; 
		bool fixd = CurrentRoom.Stats.Durability >= 50f; 
		return fixd || destroyed;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.FireLevel = 0f;
		CurrentRoom.Stats.WeatherThreat = false;
		CurrentRoom.Stats.PlantsLevel = 0f;
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!(CurrentRoom.Stats.Durability <= 50f)) CurrentRoom.Stats.Durability = 50f;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.Durability = 0f;
		CurrentAnimator.SetFloat("Durability", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("Durability", CurrentRoom.Stats.Durability);
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) CurrentRoom.Objects.Characters[i].Hurt(2.5f);
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (UnityEngine.Random.value > 0.95f) CurrentRoom.neighbors.Keys.ElementAt(i).Damage(2.5f);
		}
	}
	
}
