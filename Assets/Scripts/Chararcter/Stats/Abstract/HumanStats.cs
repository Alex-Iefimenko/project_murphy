using UnityEngine;
using System.Collections;

public class HumanStats : CharacterStatsBase {
	
	// Sanity
	public new float maxSanity;
	public new float sanity;
	public new float sanityIncrease;
	public new float sanityRegeneration;
	public new float sanityReduction;
	public new float sanityThreshold;
	// Character Activities
	public new float healOther;
	// traits
	public new Enums.Traits traitOne;
	public new Enums.Traits traitTwo;
	
	// Sanity 
	public override float MaxSanity { get { return maxSanity; } }
	public override float Sanity { get { return sanity; } }
	public override float SanityIncrease { get { return sanityIncrease; } }
	public override float SanityRegeneration { get { return sanityRegeneration; } }
	public override float SanityReduction { get { return sanityReduction; } }
	public override float SanityThreshold { get { return sanityThreshold; } }
	// Character Activities
	public override float HealOther { get { return healOther; } }
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
		sanity = sanity + sanityRegeneration - sanityReduction;
		sanity = Mathf.Clamp(Sanity, -10f, MaxSanity);
	}

}
