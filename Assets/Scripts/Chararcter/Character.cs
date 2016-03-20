using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, IGroupCharacter {

	private ICharacterAIHandler aiHandler;
	private ICharacterStatePublic stats;
	private IMovement movement;
	private SpriteRenderer[] sprites;
	private IndividualCoordinator coordinator;

	public bool Lock { get; set; }
	
	public GameObject GObject { get { return gameObject; } }
	
	public void Init (ICharacterAIHandler newAiHandler, AiStateParams param)
	{
		aiHandler = newAiHandler;
		stats = param.Stats;
		movement = param.Movement;
		coordinator = param.Coordinator;
		int layer = Random.Range(100, 32766);
		sprites = System.Array.ConvertAll(GetComponentsInChildren(typeof(SpriteRenderer), true), v => (SpriteRenderer) v);
		ChangeLayer (layer);
	}
	
	public Enums.CharacterSides Side { get { return stats.Side; } }

	public Enums.CharacterTypes Type { get { return stats.Type; } }

	public IndividualCoordinator Coordinator { get { return coordinator; } }

	public Room CurrentRoom { get { return movement.CurrentRoom; } }

	public bool IsMoving { get { return movement.IsMoving; } }
	
	public bool IsHealthy { get { return stats.IsHealthy; } }

	public bool IsActive { get { return stats.IsActive; } }

	public bool IsWounded { get { return stats.IsWounded; } }

	public bool IsUnconscious { get { return stats.IsUnconscious; } }

	public bool IsDead { get { return stats.IsDead; } }

	public void Heal (float amount) { stats.Heal (amount); }

	public void Hurt (float amount) { stats.Hurt (amount); }

	public void Infect (float amount) { stats.Infect (amount); }
	
	public void Push (Vector3 point) { movement.AdjustPostion (point); }

	public void Navigate (Room room) 
	{
		NavigateState nav = aiHandler.GetState<NavigateState> ();
		nav.TargetRoom = room;
		aiHandler.ForceState (nav);
	} 

	public void Vanish () 
	{
		movement.CurrentRoom.Objects.Untrack(this);
		Destroy(GObject, 10f);
	} 

	public void ChangeLayer (int layer)
	{
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].sortingOrder = layer;
		}
	}

	public void MutateType (Enums.CharacterTypes newType)
	{
		stats.Type = newType;
		aiHandler.ChangeStateChain (CharacterFactory.CreateAIPriorities(stats.Side, stats.Type));
	}
}
