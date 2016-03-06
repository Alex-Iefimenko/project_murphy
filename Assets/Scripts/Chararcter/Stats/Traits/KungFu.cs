using UnityEngine;
using System.Collections;

public class KungFu : Trait {
	
	public KungFu (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.KungFu;
		stats.AttackRate *= 0.7f;
	}
}
