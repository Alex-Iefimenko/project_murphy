using UnityEngine;
using System.Collections;

public class RoomStats {

	private float MaxDurability { get; set; }
	public float Durability { get; set; } 
	public float FireLevel { get; set; }
	public float RadiationLevel { get; set; }
	public float ChemistryLevel { get; set; }
	public float PlantsLevel { get; set; }
	public bool Unelectryfied { get; set; }
	public bool WeatherThreat { get; set; }
	public bool NoGravity { get; set; }

	public RoomStats ()
	{
		MaxDurability = 200f;
		Durability = MaxDurability;
		FireLevel = 0f;
		RadiationLevel = 0f;
		ChemistryLevel = 0f;
		PlantsLevel = 0f;
		Unelectryfied = false; 
		WeatherThreat = false; 
		NoGravity = false;
	}

	public bool IsBroken ()
	{
		return Durability < MaxDurability;
	}

	public bool IsOnFire ()
	{
		return FireLevel > 0f;
	}

	public bool IsRadioactive ()
	{
		return RadiationLevel > 0f;
	}

	public bool IsHazardous ()
	{
		return ChemistryLevel > 0f;
	}

	public bool IsPlantMutated ()
	{
		return PlantsLevel > 0f; 
	}

}
