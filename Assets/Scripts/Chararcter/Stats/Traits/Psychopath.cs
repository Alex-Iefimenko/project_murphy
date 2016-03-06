using UnityEngine;
using System.Collections;

public class Psychopath : Trait {
	
	public Psychopath (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Psychopath;
	}
}