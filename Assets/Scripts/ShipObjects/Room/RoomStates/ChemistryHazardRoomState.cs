using UnityEngine;
using System.Collections;
using System.Linq;

public class ChemistryHazardRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsHazardous();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool radiation = CurrentRoom.Stats.IsRadioactive();
		bool fire = CurrentRoom.Stats.IsOnFire();
		bool destroyed = CurrentRoom.Stats.Durability <= 0f;
		bool weather = CurrentRoom.Stats.WeatherThreat; 
		bool cleaned = CurrentRoom.Stats.ChemistryLevel <= 0f;
		return destroyed || weather || cleaned || radiation || fire;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.PlantsLevel = 0f;
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.IsHazardous()) CurrentRoom.Stats.ChemistryLevel = amount;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.ChemistryLevel = 0f;
		CurrentAnimator.SetFloat("ChemistryLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("ChemistryLevel", CurrentRoom.Stats.ChemistryLevel);
		float damage = Mathf.Ceil(CurrentRoom.Stats.ChemistryLevel / 20f);
		CurrentRoom.Stats.ChemistryLevel += damage;
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) CurrentRoom.Objects.Characters[i].Hurt(damage);
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (CurrentRoom.Stats.ChemistryLevel > 75f && UnityEngine.Random.value > 0.8f) 
				CurrentRoom.neighbors.Keys.ElementAt(i).SatesHandler.ForceState<ChemistryHazardRoomState>(10f);
		}
	}
	
}
