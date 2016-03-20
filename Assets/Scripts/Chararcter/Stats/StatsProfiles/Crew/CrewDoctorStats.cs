using UnityEngine;
using System.Collections;

public class CrewDoctorStats : CrewStats {

	// Use this for initialization
	public override void Init()
	{
		base.Init();
		// Common
		Side 					= Enums.CharacterSides.Crew;
		Type 					= Enums.CharacterTypes.Doctor;
		WalkSpeed			 	= 0.5f;
		RunSpeed 				= 0.7f;
		BasicRoom 				= ShipState.Inst.specRooms[Enums.RoomTypes.MedBay];
		// Health
		MaxHealth 				= Random.Range(60, 80);
		Health 					= maxHealth;
		HealthIncrease 			= Random.Range(4, 6);
		HealthRegeneration 		= 0.0f;
		HealthReduction 		= 0.0f;
		HealthThreshold 		= Random.Range(15, 25);
		// Fatigue
		MaxFatigue 				= Random.Range(60, 80);
		Fatigue 				= maxFatigue;
		FatigueIncrease 		= Random.Range(4, 6);
		FatigueRegeneration		= 0.0f;
		FatigueReduction		= Random.Range(0.3f, 0.6f);
		FatigueThreshold		= Random.Range(15, 25);
		// Sanity
		MaxSanity				= Random.Range(60, 80);
		Sanity					= maxSanity;
		SanityIncrease			= Random.Range(4, 6);
		SanityRegeneration		= 0.0f;
		SanityReduction			= Random.Range(0.3f, 0.6f);
		SanityThreshold			= Random.Range(15, 25);
		// Character Activities
		HealOther				= 2.0f;
		// Hardware Activities
		Repair					= 1.0f;
		FireExtinguish			= 1.0f;
		CleanRadiation			= 1.0f;
		CleanChemistry			= 0.0f;
		// Attack
		Damage					= 5.0f;
		AttackRate				= 1.0f;	
		AbbleDistantAttack		= false;
		// Working and Resting
		RestProbability 		= 0.25f;
		WorkTasks 				= new string[] { "HealOther", "TakeWoundedBody" };
		// Traits
		ApplyCrewTraits (this);
	}
	
}
