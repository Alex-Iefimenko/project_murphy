using UnityEngine;
using System.Collections;

public interface IMovable {

	GameObject GObject { get; }

	bool Lock { get; set; }

}
