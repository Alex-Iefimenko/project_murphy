using System.Collections;
using System.Collections.Generic;

public class SidesRelations {

	private static SidesRelations instance;
	private static readonly object locker = new object();
	private Dictionary<CharacterMain.CharacterSides, string> sideStatTyping;

	private SidesRelations()
	{
		sideStatTyping = new Dictionary<CharacterMain.CharacterSides, string> ()
		{
			{ CharacterMain.CharacterSides.Crew,     "CrewStats" },
			{ CharacterMain.CharacterSides.Pirate,   "HumanStats" },
			{ CharacterMain.CharacterSides.Trader,   "HumanStats" },
			{ CharacterMain.CharacterSides.Creature, "CreatureStats" }, 
			{ CharacterMain.CharacterSides.Player,   "PlayerStats" }
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

	public string GetStatsType(CharacterMain.CharacterSides side)
	{
		return sideStatTyping[side];
	}

}
