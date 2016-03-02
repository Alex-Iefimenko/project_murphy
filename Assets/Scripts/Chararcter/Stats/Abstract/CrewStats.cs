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
	public new Enums.Traits traitOne;
	public new Enums.Traits traitTwo;

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
	public override Enums.Traits TraitOne { get { return traitOne; } }
	public override Enums.Traits TraitTwo { get { return traitTwo; } }

	public override void Init(CharacterMain character)
	{
		base.Init(character);
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
		fatigue = fatigue + fatigueRegeneration - fatigueReduction;
		sanity = sanity + sanityRegeneration - sanityReduction;
		fatigue = Mathf.Clamp(Fatigue, -10f, MaxFatigue);
		sanity = Mathf.Clamp(Sanity, -10f, MaxSanity);
	}

}
