using UnityEngine;
using System.Collections;

public class RoomStats : MonoBehaviour {

	private float maxDurability;
	public float durability;
	public float fireLevel;
	public float radiationLevel;
	public float chemistryLevel;
	public float plantsLevel;
	public bool unelectryfied;
	public bool weatherThreat;
	public bool noGravity;

	public float MaxDurability { get { return maxDurability; } set { maxDurability = value; } }
	public float Durability { get { return durability; } set { durability = value; } }
	public float FireLevel { get { return fireLevel; } set { fireLevel = value; } }
	public float RadiationLevel { get { return radiationLevel; } set { radiationLevel = value; } }
	public float ChemistryLevel { get { return chemistryLevel; } set { chemistryLevel = value; } }
	public float PlantsLevel { get { return plantsLevel; } set { plantsLevel = value; } }
	public bool Unelectryfied { get { return unelectryfied; } set { unelectryfied = value; } }
	public bool WeatherThreat { get { return weatherThreat; } set { weatherThreat = value; } }
	public bool NoGravity { get { return noGravity; } set { noGravity = value; } }

	public void Awake ()
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

	private void Update ()
	{
		durability = Mathf.Clamp(durability, -2f, maxDurability);
		fireLevel = Mathf.Clamp(fireLevel, -2f, 100f);
		radiationLevel = Mathf.Clamp(radiationLevel, -2f, 100f);
		chemistryLevel = Mathf.Clamp(chemistryLevel, -2f, 100f);
		plantsLevel = Mathf.Clamp(plantsLevel, -2f, 100f);
	}

	public bool IsBroken ()
	{
		return Durability < MaxDurability;
	}

	public bool IsDepressurized ()
	{
		return Durability < Durability;
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

	public bool IsDangerous ()
	{
		bool result = 
			IsOnFire() || 
			IsRadioactive() || 
			IsHazardous() || 
			IsPlantMutated() ||
			IsDepressurized() ||
			WeatherThreat;

		return result;
	}

}
