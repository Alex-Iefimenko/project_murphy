using UnityEngine;
using System.Collections;

public class Enums {

	// CHARACTERS
	public enum CharacterSides { 
		Crew, 
		Pirate, 
		Trader, 
		Creature, 
		Player, 
		Alien 
	};

	public enum CharacterTypes { 
		// Crew
		Engineer, 
		Guard, 
		Doctor, 
		Scientist, 
		LifeSupportEngineer, 
		// Player
		Murphy, 
		// Trader
		Merchant,
		// Pirate
		Terrorist
	};
	
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
	
	// ROOMS
	public enum RoomTypes { 
		Nothing, 
		MedBay, 
		Safety, 
		Engine, 
		Control, 
		PowerSource, 
		LifeSupport, 
		Engineering, 
		Dinnery, 
		LivingQuarters, 
		Disposal, 
		Science
	};
}
