using UnityEngine;
using System.Collections;

public delegate void AttackDelegate();

public interface ICharacterStatePrivate : ICharacterStatePublic {
	
	void Purge ();

	// Basic
	float WalkSpeed { get; }
	float RunSpeed { get; }
	// Health
	float Health { get; }
	float HealthIncrease { get; }
	bool IsHeavyWounded { get; }
	// Fatigue
	float Fatigue { get; }
	bool IsHungry { get; }
	bool IsFull { get; }
	void Eat ();
	// Sanity
	float Sanity { get; }
	bool IsExhaust { get; }
	bool IsRested { get; }
	void Sleep ();
	// Fighting
	float Damage { get; }
	bool AbbleDistantAttack { get; }
	void StartAttackPrepare ();
	event AttackDelegate attackReady;
	// Capabilities
	float HealOther { get; }
	float Repair { get; }
	float FireExtinguish { get; }
	float CleanRadiation { get; }
	float CleanChemistry { get; }
	float RestProbability { get; }
	string[] WorkTasks { get; }
	// traits
	TraitsProvider.Traits TraitOne { get; }
	TraitsProvider.Traits TraitTwo { get; }

}
