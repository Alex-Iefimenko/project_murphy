
public interface ICharacter {

	CharacterMain.CharacterSides Side { get; }

	CharacterMain.CharacterTypes Type { get; }
	
	CharacterStatsBase Stats { get; }

	ICharacterAIHandler AiHandler { get; }

	IMovement Movement { get; }

	ICharacterView CharacterView { get; }

}
