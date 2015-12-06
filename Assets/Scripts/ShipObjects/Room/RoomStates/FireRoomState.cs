using UnityEngine;
using System.Collections;
using System.Linq;

public class FireRoomState : RoomStateBase {

	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsOnFire();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = CurrentRoom.Stats.Durability <= 50f; 
		bool weather = CurrentRoom.Stats.WeatherThreat; 
		bool extinguished = CurrentRoom.Stats.FireLevel <= 0f;
		return broken || weather || extinguished;
	}

	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.FireLevel += Mathf.Ceil(CurrentRoom.Stats.ChemistryLevel / 3f);
		CurrentRoom.Stats.FireLevel += Mathf.Ceil(CurrentRoom.Stats.PlantsLevel / 3f);
		CurrentRoom.Stats.ChemistryLevel = 0f;
		CurrentRoom.Stats.PlantsLevel = 0f;
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.IsOnFire()) CurrentRoom.Stats.FireLevel = amount;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.FireLevel = 0f;
		CurrentAnimator.SetFloat("FireLevel", -1f);
	}
	
	public override void Tick () 
	{ 
		if (DisableCondition ()) 
		{
			StateDisable (); 
			return;
		}
		CurrentAnimator.SetFloat("FireLevel", CurrentRoom.Stats.FireLevel);
		float damage = Mathf.Ceil(CurrentRoom.Stats.FireLevel / 20f);
		CurrentRoom.Stats.FireLevel += damage;
		CurrentRoom.Stats.Durability -= damage;
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) CurrentRoom.Objects.Characters[i].Hurt(damage);
		for (int i = 0; i < CurrentRoom.neighbors.Count; i++ ) 
		{
			if (CurrentRoom.Stats.FireLevel > 75f && UnityEngine.Random.value > 0.8f) 
				CurrentRoom.neighbors.Keys.ElementAt(i).SatesHandler.ForceState<FireRoomState>(10f);
		}
	}

}
