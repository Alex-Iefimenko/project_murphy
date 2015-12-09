using UnityEngine;
using System.Collections;
using System.Linq;

public class PlantsMutationRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsPlantMutated();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = CurrentRoom.Stats.Durability <= 50f; 
		bool fire = CurrentRoom.Stats.FireLevel >= 0f;
		bool chemistry = CurrentRoom.Stats.ChemistryLevel >= 0f;
		return broken || fire || chemistry;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.IsPlantMutated()) CurrentRoom.Stats.PlantsLevel = amount;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.PlantsLevel = 0f;
		CurrentAnimator.SetFloat("PlantsLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("PlantsLevel", CurrentRoom.Stats.PlantsLevel);
		float damage = Mathf.Ceil(CurrentRoom.Stats.PlantsLevel / 20f);
		CurrentRoom.Stats.PlantsLevel += damage;
		CurrentRoom.Stats.Durability -= damage;
		CurrentRoom.Stats.PlantsLevel += Mathf.Ceil(CurrentRoom.Stats.RadiationLevel / 40f);
		if (CurrentRoom.Stats.WeatherThreat) CurrentRoom.Stats.PlantsLevel += 1f;
		if (CurrentRoom.Stats.NoGravity) CurrentRoom.Stats.PlantsLevel += 1f;
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (CurrentRoom.Stats.PlantsLevel > 75f && UnityEngine.Random.value > 0.95f) 
				CurrentRoom.neighbors.Keys.ElementAt(i).SatesHandler.ForceState<PlantsMutationRoomState>(10f);
		}
	}
	
}
