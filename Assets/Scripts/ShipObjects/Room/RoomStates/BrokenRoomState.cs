using UnityEngine;
using System.Collections;

public class BrokenRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsBroken;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool destroyed = stats.IsDestroyed;
		bool repaired = stats.IsRepaired; 
		return repaired || destroyed;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsBroken) stats.Damage(amount);
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.Repair(999f);
		CurrentAnimator.SetFloat("Breaking", 201f);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
		CurrentAnimator.SetFloat("Breaking", stats.Durability);
	}
	
}
