using UnityEngine;
using System.Collections;

public class Puny : Trait {
	
	public Puny (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Puny;
		stats.MaxHealth *= 0.8f;
	}
}