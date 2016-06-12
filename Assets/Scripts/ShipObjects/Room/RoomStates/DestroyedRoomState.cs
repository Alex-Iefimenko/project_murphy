using UnityEngine;
using System.Collections;

public class DestroyedRoomState : RoomStateBase {
	
	public override bool EnableCondition ()
	{ 
		bool result = !DisableCondition() && stats.Durability <= 0f;
		return result; 
	} 
	
	public override bool DisableCondition ()
	{ 
		return false;
	}
	
	public override void StateEnable () 
	{ 
		base.StateEnable ();
		stats.ReduceFire (999f);
		stats.ReduceRadiation (999f);
		stats.CleanHazard (999f);
		stats.DamagePlants (999f);
		stats.IsUnelectryfied = false;
		stats.HasWeatherThreat = false;
		stats.HasNoGravity = false;
		CurrentAnimator.SetBool("Destroyed", true);
		controller.GObject.tag = "Untagged";
		Ship.Inst.Init();
		for (int i = 0; i < objects.Characters.Count; i++ ) 
		{
			objects.Characters[i].Hurt(999f);
			objects.Characters[i].Push(new Vector3(-9f, -7f, 1f));
			MonoBehaviour.Destroy(objects.Characters[i].GObject, 15f);
			foreach (SpriteRenderer sprite in objects.Characters[i].GObject.GetComponentsInChildren<SpriteRenderer>())
			{
				sprite.sortingOrder = -5;
			}
			objects.Untrack(objects.Characters[i]);
		}
	}
	
	public override bool InitiatedEnable (float amount) 
	{ 
		if (!(stats.Durability <= 0f)) stats.Damage (999f);
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
