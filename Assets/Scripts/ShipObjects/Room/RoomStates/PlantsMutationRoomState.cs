using UnityEngine;
using System.Collections;
using System.Linq;

public class PlantsMutationRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsPlantMutated;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = stats.Durability <= 50f; 
		bool fire = stats.IsOnFire;
		bool chemistry = stats.IsHazardous;
		return broken || fire || chemistry;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsPlantMutated) stats.GrowPlants(amount);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.DamagePlants(999f);
		CurrentAnimator.SetFloat("PlantsLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("PlantsLevel", stats.PlantLevel);
		float damage = Mathf.Ceil(stats.PlantLevel / 20f);
		stats.GrowPlants (damage);
		stats.Damage (damage);
		stats.GrowPlants (Mathf.Ceil(stats.RadiationLevel / 40f));
		if (stats.HasWeatherThreat) stats.GrowPlants (1f);
		if (stats.HasNoGravity) stats.GrowPlants (1f);
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (stats.PlantLevel > 75f && UnityEngine.Random.value > 0.95f) 
				controller.Neighbors.ElementAt(i).Room.ForceState<PlantsMutationRoomState>(10f);
		}
	}
	
}
