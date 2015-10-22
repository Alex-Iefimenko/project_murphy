using UnityEngine;
using System.Collections;

public interface IState {

	void Actualize();

	void ExecuteStateActions();

	int StateKind { get; }
}
