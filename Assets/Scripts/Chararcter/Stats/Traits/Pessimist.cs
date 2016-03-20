using UnityEngine;
using System.Collections;

public class Pessimist : Trait {
	
	public Pessimist (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Pessimist;
		stats.MaxSanity *= 0.8f;
		stats.Sanity = stats.MaxSanity;
	}
}
