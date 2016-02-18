using UnityEngine;
using System.Collections;
using System.Linq;

public class EventSource : MonoBehaviour {

	public EventBase[] easyEvents;
	public EventBase[] mediumEvents;
	public EventBase[] hardEvents;
	private EventBase[] allEvents;

	void Start () {
		Broadcaster.Instance.dayEvent += SelectEvent;
		allEvents = new EventBase[easyEvents.Length + mediumEvents.Length + hardEvents.Length];
		easyEvents.CopyTo(allEvents, 0);
		mediumEvents.CopyTo(allEvents, easyEvents.Length);
		hardEvents.CopyTo(allEvents, mediumEvents.Length);
	}
	
	public void SelectEvent () {
		float value = Random.value;
		if (value < 0.4) {
			 
		} else if (value < 0.7 && easyEvents.Length > 0) {
			Helpers.GetRandomArrayValue<EventBase>(easyEvents).StartEvent();
		} else if (value < 0.9 && mediumEvents.Length > 0) {
			Helpers.GetRandomArrayValue<EventBase>(mediumEvents).StartEvent();
		} else if (value < 1 && hardEvents.Length > 0) {
			Helpers.GetRandomArrayValue<EventBase>(hardEvents).StartEvent();
		}
	}

	public void ForceState<T> ()
	{
		allEvents.Single(v => typeof(T) == v.GetType()).StartEvent();
	}
}
