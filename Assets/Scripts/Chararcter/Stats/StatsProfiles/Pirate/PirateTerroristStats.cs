using UnityEngine;
using System.Collections;

public class PirateTerroristStats : HumanStats {
	
	// Use this for initialization
	public override void Init(CharacterMain character)
	{
		base.Init(character);
		// Common
		walkSpeed			 	= 0.5f;
		runSpeed 				= 0.7f;
		basicRoom 				= ShipState.Inst.RoomByPoint(character.GObject.transform.position);
		// Health
		maxHealth 				= Random.Range(60, 80);
		health 					= maxHealth;
		healthIncrease 			= Random.Range(4, 6);
		healthRegeneration 		= 0.1f;
		healthReduction 		= 0.0f;
		healthThreshold 		= Random.Range(15, 25);
		// Sanity
		maxSanity				= Random.Range(60, 80);
		sanity					= maxSanity;
		sanityIncrease			= Random.Range(4, 6);
		sanityRegeneration		= 0.0f;
		sanityReduction			= 0.0f;
		sanityThreshold			= Random.Range(15, 25);
		// Character Activities
		healOther				= 0.0f;
		// Attack
		damage					= 4.0f;
		attackRate				= 1.0f;	
		abbleDistantAttack		= true;
	}
	
}
