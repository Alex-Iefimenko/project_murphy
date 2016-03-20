using UnityEngine;
using System.Collections;
using System.Linq;

public class RadiationHazardRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsRadioactive();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool chemistry = CurrentRoom.Stats.IsHazardous();
		bool destroyed = CurrentRoom.Stats.Durability <= 0f;
		bool weather = CurrentRoom.Stats.WeatherThreat; 
		bool cleaned = CurrentRoom.Stats.RadiationLevel <= 0f;
		return destroyed || weather || cleaned || chemistry;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.IsRadioactive()) CurrentRoom.Stats.RadiationLevel = amount;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.RadiationLevel = 0f;
		CurrentAnimator.SetFloat("RadiationLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("RadiationLevel", CurrentRoom.Stats.RadiationLevel);
		CurrentRoom.Stats.RadiationLevel += Mathf.Ceil(CurrentRoom.Stats.RadiationLevel / 20f);
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) 
		{
			if (CurrentRoom.Stats.RadiationLevel > 25f && UnityEngine.Random.value > 0.2f) 
				CurrentRoom.Objects.Characters[i].Infect (1f);
		}
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (CurrentRoom.Stats.RadiationLevel > 75f && UnityEngine.Random.value > 0.8f) 
				CurrentRoom.neighbors.Keys.ElementAt(i).SatesHandler.ForceState<RadiationHazardRoomState>(10f);
		}
	}
	
}
