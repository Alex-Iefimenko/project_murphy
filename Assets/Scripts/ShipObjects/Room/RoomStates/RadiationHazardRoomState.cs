using UnityEngine;
using System.Collections;
using System.Linq;

public class RadiationHazardRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsRadioactive;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool chemistry = stats.IsHazardous;
		bool destroyed = stats.IsDestroyed;
		bool weather = stats.HasWeatherThreat; 
		bool cleaned = stats.IsRadioactive;
		return destroyed || weather || cleaned || chemistry;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsRadioactive) stats.SpreadRadiation (amount);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.ReduceRadiation (999f);
		CurrentAnimator.SetFloat("RadiationLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("RadiationLevel", stats.RadiationLevel);
		stats.SpreadRadiation (Mathf.Ceil(stats.RadiationLevel / 20f));
		for (int i = 0; i < objects.Characters.Count; i++ ) 
		{
			if (stats.RadiationLevel > 25f && UnityEngine.Random.value > 0.2f) 
				objects.Characters[i].Infect (1f);
		}
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (stats.RadiationLevel > 75f && UnityEngine.Random.value > 0.8f) 
				controller.Neighbors.ElementAt(i).Room.ForceState<RadiationHazardRoomState>(10f);
		}
	}
	
}
