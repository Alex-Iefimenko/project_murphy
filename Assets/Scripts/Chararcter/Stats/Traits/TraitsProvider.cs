using UnityEngine;
using System.Collections;

public static class TraitsProvider {
	
	public enum Traits { 
		Hungry, 
		Pessimist, 
		IronWill, 
		Puny, 
		Big, 
		SlowRecovery, 
		Depresive, 
		Bully, 
		Dweeb, 
		KungFu,
		Muff, 
		Lazy, 
		HardWorker, 
		Slowpoke, 
		Fast, 
		Psychopath, 
		Proffi, 
		Masochist 
	};

	private static System.Type[] crewTraits = new System.Type[18] 
	{
		typeof(Hungry), 
		typeof(Pessimist),
		typeof(IronWill),
		typeof(Puny),
		typeof(Big),
		typeof(SlowRecovery),
		typeof(Depresive),
		typeof(Bully),
		typeof(Dweeb),
		typeof(KungFu),
		typeof(Muff),
		typeof(Lazy),
		typeof(HardWorker),
		typeof(Slowpoke),
		typeof(Fast),
		typeof(Psychopath),
		typeof(Proffi),
		typeof(Masochist)
	};

	private static System.Type[] otherTraits = new System.Type[13] 
	{
		typeof(Pessimist),
		typeof(IronWill),
		typeof(Puny),
		typeof(Big),
		typeof(SlowRecovery),
		typeof(Depresive),
		typeof(Bully),
		typeof(Dweeb),
		typeof(KungFu),
		typeof(Muff),
		typeof(Slowpoke),
		typeof(Fast),
		typeof(Masochist)
	};


	public static void ProvideCrewTraits (CharatcerStatsAbstract stats)
	{
		ProvideTraits(stats, crewTraits);
	}

	public static void ProvideNonCrewTraits (CharatcerStatsAbstract stats)
	{
		ProvideTraits(stats, otherTraits);
	}

	private static void ProvideTraits (CharatcerStatsAbstract stats, System.Type[] traits)
	{
		System.Type traitType1 = null;
		System.Type traitType2 = null;
		while (traitType1 == traitType2)
		{
			traitType1 = Helpers.GetRandomArrayValue<System.Type>(traits);
			traitType2 = Helpers.GetRandomArrayValue<System.Type>(traits);
		}
		Trait trait1 = (Trait)System.Activator.CreateInstance(traitType1, new CharatcerStatsAbstract [] {stats});
		Trait trait2 = (Trait)System.Activator.CreateInstance(traitType2, new CharatcerStatsAbstract [] {stats});
		stats.TraitOne = trait1.Name;
		stats.TraitTwo = trait2.Name;
	}

}