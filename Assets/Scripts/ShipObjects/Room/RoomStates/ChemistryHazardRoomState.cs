using UnityEngine;
using System.Collections;
using System.Linq;

public class ChemistryHazardRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsHazardous;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool radiation = stats.IsRadioactive;
		bool fire = stats.IsOnFire;
		bool destroyed = stats.IsDestroyed;
		bool weather = stats.HasWeatherThreat; 
		bool cleaned = stats.IsHazardous;
		return destroyed || weather || cleaned || radiation || fire;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.DamagePlants(999f);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsHazardous) stats.SpreadHazard (amount);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.CleanHazard(999f);
		CurrentAnimator.SetFloat("ChemistryLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("ChemistryLevel", stats.HazardLevel);
		float damage = Mathf.Ceil(stats.HazardLevel / 20f);
		stats.SpreadHazard (damage);
		for (int i = 0; i < objects.Characters.Count; i++ ) objects.Characters[i].Hurt(damage);
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (stats.HazardLevel > 75f && UnityEngine.Random.value > 0.8f) 
				controller.Neighbors.ElementAt(i).Room.ForceState<ChemistryHazardRoomState>(10f);
		}
	}
	
}
