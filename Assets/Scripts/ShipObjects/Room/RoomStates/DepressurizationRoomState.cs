using UnityEngine;
using System.Collections;
using System.Linq;

public class DepressurizationRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsDepressurized;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool destroyed = stats.IsDestroyed; 
		bool fixd = stats.Durability >= 50f; 
		return fixd || destroyed;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.ReduceFire (999f);
		stats.HasWeatherThreat = false;
		stats.DamagePlants (999f);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		stats.Repair (999f);
		stats.Damage (151f);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.Damage (999f);
		CurrentAnimator.SetFloat("Durability", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("Durability", stats.Durability);
		for (int i = 0; i < objects.Characters.Count; i++ ) objects.Characters[i].Hurt(2.5f);
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (UnityEngine.Random.value > 0.95f) controller.Neighbors.ElementAt(i).Room.Damage(2.5f);
		}
	}
	
}
