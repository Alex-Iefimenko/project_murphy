using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Proffi : Trait {

	private Dictionary<Enums.CharacterTypes, string> responsibilities = new Dictionary<Enums.CharacterTypes, string>()
	{
		{Enums.CharacterTypes.Doctor, "HealOther"},
		{Enums.CharacterTypes.Engineer, "Repair"},
		{Enums.CharacterTypes.Guard, "FireExtinguish"},
		{Enums.CharacterTypes.LifeSupportEngineer, "CleanChemistry"}
	};

	public Proffi (CharatcerStatsAbstract stats) : base(stats) 
	{
		Name = TraitsProvider.Traits.Proffi;
		PropertyInfo field = stats.GetType ().GetProperty (responsibilities[stats.Type]);
		float currentValue = (float)field.GetValue (stats, null);
		field.SetValue(stats, currentValue  * 1.3f, null);
	}
}

