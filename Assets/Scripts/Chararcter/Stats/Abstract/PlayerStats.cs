using UnityEngine;
using System.Collections;

public class PlayerStats : CharacterStatsBase {

	// Hardware Activities
	public new float repair;
	public new float fireExtinguish;
	public new float cleanRadiation;
	public new float cleanChemistry;

	// Hardware Activities
	public override float Repair { get { return repair; } set { repair = value; } }
	public override float FireExtinguish { get { return fireExtinguish; } set { fireExtinguish = value; } }
	public override float CleanRadiation { get { return cleanRadiation; } set { cleanRadiation = value; } }
	public override float CleanChemistry { get { return cleanChemistry; } set { cleanChemistry = value; } }

	public override void Init()
	{
		base.Init();
	}

	public override void StatsUpdate()
	{
		base.StatsUpdate();
	}

}
