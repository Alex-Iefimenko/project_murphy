using UnityEngine;
using System.Collections;

public class Depresive : Trait {
	
	public Depresive (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Depresive;
		stats.SanityIncrease *= 0.5f;
	}
}