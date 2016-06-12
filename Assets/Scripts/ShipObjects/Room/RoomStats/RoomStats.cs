using UnityEngine;
using System.Collections;

public class RoomStats : MonoBehaviour, IRoomStats {

	public Enums.RoomTypes type;
	public float maxDurability;
	public float durability;
	public float fireLevel;
	public float radiationLevel;
	public float chemistryLevel;
	public float plantsLevel;
	public bool unelectryfied;
	public bool weatherThreat;
	public bool noGravity;
	public bool locked;

	public Enums.RoomTypes Type { get { return type; } private set { type = value; } }
	public bool Locked { get { return locked; } set { locked = value; } }

	public void Init (Enums.RoomTypes currentType)
	{
		Type = currentType;
		maxDurability = 200f;
		durability = maxDurability;
		fireLevel = 0f;
		radiationLevel = 0f;
		chemistryLevel = 0f;
		plantsLevel = 0f;
		unelectryfied = false; 
		weatherThreat = false; 
		noGravity = false;
	}

	private void Update ()
	{
		durability = Mathf.Clamp(durability, -2f, maxDurability);
		fireLevel = Mathf.Clamp(fireLevel, -2f, 100f);
		radiationLevel = Mathf.Clamp(radiationLevel, -2f, 100f);
		chemistryLevel = Mathf.Clamp(chemistryLevel, -2f, 100f);
		plantsLevel = Mathf.Clamp(plantsLevel, -2f, 100f);
	}

	// Durability

	public float Durability { get { return durability; } }

	public bool IsBroken { get { return durability < maxDurability; } }

	public bool IsRepaired { get { return !IsBroken; } }

	public bool IsDestroyed { get { return durability <= 0f; } }

	public void Damage (float amount) 
	{
		durability -= amount;
	}

	public void Repair (float amount)
	{
		durability += amount;
	}

	// Chemistry

	public bool IsHazardous { get { return chemistryLevel > 0f; } }

	public float HazardLevel { get { return chemistryLevel; } }

	public void SpreadHazard (float amount)
	{
		chemistryLevel += amount;
	}

	public void CleanHazard (float amount)
	{
		chemistryLevel -= amount;
	}

	// Fire
	
	public bool IsOnFire { get { return fireLevel > 0; } }
	
	public float FireLevel { get { return fireLevel; } }
	
	public void SpreadFire (float amount)
	{
		fireLevel += amount;
	}
		
	public void ReduceFire (float amount)
	{
		fireLevel -= amount;
	}

	// Radiation
	
	public bool IsRadioactive { get { return radiationLevel > 0f; } }

	public float RadiationLevel { get { return radiationLevel; } }
	
	public void SpreadRadiation (float amount)
	{
		radiationLevel += amount;
	}
		
	public void ReduceRadiation (float amount)
	{
		radiationLevel -= amount;
	}

	// Plants 
	
	public bool IsPlantMutated { get { return plantsLevel > 0f; } }
	
	public float PlantLevel { get { return plantsLevel; } }
		
	public void DamagePlants (float amount)
	{
		plantsLevel -= amount;
	}
	
	public void GrowPlants (float amount)
	{
		plantsLevel += amount;
	}

	// Depressuriazation
	
	public bool IsDepressurized { get { return durability <= 50f; } }

	// Unelectrifyed
	
	public bool IsUnelectryfied { get { return unelectryfied; } set { unelectryfied = value; } }

	// Weather
	
	public bool HasWeatherThreat { get { return weatherThreat; } set { weatherThreat = value; } }

	// Gravity
	
	public bool HasNoGravity { get { return noGravity; } set { noGravity = value; } }



	public bool IsDangerous
	{
		get {
			bool result = 
				IsOnFire || 
				IsRadioactive || 
				IsHazardous || 
				IsPlantMutated ||
				IsDepressurized ||
				HasWeatherThreat;

			return result;
		}
	}

}
