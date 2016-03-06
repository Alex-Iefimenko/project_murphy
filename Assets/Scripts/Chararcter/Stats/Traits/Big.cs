using UnityEngine;
using System.Collections;

public class Big : Trait {

	public Big (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Big;
		stats.MaxHealth *= 1.1f;
	}
}
