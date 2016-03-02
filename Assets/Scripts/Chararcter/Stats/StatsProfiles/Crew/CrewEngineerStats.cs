using UnityEngine;
using System.Collections;

public class CrewEngineerStats : CrewStats {
	
	// Use this for initialization
	public override void Init(CharacterMain character)
	{
		base.Init(character);
		// Common
		walkSpeed			 	= 0.5f;
		runSpeed 				= 0.7f;
		basicRoom 				= ShipState.Inst.specRooms[Enums.RoomTypes.Engineering];
		// Health
		maxHealth 				= Random.Range(60, 80);
		health 					= maxHealth;
		healthIncrease 			= Random.Range(4, 6);
		healthRegeneration 		= 0.0f;
		healthReduction 		= 0.0f;
		healthThreshold 		= Random.Range(15, 25);
		// Fatigue
		maxFatigue 				= Random.Range(60, 80);
		fatigue 				= maxFatigue;
		fatigueIncrease 		= Random.Range(4, 6);
		fatigueRegeneration		= 0.0f;
		fatigueReduction		= Random.Range(0.3f, 0.6f);
		fatigueThreshold		= Random.Range(15, 25);
		// Sanity
		maxSanity				= Random.Range(60, 80);
		sanity					= maxSanity;
		sanityIncrease			= Random.Range(4, 6);
		sanityRegeneration		= 0.0f;
		sanityReduction			= Random.Range(0.3f, 0.6f);
		sanityThreshold			= Random.Range(15, 25);
		// Character Activities
		healOther				= 0.0f;
		// Hardware Activities
		repair					= 3.0f;
		fireExtinguish			= 3.0f;
		cleanRadiation			= 1.0f;
		cleanChemistry			= 0.0f;
		// Attack
		damage					= 5.0f;
		attackRate				= 1.0f;	
		abbleDistantAttack		= false;
		// Working and Resting
		restProbability 		= 0.25f;
		workTasks 				= new string[] { "Extinguish", "Repair" };
		// Traits
		//		traitOne;
		//		traitTwo;	
	}
	
}
