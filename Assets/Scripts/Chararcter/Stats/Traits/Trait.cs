using UnityEngine;
using System.Collections;

public abstract class Trait {

	public TraitsProvider.Traits Name { get; protected set; }
	public string Description { get; protected set; }

	public Trait (CharatcerStatsAbstract stats) { }
}
