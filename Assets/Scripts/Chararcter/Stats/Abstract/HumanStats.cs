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
	public new TraitsProvider.Traits traitOne;
	public new TraitsProvider.Traits traitTwo;
	
	// Sanity 
	public override float MaxSanity { get { return maxSanity; } set { maxSanity = value; } }
	public override float Sanity { get { return sanity; } set { sanity = value; } }
	public override float SanityIncrease { get { return sanityIncrease; } set { sanityIncrease = value; } }
	public override float SanityRegeneration { get { return sanityRegeneration; } set { sanityRegeneration = value; } }
	public override float SanityReduction { get { return sanityReduction; } set { sanityReduction = value; } }
	public override float SanityThreshold { get { return sanityThreshold; } set { sanityThreshold = value; } }
	// Character Activities
	public override float HealOther { get { return healOther; } set { healOther = value; } }
	// traits
	public override TraitsProvider.Traits TraitOne { get { return traitOne; } set { traitOne = value; } }
	public override TraitsProvider.Traits TraitTwo { get { return traitTwo; } set { traitTwo = value; } }
	
	public override void Init()
	{
		base.Init();
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
		Sanity = Sanity + SanityRegeneration - SanityReduction;
		Sanity = Mathf.Clamp(Sanity, -10f, MaxSanity);
	}

}
