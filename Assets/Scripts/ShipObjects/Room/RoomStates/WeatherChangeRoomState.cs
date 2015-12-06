using UnityEngine;
using System.Collections;
using System.Linq;

public class WeatherChangeRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.WeatherThreat;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool repaired = CurrentRoom.Stats.Durability >= CurrentRoom.Stats.MaxDurability;
		bool gravity = CurrentRoom.Stats.NoGravity;
		bool depress = CurrentRoom.Stats.Durability <= 50f;
		return repaired || gravity || depress;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.ChemistryLevel = 0f;
		CurrentRoom.Stats.RadiationLevel = 0f;
		CurrentRoom.Stats.FireLevel = 0f;
		CurrentAnimator.SetBool("WeatherThreat", true);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.WeatherThreat) CurrentRoom.Stats.WeatherThreat = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.WeatherThreat = false;
		CurrentAnimator.SetBool("WeatherThreat", false);
	}
	
	public override void Tick () 
	{ 
		if (DisableCondition ()) 
		{
			StateDisable (); 
			return;
		}
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) CurrentRoom.Objects.Characters[i].Hurt(2.5f);
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (CurrentRoom.Stats.WeatherThreat && UnityEngine.Random.value > 0.995f) 
				CurrentRoom.neighbors.Keys.ElementAt(i).SatesHandler.ForceState<WeatherChangeRoomState>(10f);
		}
	}
	
}
