using UnityEngine;
using System.Collections;

public class DefendState : StateBase {

	private int stateIndex = 15;
	private ICharacter enemy = null;
	private GameObject shot = null;

	public DefendState (CharacterMain character) : base(character) 
	{ 
		shot = Resources.Load ("DynamicPrefabs/Shots/Shot") as GameObject;
	}
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		ICharacter hostile = room.ContainsHostile(character);
		return (hostile != null && !hostile.Stats.IsDead() && !hostile.Stats.IsUnconscious());
	}

	public override void Actualize () 
	{ 
		base.Actualize ();
		character.View.SetCustomBool("AbleToShot", character.Stats.AbbleDistantAttack);
		enemy = character.Movement.CurrentRoom.ContainsHostile(character);
		if (character.Stats.AbbleDistantAttack)
		{
			NavigateTo(character.Movement.CurrentRoom);
			character.Stats.attackReady += DistantAttack;
		}
		else
		{
			NavigateTo(enemy);
			character.Stats.attackReady += CloseAttack;
		}

	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false 
		    && character.Stats.AbbleDistantAttack == false
		    && character.Movement.IsNearObject(enemy.GObject) == false)
			character.PurgeActions();
		if (enemy.Movement.CurrentRoom != character.Movement.CurrentRoom)
			character.PurgeActions();
		if (enemy.Stats.IsDead() || enemy.Stats.IsUnconscious())
			character.PurgeActions();
	}

	private void CloseAttack ()
	{
		if (character.Movement.IsNearObject(enemy.GObject))
		{
			character.View.SetSubState(1);
			enemy.Hurt(character.Stats.Damage);
			character.Stats.AttackCoolDown = character.Stats.AttackRate;
			character.View.SetSubState(0);
		}
	}

	private void DistantAttack ()
	{
		character.View.RotateTowards(enemy.GObject.transform.position);
		character.View.SetSubState(1);
		float t = Mathf.Atan2((enemy.GObject.transform.position.y - character.GObject.transform.position.y), 
		                      (enemy.GObject.transform.position.x - character.GObject.transform.position.x)) * Mathf.Rad2Deg;
		Quaternion rotat = Quaternion.Euler(0, 0, t);
		GameObject thisShot = GameObject.Instantiate(shot, character.GObject.transform.position, rotat) as GameObject;
		thisShot.GetComponent<Shot>().Target = enemy.GObject;
		enemy.Hurt(character.Stats.Damage);
		character.Stats.AttackCoolDown = character.Stats.AttackRate;
		character.View.SetSubState(0);
	}

}
