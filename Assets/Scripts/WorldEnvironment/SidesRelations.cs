using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sides = Enums.CharacterSides;

public class SidesRelations {

	private static SidesRelations instance;
	private static readonly object locker = new object();
	private Dictionary<Sides, string> sideStatTyping;
	private Dictionary<Sides, Sides[]> sideEnemies;

	private SidesRelations()
	{
		sideStatTyping = new Dictionary<Enums.CharacterSides, string> ()
		{
			{ Sides.Crew,     "CrewStats" },
			{ Sides.Pirate,   "HumanStats" },
			{ Sides.Trader,   "HumanStats" },
			{ Sides.Creature, "CreatureStats" }, 
			{ Sides.Player,   "PlayerStats" }
		}; 

		sideEnemies = new Dictionary<Sides, Sides[]> ()
		{
			{ Sides.Crew,     new Sides[] { Sides.Pirate, Sides.Creature } },
			{ Sides.Pirate,   new Sides[] { Sides.Crew, Sides.Trader, Sides.Creature, Sides.Player } },
			{ Sides.Trader,   new Sides[] { Sides.Pirate, Sides.Creature } },
			{ Sides.Creature, new Sides[] { Sides.Crew, Sides.Trader, Sides.Pirate, Sides.Player } }, 
			{ Sides.Player,   new Sides[] { Sides.Pirate, Sides.Creature } }
		}; 
	}

	public static SidesRelations Instance
	{
		get
		{
			lock (locker)
			{
				if (instance == null) instance = new SidesRelations();
			}
			return instance;
		}
	}

	public string GetStatsType(Enums.CharacterSides side)
	{
		return sideStatTyping[side];
	}

	public bool IsEnemies(ICharacter characterOne, ICharacter characterTwo)
	{
		return sideEnemies[characterOne.Side].Contains(characterTwo.Side);
	}
}
