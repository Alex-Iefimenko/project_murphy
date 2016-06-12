using UnityEngine;
using System.Collections;
//using System.Linq;

public class NoGravityRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.HasNoGravity;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool broken = stats.IsDestroyed; 
		bool repaired = stats.IsRepaired;
		return broken || repaired;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.HasWeatherThreat = false;
		CurrentAnimator.SetBool("NoGravity", true);
	}

	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.HasNoGravity) stats.HasNoGravity = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.HasNoGravity = false;
		CurrentAnimator.SetBool("NoGravity", false);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
	}
	
}
