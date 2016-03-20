using UnityEngine;
using System.Collections;

public class AiStateParams {

	public IMovement Movement { get; private set; }
	public ICharacterStatePrivate Stats { get; private set; }
	public IndividualCoordinator Coordinator { get; private set; }

	public AiStateParams (ICharacterStatePrivate newStats, IMovement newMovement, IndividualCoordinator coordinator)
	{
		Movement = newMovement;
		Stats = newStats;
		Coordinator = coordinator;
	}
}
