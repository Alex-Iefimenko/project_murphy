using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using System.Linq;
using System.Reflection;

public class CharacterStatsConstructor {

	Random rnd = null;
	// Type
	public string characterType;
	// Basic
	public float Speed { get; set; }
	public Room BasicRoom { get; set; }
	// Health
	public float MaxHealth { get; set; }
	public float Health { get; set; }
	public float HealthIncrease { get; set; }
	public float HealthRegeneration { get; set; }
	public float HealthReduction { get; set; }
	public float HealthThreshold { get; set; }
	// Fatigue
	public float MaxFatigue { get; set; }
	public float Fatigue { get; set; }
	public float FatigueIncrease { get; set; }
	public float FatigueRegeneration { get; set; }
	public float FatigueReduction { get; set; }
	public float FatigueThreshold { get; set; }
	// Sanity 
	public float MaxSanity { get; set; }
	public float Sanity { get; set; }
	public float SanityIncrease { get; set; }
	public float SanityRegeneration { get; set; }
	public float SanityReduction { get; set; }
	public float SanityThreshold { get; set; }
	// Fighting
	public float Damage { get; set; }
	public float AttackRate { get; set; }
	public bool AbbleDistantAttack { get; set; }
	// Character Activities
	public float HealOther { get; set; }
	// Hardware Activities
	public float Repair { get; set; }
	public float FireExtinguish { get; set; }
	public float CleanRadiation { get; set; }
	public float CleanChemistry { get; set; }
	// Working andResting
	public float RestProbability { get; set; }
	public List<string> WorkTasks { get; set; }
	// traits
	public CharatcerStatsAbstract.Traits TraitOne { get; set; }
	public CharatcerStatsAbstract.Traits TraitTwo { get; set; }

	public CharacterStatsConstructor (JsonData json, string type)
	{
		rnd = new Random();
		characterType = type;
		System.Collections.Generic.ICollection<string> keys = json.Keys;
		ConstructBasic(json);
		ConstructHealth(json["Health"]);
		ConstructFighting(json["Fighting"]);
		if (keys.Contains("Fatigue")) ConstructFatigue(json["Fatigue"]);
		if (keys.Contains("Sanity")) ConstructSanity(json["Sanity"]);
		if (keys.Contains("CharacterActivities")) ConstructCharacterActivities(json["CharacterActivities"]);
		if (keys.Contains("HardwareActivities")) ConstructHardwareActivities(json["HardwareActivities"]);
		if (keys.Contains("RestProbability")) ApplyRestProb(json);
		if (keys.Contains("Traits") && (bool) json["Traits"]) GenerateTraits();
	}

	private void ConstructBasic (JsonData json)
	{
		Speed = Convert.ToSingle((double) json["Speed"]);
		Room.RoomTypes roomEnum = (Room.RoomTypes)Enum.Parse(typeof(Room.RoomTypes), (string)json["BasicRoom"]);
		BasicRoom = ShipState.Inst.specRooms[roomEnum].GetComponent<Room>();
	}

	private void ConstructHealth (JsonData json)
	{
		MaxHealth = GetRandomFloatBetween(json["Max"]);
		Health = MaxHealth;
		HealthIncrease = GetRandomFloatBetween(json["Increase"]);
		HealthRegeneration = Convert.ToSingle((double) json["Regeneration"]);
		HealthReduction = Convert.ToSingle((double) json["Reduction"]);
		HealthThreshold = GetRandomFloatBetween(json["Treshhold"]);
	}

	private void ConstructFighting (JsonData json)
	{
		Damage = Convert.ToSingle((double) json["Damage"]);
		AttackRate = Convert.ToSingle((double) json["AttackRate"]);
		AbbleDistantAttack = (bool) json["AbbleDistantAttack"];
	}

	private void ConstructFatigue (JsonData json)
	{
		MaxFatigue = GetRandomFloatBetween(json["Max"]);
		Fatigue = MaxFatigue;
		FatigueIncrease = GetRandomFloatBetween(json["Increase"]);
		FatigueRegeneration = Convert.ToSingle((double) json["Regeneration"]);
		FatigueReduction = GetRandomFloatBetween(json["Reduction"]);
		FatigueThreshold = GetRandomFloatBetween(json["Treshhold"]);
	}

