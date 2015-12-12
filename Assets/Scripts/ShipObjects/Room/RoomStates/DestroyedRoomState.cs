using UnityEngine;
using System.Collections;

public class DestroyedRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.Durability <= 0f;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		return false;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.FireLevel = 0f;
		CurrentRoom.Stats.RadiationLevel = 0f;
		CurrentRoom.Stats.ChemistryLevel = 0f;
		CurrentRoom.Stats.PlantsLevel = 0f;
		CurrentRoom.Stats.Unelectryfied = false;
		CurrentRoom.Stats.WeatherThreat = false;
		CurrentRoom.Stats.NoGravity = false;
		CurrentAnimator.SetBool("Destroyed", true);
		CurrentRoom.gameObject.tag = "Untagged";
		ShipState.Inst.Init();
		for (int i = 0; i < CurrentRoom.Objects.Characters.Count; i++ ) 
		{
			CurrentRoom.Objects.Characters[i].Hurt(999f);
			CurrentRoom.Objects.Characters[i].Movement.AdjustPostion(new Vector3(-9f, -7f, 1f));
			MonoBehaviour.Destroy(CurrentRoom.Objects.Characters[i].GObject, 15f);
			foreach (SpriteRenderer sprite in CurrentRoom.Objects.Characters[i].GObject.GetComponentsInChildren<SpriteRenderer>())
			{
				sprite.sortingOrder = -5;
			}
			CurrentRoom.Objects.Untrack(CurrentRoom.Objects.Characters[i]);
		}
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!(CurrentRoom.Stats.Durability <= 0f)) CurrentRoom.Stats.Durability = 0f;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
	}
}
