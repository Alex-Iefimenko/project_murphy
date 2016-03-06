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

	void MuatateReaction (string currentReaction, string newReaction);

	void MutateType (Enums.CharacterTypes newType);

	void MutateSide (Enums.CharacterSides newSide);

	void MutateSideAndType (Enums.CharacterSides newSide, Enums.CharacterTypes newType);

	void MutateFully (Enums.CharacterSides newSide, Enums.CharacterTypes newType);

	void PurgeActions ();

	void Tick ();

	// Outside Character Facade

	void Navigate (Room room, bool full);

	void Heal (float amount);

	void Hurt (float amount);
}
