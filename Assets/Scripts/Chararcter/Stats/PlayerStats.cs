using UnityEngine;
using System.Collections;

public class PlayerStats : CharacterStatsBase {

	// Hardware Activities
	public new float repair;
	public new float fireExtinguish;

	// Hardware Activities
	public override float Repair { get { return repair; } }
	public override float FireExtinguish { get { return fireExtinguish; } }

	public override void Init(CharacterStatsConstructor constructor)
	{
		base.Init(constructor);
		repair = constructor.Repair;
		fireExtinguish = constructor.FireExtinguish;
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
	}

}
