using UnityEngine;
using System.Collections;

public class Bully : Trait {
	
	public Bully (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Bully;
		stats.Damage *= 1.5f;
	}
}