	private void ConstructSanity (JsonData json)
	{
		MaxSanity = GetRandomFloatBetween(json["Max"]);
		Sanity = MaxSanity;
		SanityIncrease = GetRandomFloatBetween(json["Increase"]);
		SanityRegeneration = Convert.ToSingle((double) json["Regeneration"]);
		SanityReduction = GetRandomFloatBetween(json["Reduction"]);
		SanityThreshold = GetRandomFloatBetween(json["Treshhold"]);
	}

	private void ConstructCharacterActivities (JsonData json)
	{
		HealOther = Convert.ToSingle((double) json["HealOther"]);
	}

	private void ConstructHardwareActivities (JsonData json)
	{
		Repair = Convert.ToSingle((double) json["Repair"]);
		FireExtinguish = Convert.ToSingle((double) json["FireExtinguish"]);
		CleanRadiation = Convert.ToSingle((double) json["CleanRadiation"]);
		CleanChemistry = Convert.ToSingle((double) json["CleanChemistry"]);
	}

	private void ApplyRestProb (JsonData json)
	{
		RestProbability = Convert.ToSingle((double) json["RestProbability"]);
		WorkTasks = new List<string> ();
		for (int i = 0; i < json["WorkTasks"].Count; i++) WorkTasks.Add((string)json["WorkTasks"][i]);
	}

	private void GenerateTraits ()
	{
		System.Array traitsList = System.Enum.GetValues (typeof(CharatcerStatsAbstract.Traits));
		List<CharatcerStatsAbstract.Traits> traitsValues = 
			new List<CharatcerStatsAbstract.Traits>(traitsList.Cast<CharatcerStatsAbstract.Traits>());
		TraitOne = Helpers.GetRandomArrayValue<CharatcerStatsAbstract.Traits>(traitsValues);
		traitsValues.Remove (TraitOne);
		TraitTwo = Helpers.GetRandomArrayValue<CharatcerStatsAbstract.Traits>(traitsValues);
		List<CharatcerStatsAbstract.Traits> activeTraits = new List<CharatcerStatsAbstract.Traits>() {TraitOne, TraitTwo};  
		UnityEngine.TextAsset json = UnityEngine.Resources.Load("Characters/Data/Stats/Traits") as UnityEngine.TextAsset;
		JsonData traitsData = JsonMapper.ToObject(json.text);

		foreach (CharatcerStatsAbstract.Traits trait in activeTraits)
		{
			JsonData traitData = traitsData[trait.ToString()];
			if (!(bool)traitData["Specific"]) 
				UpdateProperty ((string)traitData["Stat"], Convert.ToSingle((double)traitData["Coefficient"]));
			else
				ApplySpecificTrait (trait, traitData);
		}
		Health = MaxHealth;
		Sanity = MaxSanity;
		Fatigue = MaxFatigue;
	}

	private void ApplySpecificTrait (CharatcerStatsAbstract.Traits trait, JsonData traitData)
	{
		switch (trait)
		{
			case CharatcerStatsAbstract.Traits.Proffi:
				ApplyProffi (traitData);
				break;
		}
	}

	private void ApplyProffi (JsonData traitData)
	{
		UpdateProperty ((string)traitData["Stat"][characterType], Convert.ToSingle((double)traitData["Coefficient"]));
	}

	private void UpdateProperty (string propertyName, float coefficient)
	{
		PropertyInfo field = this.GetType ().GetProperty (propertyName);
		float currentValue = (float)field.GetValue (this, null);
		field.SetValue(this, currentValue  * coefficient, null);
	}

	private float GetRandomFloatBetween(JsonData array)
	{
		double d = (double) array[0] + rnd.NextDouble() * ((double) array[1] - (double) array[0]);
		return Convert.ToSingle(Math.Round(d, 1));
	}

}
