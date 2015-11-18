using UnityEngine;
using System.Collections;

public class CrewStats : CharacterStatsBase {

	// Fatigue
	public new float maxFatigue;
	public new float fatigue;
	public new float fatigueIncrease;
	public new float fatigueRegeneration;
	public new float fatigueReduction;
	public new float fatigueThreshold;
	// Sanity
	public new float maxSanity;
	public new float sanity;
	public new float sanityIncrease;
	public new float sanityRegeneration;
	public new float sanityReduction;
	public new float sanityThreshold;
	// Character Activities
	public new float healOther;
	// Hardware Activities
	public new float repair;
	public new float fireExtinguish;
	public new float cleanRadiation;
	public new float cleanChemistry;
	// Working and Resting
	public new float restProbability;
	public new string[] workTasks;
	// traits
	public new Traits traitOne;
	public new Traits traitTwo;

	// Fatigue
	public override float MaxFatigue { get { return maxFatigue; } }
	public override float Fatigue { get { return fatigue; } set { fatigue = value; } }
	public override float FatigueIncrease { get { return fatigueIncrease; } }
	public override float FatigueRegeneration { get { return fatigueRegeneration; } }
	public override float FatigueReduction { get { return fatigueReduction; } }
	public override float FatigueThreshold { get { return fatigueThreshold; } }
	// Sanity 
	public override float MaxSanity { get { return maxSanity; } }
	public override float Sanity { get { return sanity; } set { sanity = value; } }
	public override float SanityIncrease { get { return sanityIncrease; } }
	public override float SanityRegeneration { get { return sanityRegeneration; } }
	public override float SanityReduction { get { return sanityReduction; } }
	public override float SanityThreshold { get { return sanityThreshold; } }
	// Character Activities
	public override float HealOther { get { return healOther; } }
	// Hardware Activities
	public override float Repair { get { return repair; } }
	public override float FireExtinguish { get { return fireExtinguish; } }
	public override float CleanRadiation { get { return cleanRadiation; } }
	public override float CleanChemistry { get { return cleanChemistry; } }
	// Working and Resting
	public override float RestProbability { get { return restProbability; } }
	public override string[] WorkTasks { get { return workTasks; } }
	// traits
	public override Traits TraitOne { get { return traitOne; } }
	public override Traits TraitTwo { get { return traitTwo; } }

	public override void Init(CharacterStatsConstructor constructor)
	{
		base.Init(constructor);
		maxFatigue = constructor.MaxFatigue;
		fatigue = constructor.Fatigue;
		fatigueIncrease = constructor.FatigueIncrease;
		fatigueRegeneration = constructor.FatigueRegeneration;
		fatigueReduction = constructor.FatigueReduction;
		fatigueThreshold = constructor.FatigueThreshold;
		maxSanity = constructor.MaxSanity;
		sanity = constructor.Sanity;
		sanityIncrease = constructor.SanityIncrease;
		sanityRegeneration = constructor.SanityRegeneration;
		sanityReduction = constructor.SanityReduction;
		sanityThreshold = constructor.SanityThreshold;
		healOther = constructor.HealOther;
		repair = constructor.Repair;
		fireExtinguish = constructor.FireExtinguish;
		cleanRadiation = constructor.CleanRadiation;
		cleanChemistry = constructor.CleanChemistry;
		restProbability = constructor.RestProbability;
		workTasks = constructor.WorkTasks.ToArray();
		traitOne = constructor.TraitOne;
		traitTwo = constructor.TraitTwo;
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
		fatigue = fatigue + fatigueRegeneration - fatigueReduction;
		sanity = sanity + sanityRegeneration - sanityReduction;
	}

}
