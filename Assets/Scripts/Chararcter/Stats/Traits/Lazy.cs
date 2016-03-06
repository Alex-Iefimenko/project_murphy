using UnityEngine;
using System.Collections;

public class Lazy : Trait {
	
	public Lazy (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Lazy;
		stats.RestProbability *= 1.3f;
	}
}