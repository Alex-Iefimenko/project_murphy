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
	public new TraitsProvider.Traits traitOne;
	public new TraitsProvider.Traits traitTwo;

	// Fatigue
	public override float MaxFatigue { get { return maxFatigue; } set { maxFatigue = value; } }
	public override float Fatigue { get { return fatigue; } set { fatigue = value; } }
	public override float FatigueIncrease { get { return fatigueIncrease; } set { fatigueIncrease = value; } }
	public override float FatigueRegeneration { get { return fatigueRegeneration; } set { fatigueRegeneration = value; } }
	public override float FatigueReduction { get { return fatigueReduction; } set { fatigueReduction = value; } }
	public override float FatigueThreshold { get { return fatigueThreshold; } set { fatigueThreshold = value; } }
	// Sanity 
	public override float MaxSanity { get { return maxSanity; } set { maxSanity = value; } }
	public override float Sanity { get { return sanity; } set { sanity = value; } }
	public override float SanityIncrease { get { return sanityIncrease; } set { sanityIncrease = value; } }
	public override float SanityRegeneration { get { return sanityRegeneration; } set { sanityRegeneration = value; } }
	public override float SanityReduction { get { return sanityReduction; } set { sanityReduction = value; } }
	public override float SanityThreshold { get { return sanityThreshold; } set { sanityThreshold = value; } }
	// Character Activities
	public override float HealOther { get { return healOther; } set { healOther = value; } }
	// Hardware Activities
	public override float Repair { get { return repair; } set { repair = value; } }
	public override float FireExtinguish { get { return fireExtinguish; } set { fireExtinguish = value; } }
	public override float CleanRadiation { get { return cleanRadiation; } set { cleanRadiation = value; } }
	public override float CleanChemistry { get { return cleanChemistry; } set { cleanChemistry = value; } }
	// Working and Resting
	public override float RestProbability { get { return restProbability; } set { restProbability = value; } }
	public override string[] WorkTasks { get { return workTasks; } set { workTasks = value; } }
	// traits
	public override TraitsProvider.Traits TraitOne { get { return traitOne; } set { traitOne = value; } }
	public override TraitsProvider.Traits TraitTwo { get { return traitTwo; } set { traitTwo = value; } }

	public override void Init(CharacterMain character)
	{
		base.Init(character);
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
		Fatigue = Fatigue + FatigueRegeneration - FatigueReduction;
		Sanity = Sanity + SanityRegeneration - SanityReduction;
		Fatigue = Mathf.Clamp(Fatigue, -10f, MaxFatigue);
		Sanity = Mathf.Clamp(Sanity, -10f, MaxSanity);
	}

}
