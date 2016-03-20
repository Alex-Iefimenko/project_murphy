using UnityEngine;
using System.Collections;

public interface ICharacterStatePublic {
	// Basic
	Enums.CharacterSides Side { get; set; }
	Enums.CharacterTypes Type { get; set; }
	Room BasicRoom { get; }
	// Health
	bool IsHealthy { get; }
	bool IsActive { get; }
	bool IsWounded { get; }
	bool IsUnconscious { get; }
	bool IsDead { get; }
	void Heal (float amount);
	void Hurt (float amount);
	void Infect (float amount);

}
