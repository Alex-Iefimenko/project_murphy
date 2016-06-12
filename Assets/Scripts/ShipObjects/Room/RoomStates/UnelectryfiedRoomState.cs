using UnityEngine;
using System.Collections;
//using System.Linq;

public class UnelectryfiedRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.IsUnelectryfied;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
//		bool repaired = stats.Durability >= stats.MaxDurability;
		bool depress = stats.Durability <= 0f;
		return depress;// || depress;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.DamagePlants (999f);
		CurrentAnimator.SetBool("Unelectryfied", true);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!stats.IsUnelectryfied) stats.IsUnelectryfied = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		stats.IsUnelectryfied = false;
		CurrentAnimator.SetBool("Unelectryfied", false);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
	}
	
}
