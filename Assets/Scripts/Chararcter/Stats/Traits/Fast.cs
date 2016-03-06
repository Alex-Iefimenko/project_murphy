using UnityEngine;
using System.Collections;

public class Fast : Trait {
	
	public Fast (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Fast;
		stats.WalkSpeed *= 1.3f;
		stats.RunSpeed *= 1.3f;
	}
}