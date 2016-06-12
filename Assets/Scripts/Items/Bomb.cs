using UnityEngine;
using System.Collections;

public class Bomb : Item {

	
	public float roomDamage = 50f;
	public float characterDamage = 40f;
	public GameObject effect = null;
	public IRoom Room { get; set; }

	private int tick = 40;

	// Use this for initialization
	void Awake () 
	{
		Broadcaster.Instance.tickEvent += Tick;
	}
	
	// Update is called once per frame
	public void Tick () 
	{
		tick -= 1;
		if (tick == 0)
		{
			Vector3 place = transform.position;
			GameObject particle = null;
			if (effect != null) particle = GameObject.Instantiate(effect, place, Quaternion.identity) as GameObject;
			Room.Damage(roomDamage);
			Room.ForceState<FireRoomState>(roomDamage / 2f);
			for (int i = 0; i < Room.Characters.Count; i++) Room.Characters[i].Hurt(characterDamage);
			Destroy(gameObject);
			Destroy(particle, 2f);
		}
	}
}
