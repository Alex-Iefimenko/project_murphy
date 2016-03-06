using UnityEngine;
using System.Collections;

public class HardWorker : Trait {
	
	public HardWorker (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.HardWorker;
		stats.RestProbability *= 0.7f;
	}
}