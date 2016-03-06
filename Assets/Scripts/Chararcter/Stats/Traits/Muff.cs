using UnityEngine;
using System.Collections;

public class Muff : Trait {
	
	public Muff (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Muff;
		stats.AttackRate *= 1.3f;
	}
}