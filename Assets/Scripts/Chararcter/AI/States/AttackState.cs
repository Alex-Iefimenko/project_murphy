using UnityEngine;
using System.Collections;
using System.Linq;

public class AttackState : StateBase {
	
	private new int stateIndex = 3;
	private ICharacter enemy = null;
	private GameObject shot = null;

	public override int StateKind { get { return this.stateIndex; } }

	public AttackState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) 
	{ 
		GameObject[] shots = Resources.LoadAll ("DynamicPrefabs/Shots/", typeof(GameObject)).Cast<GameObject>().ToArray();
		shot = Helpers.GetRandomArrayValue<GameObject>(shots);
	}
	
	public override bool EnableCondition (Room room) 
	{
		enemy = room.Objects.ContainsHostile (stats.Side);
		return (enemy != null && enemy.IsActive);
	}

	public override void Actualize () 
	{ 		
		base.Actualize ();
		OnSetCustomBool("AbleToShot", stats.AbbleDistantAttack);
		if (stats.AbbleDistantAttack)
		{
			movement.Run().ToFurniture(movement.CurrentRoom, "Random");
			stats.attackReady += DistantAttack;
		}
		else
		{
			movement.Run().ToCharacter(enemy);
			stats.attackReady += CloseAttack;
		}
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false 
		    && stats.AbbleDistantAttack == false
		    && movement.IsNearObject(enemy.GObject) == false)
			movement.Run().ToCharacter(enemy);
		if (movement.CurrentRoom != movement.CurrentRoom)
			movement.Run().ToCharacter(enemy);
	}

	public override bool DisableCondition () 
	{
		return !enemy.IsActive;
	}

	private void CloseAttack ()
	{
		if (movement.IsNearObject(enemy.GObject))
		{
			OnSubStateChange(1);
			enemy.Hurt(stats.Damage);
			stats.StartAttackPrepare ();
		}
	}
	
	private void DistantAttack ()
	{
		movement.LookAt(enemy.GObject.transform.position);
		OnSubStateChange(1);
		float t = Mathf.Atan2((enemy.GObject.transform.position.y - movement.GObject.transform.position.y), 
		                      (enemy.GObject.transform.position.x - movement.GObject.transform.position.x)) * Mathf.Rad2Deg;
		Quaternion rotat = Quaternion.Euler(0, 0, t);
		GameObject thisShot = GameObject.Instantiate(shot, movement.GObject.transform.position, rotat) as GameObject;
		thisShot.GetComponent<Shot>().Target = enemy.GObject;
		enemy.Hurt(stats.Damage);
		stats.StartAttackPrepare ();
	}
	
}
