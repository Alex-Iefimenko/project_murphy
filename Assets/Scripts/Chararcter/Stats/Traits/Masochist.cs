using UnityEngine;
using System.Collections;

public class Masochist : Trait {
	
	public Masochist (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Masochist;
	}
}