using UnityEngine;
using System.Collections;
using E = Enums;

public abstract class CharatcerStatsAbstract : MonoBehaviour {
	//
	// Fields
	//
	// Basic
	[HideInInspector] public readonly float walkSpeed;
	[HideInInspector] public readonly float runSpeed;
	[HideInInspector] public readonly Room basicRoom;
	// Health
	[HideInInspector] public readonly float maxHealth;
	[HideInInspector] public readonly float health;
	[HideInInspector] public readonly float healthIncrease;
	[HideInInspector] public readonly float healthRegeneration;
	[HideInInspector] public readonly float healthReduction;
	[HideInInspector] public readonly float healthThreshold;
	// Fatigue
	[HideInInspector] public readonly float maxFatigue;
	[HideInInspector] public readonly float fatigue;
	[HideInInspector] public readonly float fatigueIncrease;
	[HideInInspector] public readonly float fatigueRegeneration;
	[HideInInspector] public readonly float fatigueReduction;
	[HideInInspector] public readonly float fatigueThreshold;
	// Sanity 
	[HideInInspector] public readonly float maxSanity;
	[HideInInspector] public readonly float sanity;
	[HideInInspector] public readonly float sanityIncrease;
	[HideInInspector] public readonly float sanityRegeneration;
	[HideInInspector] public readonly float sanityReduction;
	[HideInInspector] public readonly float sanityThreshold;
	// Fighting
	[HideInInspector] public readonly float damage;
	[HideInInspector] public readonly float attackRate;
	[HideInInspector] public readonly float attackCoolDown;
	[HideInInspector] public readonly float abbleDistantAttack;
	// Character Activities
	[HideInInspector] public readonly float healOther;
	// Hardware Activities
	[HideInInspector] public readonly float repair;
	[HideInInspector] public readonly float fireExtinguish;
	[HideInInspector] public readonly float cleanRadiation;
	[HideInInspector] public readonly float cleanChemistry;
	// Working and Resting
	[HideInInspector] public readonly float restProbability;
	[HideInInspector] public readonly string[] workTasks;
	// traits
	[HideInInspector] public readonly E.Traits traitOne;
	[HideInInspector] public readonly E.Traits traitTwo;

	//
	// Properties 
	//
	// Basic
	public virtual float WalkSpeed { get; set; }
	public virtual float RunSpeed { get; set; }
	public virtual Room BasicRoom { get; set; }
	// Health
	public virtual float MaxHealth { get; set; }
	public virtual float Health { get; set; }
	public virtual float HealthIncrease { get; set; }
	public virtual float HealthRegeneration { get; set; }
	public virtual float HealthReduction { get; set; }
	public virtual float HealthThreshold { get; set; }
	// Fatigue
	public virtual float MaxFatigue { get; set; }
	public virtual float Fatigue { get; set; }
	public virtual float FatigueIncrease { get; set; }
	public virtual float FatigueRegeneration { get; set; }
	public virtual float FatigueReduction { get; set; }
	public virtual float FatigueThreshold { get; set; }
	// Sanity 
	public virtual float MaxSanity { get; set; }
	public virtual float Sanity { get; set; }
	public virtual float SanityIncrease { get; set; }
	public virtual float SanityRegeneration { get; set; }
	public virtual float SanityReduction { get; set; }
	public virtual float SanityThreshold { get; set; }
	// Fighting
	public virtual float Damage { get; set; }
	public virtual float AttackRate { get; set; }
	public virtual float AttackCoolDown { get; set; }
	public virtual bool AbbleDistantAttack { get; set; }
	// Character Activities
	public virtual float HealOther { get; set; }
	// Hardware Activities
	public virtual float Repair { get; set; }
	public virtual float FireExtinguish { get; set; }
	public virtual float CleanRadiation { get; set; }
	public virtual float CleanChemistry { get; set; }
	// Working and Resting
	public virtual float RestProbability { get; set; }
	public virtual string[] WorkTasks { get; set; }
	// traits
	public virtual E.Traits TraitOne { get; set; }
	public virtual E.Traits TraitTwo { get; set; }

}
