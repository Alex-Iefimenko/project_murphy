using UnityEngine;
using System.Collections;

public class PlayerMurphyStats : PlayerStats {

	// Use this for initialization
	public override void Init(CharacterMain character)
	{
		base.Init(character);
		// Common
		walkSpeed			 	= 0.75f;
		runSpeed 				= 0.75f;
		basicRoom 				= ShipState.Inst.specRooms[Enums.RoomTypes.Control];
		// Health
		maxHealth 				= Random.Range(70, 80);
		health 					= maxHealth;
		healthIncrease 			= Random.Range(4, 6);
		healthRegeneration 		= 0.0f;
		healthReduction 		= 0.0f;
		healthThreshold 		= Random.Range(15, 25);
		// Hardware Activities
		repair					= 1.0f;
		fireExtinguish			= 1.0f;
		cleanRadiation			= 1.0f;
		cleanChemistry			= 0.0f;
		// Attack
		damage					= 5.0f;
		attackRate				= 1.0f;	
		abbleDistantAttack		= true;
	}

}
