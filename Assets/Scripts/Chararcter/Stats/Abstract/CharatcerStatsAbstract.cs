using UnityEngine;
using System.Collections;
using E = Enums;

public abstract class CharatcerStatsAbstract : MonoBehaviour, ICharacterStatePrivate {
	//
	// Fields
	//
	// Basic
	[HideInInspector] [System.NonSerialized] public Enums.CharacterSides side;
	[HideInInspector] [System.NonSerialized] public Enums.CharacterTypes type;
	[HideInInspector] [System.NonSerialized] public float walkSpeed;
	[HideInInspector] [System.NonSerialized] public float runSpeed;
	[HideInInspector] [System.NonSerialized] public IRoom basicRoom;
	// Health
	[HideInInspector] [System.NonSerialized] public float maxHealth;
	[HideInInspector] [System.NonSerialized] public float health;
	[HideInInspector] [System.NonSerialized] public float healthIncrease;
	[HideInInspector] [System.NonSerialized] public float healthRegeneration;
	[HideInInspector] [System.NonSerialized] public float healthReduction;
	[HideInInspector] [System.NonSerialized] public float healthThreshold;
	// Fatigue
	[HideInInspector] [System.NonSerialized] public float maxFatigue;
	[HideInInspector] [System.NonSerialized] public float fatigue;
	[HideInInspector] [System.NonSerialized] public float fatigueIncrease;
	[HideInInspector] [System.NonSerialized] public float fatigueRegeneration;
	[HideInInspector] [System.NonSerialized] public float fatigueReduction;
	[HideInInspector] [System.NonSerialized] public float fatigueThreshold;
	// Sanity 
	[HideInInspector] [System.NonSerialized] public float maxSanity;
	[HideInInspector] [System.NonSerialized] public float sanity;
	[HideInInspector] [System.NonSerialized] public float sanityIncrease;
	[HideInInspector] [System.NonSerialized] public float sanityRegeneration;
	[HideInInspector] [System.NonSerialized] public float sanityReduction;
	[HideInInspector] [System.NonSerialized] public float sanityThreshold;
	// Fighting
	[HideInInspector] [System.NonSerialized] public float damage;
	[HideInInspector] [System.NonSerialized] public float attackRate;
	[HideInInspector] [System.NonSerialized] public float attackCoolDown;
	[HideInInspector] [System.NonSerialized] public float abbleDistantAttack;
	// Character Activities
	[HideInInspector] [System.NonSerialized] public float healOther;
	// Hardware Activities
	[HideInInspector] [System.NonSerialized] public float repair;
	[HideInInspector] [System.NonSerialized] public float fireExtinguish;
	[HideInInspector] [System.NonSerialized] public float cleanRadiation;
	[HideInInspector] [System.NonSerialized] public float cleanChemistry;
	// Working and Resting
	[HideInInspector] [System.NonSerialized] public float restProbability;
	[HideInInspector] [System.NonSerialized] public string[] workTasks;
	// traits
	[HideInInspector] [System.NonSerialized] public TraitsProvider.Traits traitOne;
	[HideInInspector] [System.NonSerialized] public TraitsProvider.Traits traitTwo;

	//
	// Properties 
	//
	// Basic
	public virtual Enums.CharacterSides Side { get; set; }
	public virtual Enums.CharacterTypes Type { get; set; }
	public virtual float WalkSpeed { get; set; }
	public virtual float RunSpeed { get; set; }
	public virtual IRoom BasicRoom { get; set; }
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
	public virtual TraitsProvider.Traits TraitOne { get; set; }
	public virtual TraitsProvider.Traits TraitTwo { get; set; }
	// Fighting
	public virtual float Damage { get; set; }
	public virtual float AttackRate { get; set; }
	public virtual float AttackCoolDown { get; set; }
	public virtual bool AbbleDistantAttack { get; set; }
	public event AttackDelegate attackReady;

	public void Update()
	{
		if (AttackCoolDown > 0f) 
		{
			AttackCoolDown -= Time.deltaTime;
		}
		else 
		{
			if (attackReady != null) attackReady();
		}
	}

	public virtual void Purge () 
	{
		attackReady = null;
	}

	public void ApplyCrewTraits (CharatcerStatsAbstract stats)
	{
		TraitsProvider.ProvideCrewTraits(stats);
	}

	public void ApplyNonCrewTraits (CharatcerStatsAbstract stats)
	{
		TraitsProvider.ProvideNonCrewTraits(stats);
	}

	//
	// Interface implementation
	//
	public bool IsHealthy { get { return Health >= MaxHealth; } }
	public bool IsDead { get { return Health <= 0f; } }
	public bool IsUnconscious { get { return Health <= 10f && !IsDead; } }
	public bool IsActive { get { return !IsDead && !IsUnconscious; } }
	public bool IsWounded { get { return Health < MaxHealth && IsActive; } }
	public bool IsHeavyWounded { get { return Health < HealthThreshold && IsActive; } }

	public void Hurt (float amount) { Health -= amount; }
	public void Infect (float amount) { HealthReduction += amount; }
	public void Heal (float amount) { Health += amount; HealthReduction -= amount; }

	public bool IsHungry { get { return Fatigue < FatigueThreshold; } }
	public bool IsFull { get {return Fatigue >= MaxFatigue; } }
	public void Eat () { Fatigue += FatigueIncrease; }

	public bool IsExhaust { get { return Sanity < SanityThreshold; } }
	public bool IsRested { get { return Sanity >= MaxSanity; } }
	public void Sleep () { Sanity += SanityIncrease; }

	public void StartAttackPrepare () { AttackCoolDown = AttackRate; }
}
