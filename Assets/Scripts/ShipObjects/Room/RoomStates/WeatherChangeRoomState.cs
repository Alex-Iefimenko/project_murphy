using UnityEngine;
using System.Collections;
using System.Linq;

public class WeatherChangeRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.HasWeatherThreat;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool repaired = stats.IsRepaired;
		bool gravity = stats.HasNoGravity;
		bool depress = stats.Durability <= 50f;
		return repaired || gravity || depress;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.CleanHazard (999f);
		stats.ReduceRadiation (999f);
		stats.ReduceFire (999f);
		CurrentAnimator.SetBool("WeatherThreat", true);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.HasWeatherThreat) stats.HasWeatherThreat = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.HasWeatherThreat = false;
		CurrentAnimator.SetBool("WeatherThreat", false);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		for (int i = 0; i < objects.Characters.Count; i++ ) objects.Characters[i].Hurt(2.5f);
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (stats.HasWeatherThreat && UnityEngine.Random.value > 0.995f) 
				controller.Neighbors.ElementAt(i).Room.ForceState<WeatherChangeRoomState>(10f);
		}
	}
	
}
