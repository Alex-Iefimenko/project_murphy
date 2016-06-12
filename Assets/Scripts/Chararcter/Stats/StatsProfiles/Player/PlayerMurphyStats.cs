using UnityEngine;
using System.Collections;

public class PlayerMurphyStats : PlayerStats {

	// Use this for initialization
	public override void Init()
	{
		base.Init();
		// Common
		Side 					= Enums.CharacterSides.Player;
		Type 					= Enums.CharacterTypes.Murphy;
		WalkSpeed			 	= 0.75f;
		RunSpeed 				= 0.75f;
		BasicRoom 				= Ship.Inst.GetRoom("Control");
		// Health
		MaxHealth 				= Random.Range(70, 80);
		Health 					= maxHealth;
		HealthIncrease 			= Random.Range(4, 6);
		HealthRegeneration 		= 0.0f;
		HealthReduction 		= 0.0f;
		HealthThreshold 		= Random.Range(15, 25);
		// Hardware Activities
		Repair					= 1.0f;
		FireExtinguish			= 1.0f;
		CleanRadiation			= 1.0f;
		CleanChemistry			= 0.0f;
		// Attack
		Damage					= 5.0f;
		AttackRate				= 1.0f;	
		AbbleDistantAttack		= true;
	}

}
