using UnityEngine;
using System.Collections;

public class Slowpoke : Trait {
	
	public Slowpoke (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Slowpoke;
		stats.WalkSpeed *= 0.7f;
		stats.RunSpeed *= 0.7f;
	}
}