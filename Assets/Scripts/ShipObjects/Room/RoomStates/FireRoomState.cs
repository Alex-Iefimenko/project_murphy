using UnityEngine;
using System.Collections;
using System.Linq;

public class FireRoomState : RoomStateBase {

	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsOnFire;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = stats.Durability <= 50f; 
		bool weather = stats.HasWeatherThreat; 
		bool extinguished = stats.FireLevel <= 0f;
		return broken || weather || extinguished;
	}

	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.SpreadFire (Mathf.Ceil(stats.HazardLevel / 3f));
		stats.SpreadFire (Mathf.Ceil(stats.HazardLevel / 3f));
		stats.CleanHazard (999f);
		stats.DamagePlants (999f);
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsOnFire) stats.SpreadFire(amount);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.ReduceFire (999f);
		CurrentAnimator.SetFloat("FireLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("FireLevel", stats.FireLevel);
		float damage = Mathf.Ceil(stats.FireLevel / 20f);
		stats.SpreadFire (damage);
		stats.Damage (damage);
		for (int i = 0; i < objects.Characters.Count; i++ ) objects.Characters[i].Hurt(damage);
		for (int i = 0; i < controller.Neighbors.Count; i++ ) 
		{
			if (stats.FireLevel > 75f && UnityEngine.Random.value > 0.8f) 
				controller.Neighbors.ElementAt(i).Room.ForceState<FireRoomState>(10f);
		}
	}

}
