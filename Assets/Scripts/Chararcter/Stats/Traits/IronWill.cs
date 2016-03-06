using UnityEngine;
using System.Collections;

public class IronWill : Trait {
	
	public IronWill (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.IronWill;
		stats.MaxSanity *= 1.2f;
	}
}