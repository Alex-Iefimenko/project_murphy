using UnityEngine;
using System.Collections;

public class Hungry : Trait {
	
	public Hungry (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Hungry;
		stats.FatigueReduction *= 2;
	}
}
