using UnityEngine;
using System.Collections;

public class PlayerStats : CharacterStatsBase {

	// Hardware Activities
	public new float repair;
	public new float fireExtinguish;
	public new float cleanRadiation;
	public new float cleanChemistry;

	// Hardware Activities
	public override float Repair { get { return repair; } }
	public override float FireExtinguish { get { return fireExtinguish; } }
	public override float CleanRadiation { get { return cleanRadiation; } }
	public override float CleanChemistry { get { return cleanChemistry; } }

	public override void Init(CharacterMain character)
	{
		base.Init(character);
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
	}

}
