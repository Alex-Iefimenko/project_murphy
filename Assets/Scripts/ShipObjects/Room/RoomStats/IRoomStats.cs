using UnityEngine;
using System.Collections;

public interface IRoomStats {

	void Init (Enums.RoomTypes currentType);

	Enums.RoomTypes Type { get; } 

	// Duarbility 

	float Durability { get; }

	bool IsBroken { get; }

	bool IsRepaired { get; }

	bool IsDestroyed { get; }
	
	void Damage (float amount);
	
	void Repair (float amount);

	// Chemistry 

	bool IsHazardous { get; }

	float HazardLevel { get; }

	void SpreadHazard (float amount);

	void CleanHazard (float amount);

	// Fire

	bool IsOnFire { get; }

	float FireLevel { get; }

	void SpreadFire (float amount);
	
	void ReduceFire (float amount);

	// Radiation

	bool IsRadioactive { get; }

	float RadiationLevel { get; }

	void SpreadRadiation (float amount);
	
	void ReduceRadiation (float amount);

	// Depressuriazation

	bool IsDepressurized { get; }
	
	// Unelectrifyed

	bool IsUnelectryfied { get; set; }
	
	// Weather

	bool HasWeatherThreat { get; set; }
	
	// Gravity

	bool HasNoGravity { get; set; } 

	// Plants 

	bool IsPlantMutated { get; }

	float PlantLevel { get; }
	
	void DamagePlants (float amount);

	void GrowPlants (float amount);

	// Overall

	bool IsDangerous { get; }

	bool Locked { get; set; }
	
}
