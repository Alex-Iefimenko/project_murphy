//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//
//public class ActionNotificator : MonoBehaviour {
//
//	private bool chatting;
//	private Movement movement;
//	private float timer;
//	private SpriteRenderer sRenderer;
//	private Dictionary<string, Sprite> actions = new Dictionary<string, Sprite> ();
//
//
//	// Use this for initialization
//	void Start () {
//		movement = GetComponentInParent<Movement>();
//		sRenderer = GetComponent<SpriteRenderer>();
//		foreach (Object s in Resources.LoadAll ("NPC/Actions/")) if (s is Sprite) actions.Add(s.name, s as Sprite);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		isChat();
//		if (timer > 0) timer -= Time.deltaTime;
//	}
//
//	// Notificator invokation mmethod
//	public void Notify (string action) 
//	{
//		if (actions.ContainsKey(action) && !chatting) sRenderer.sprite = actions[action];
//	}
//
//	// Chat notification start
//	public void isChat ()
//	{
//		if (chatting && timer <= 0f)
//		{
//			chatting = false;
//			timer = Random.Range(1f, 2f);
//		}
////		else if (!chatting && movement.currentRoom.npcCollidercs.Count > 1 && timer <= 0f)
//		{
//			chatting = true;
//			sRenderer.sprite = actions["Speak"];
//			timer = Random.Range(1f, 2f);
//		}
//	}
//}
