using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	// Movement Component
	[HideInInspector] public Movement movement;
	// Stats components
	[HideInInspector] public CharacterStats stats;
	// Stats components
	[HideInInspector] public CharacterTasks tasks;

	// Start
	void Start () {
		movement = GetComponent<Movement>();
		stats    = GetComponent<CharacterStats>();
		tasks    = GetComponent<CharacterTasks>();
	}


	// Update is called once per frame
	void Update () {
		ApplyAction ();
	}

	// Way to decide what Action take
	void ApplyAction ()
	{
		switch (tasks.currentTask)
		{
		case CharacterTasks.Tasks.Attack:
			if (stats.ableDistantAttack) Shot ();
			if (!stats.ableDistantAttack) Fight ();
			if (tasks.taskNPC.currentState == CharacterTasks.States.Unconscious || tasks.taskNPC.currentState == CharacterTasks.States.Dead) 
				tasks.ClearTaskAims ();
			break;
		}
	}
	
	// Range combat
	void Shot () 
	{
		if (stats.attackCoolDown <= 0f) 
		{
			stats.attackCoolDown = stats.attackRate;
			tasks.currentState = CharacterTasks.States.Shot;
			tasks.taskNPC.stats.Damage (stats.damage);
		}
	}
	
	// Close combat
	void Fight () 
	{
		if (stats.attackCoolDown <= 0f || movement.IsNearObject(tasks.taskNPC.gameObject))  
		{
			stats.attackCoolDown = stats.attackRate;
			tasks.currentState = CharacterTasks.States.Fight;
			tasks.taskNPC.stats.Damage (stats.damage);
		}
	}
}
