using UnityEngine;
using System.Collections;

public class ReactionBase : MonoBehaviour, IReaction {

	public ReactionBase (CharacterMain character) 
	{

	}
	
	public virtual void ExecuteReaction()
	{

	}

	public void PurgeActions() 
	{

	}

	void NavigateTo(Room room, bool randObject)
	{

	}

	void NavigateTo(CharacterMain character)
	{
		
	}

	void NavigateTo(Furniture item)
	{
		
	}

}
