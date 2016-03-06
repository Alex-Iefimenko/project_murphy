using UnityEngine;
using System.Collections;

public class Dweeb : Trait {
	
	public Dweeb (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Dweeb;
		stats.Damage *= 0.5f;
	}
}