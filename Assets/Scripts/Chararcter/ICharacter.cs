using UnityEngine;

public interface ICharacter {

	// Character Components

	Enums.CharacterSides Side { get; }

	Enums.CharacterTypes Type { get; }
	
	CharacterStatsBase Stats { get; }

	ICharacterAIHandler AiHandler { get; }

	IMovement Movement { get; }

	ICharacterView View { get; }

	Coordinator Coordinator { get; set; }

	GameObject GObject { get; }

	bool Lock { get; set; }

	// System Character Actions

	void Init ();

	void Mutate (Enums.CharacterTypes newtype);

	void PurgeActions ();

	void Tick ();

	// Outside Character Facade

	void Navigate (Room room, bool full);

	void Heal (float amount);

	void Hurt (float amount);
}
