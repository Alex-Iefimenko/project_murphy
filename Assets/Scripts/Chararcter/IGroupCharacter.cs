using UnityEngine;
using System.Collections;

public interface IGroupCharacter : ICharacter {

	IndividualCoordinator Coordinator { get; }

	void MutateType (Enums.CharacterTypes newType);

}
