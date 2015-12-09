using UnityEngine;
using System.Collections;
//using System.Linq;

public class UnelectryfiedRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.Unelectryfied;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
//		bool repaired = CurrentRoom.Stats.Durability >= CurrentRoom.Stats.MaxDurability;
		bool depress = CurrentRoom.Stats.Durability <= 0f;
		return depress;// || depress;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		CurrentRoom.Stats.PlantsLevel = 0f;
		CurrentAnimator.SetBool("Unelectryfied", true);
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.Unelectryfied) CurrentRoom.Stats.Unelectryfied = true;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.Unelectryfied = false;
		CurrentAnimator.SetBool("Unelectryfied", false);
	}
	
	public override void Tick () 
	{ 
		base.Tick ();
	}
	
}
