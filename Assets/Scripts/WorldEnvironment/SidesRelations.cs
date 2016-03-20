using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sides = Enums.CharacterSides;

public class SidesRelations {

	private static SidesRelations instance;
	private static readonly object locker = new object();
	private Dictionary<Sides, Sides[]> sideEnemies;
	private Dictionary<Sides, Sides[]> sideFriends;

	private SidesRelations()
	{
		sideEnemies = new Dictionary<Sides, Sides[]> ()
		{
			{ Sides.Crew,     new Sides[] { Sides.Pirate, Sides.Creature } },
			{ Sides.Pirate,   new Sides[] { Sides.Crew, Sides.Trader, Sides.Creature, Sides.Player } },
			{ Sides.Trader,   new Sides[] { Sides.Pirate, Sides.Creature } },
			{ Sides.Creature, new Sides[] { Sides.Crew, Sides.Trader, Sides.Pirate, Sides.Player } }, 
			{ Sides.Player,   new Sides[] { Sides.Pirate, Sides.Creature } }
		}; 

		sideFriends = new Dictionary<Sides, Sides[]> ()
		{
			{ Sides.Crew,     new Sides[] { Sides.Crew, Sides.Player } },
			{ Sides.Pirate,   new Sides[] { Sides.Pirate } },
			{ Sides.Trader,   new Sides[] { Sides.Trader } },
			{ Sides.Creature, new Sides[] { Sides.Creature } }, 
			{ Sides.Player,   new Sides[] { Sides.Player, Sides.Crew } }
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

	public bool IsEnemies(Sides sideOne, Sides sideTwo)
	{
		return sideEnemies[sideOne].Contains(sideTwo);
	}

	public bool IsFriends(Sides sideOne, Sides sideTwo)
	{
		return sideFriends[sideOne].Contains(sideTwo);
	}
}
