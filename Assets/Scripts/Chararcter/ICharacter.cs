using UnityEngine;

public interface ICharacter : IMovable {
	
	void Init (ICharacterAIHandler newAiHandler, AiStateParams param);

	Enums.CharacterSides Side { get; }
	Enums.CharacterTypes Type { get; }

	IRoom CurrentRoom { get; }
	bool IsMoving { get; }

	bool IsHealthy { get; }
	bool IsActive { get; }
	bool IsWounded { get; }
	bool IsUnconscious { get; }
	bool IsDead { get; }
	void Heal (float amount);
	void Hurt (float amount);
	void Infect (float amount);

	void Navigate (IRoom room);
	void Push (Vector3 point);
	void Vanish ();
	void ChangeLayer (int layer);

}
