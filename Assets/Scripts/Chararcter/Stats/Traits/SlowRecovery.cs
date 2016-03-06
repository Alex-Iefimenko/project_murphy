using UnityEngine;
using System.Collections;

public class SlowRecovery : Trait {
	
	public SlowRecovery (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.SlowRecovery;
		stats.HealthIncrease *= 0.5f;
	}
}