using UnityEngine;
using System.Collections;

public class BrokenRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && CurrentRoom.Stats.IsBroken();
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		bool destroyed = CurrentRoom.Stats.Durability <= 0f;
		bool repaired = CurrentRoom.Stats.Durability >= 200f; 
		return repaired || destroyed;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!CurrentRoom.Stats.IsBroken()) CurrentRoom.Stats.Durability -= amount;
		return AutoEnable (); 
	}
	
	public override void StateDisable () 
	{ 
		base.StateDisable ();
		CurrentRoom.Stats.Durability = CurrentRoom.Stats.MaxDurability;
		CurrentAnimator.SetFloat("Breaking", 201f);
	}
	
	public override void Tick () 
	{ 
		if (DisableCondition ()) 
		{
			StateDisable (); 
			return;
		}
		CurrentAnimator.SetFloat("Breaking", CurrentRoom.Stats.Durability);
	}
	
}
