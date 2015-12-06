using UnityEngine;
using System.Collections;
//using System.Linq;

public class NoGravityRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.NoGravity;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = CurrentRoom.Stats.Durability <= 0f; 
		bool repaired = CurrentRoom.Stats.Durability >= CurrentRoom.Stats.MaxDurability;
		return broken || repaired;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.WeatherThreat = false;
		CurrentAnimator.SetBool("NoGravity", true);
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.NoGravity) CurrentRoom.Stats.NoGravity = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.NoGravity = false;
		CurrentAnimator.SetBool("NoGravity", false);
	}
	
	public override void Tick () 
	{ 
		if (DisableCondition ()) 
		{
			StateDisable (); 
			return;
		}
	}
	
}
