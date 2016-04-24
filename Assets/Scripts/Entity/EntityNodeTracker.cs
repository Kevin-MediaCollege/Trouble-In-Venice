using UnityEngine;

public class EntityNodeTracker : MonoBehaviour
{
	public GridNode CurrentNode
	{
		set
		{
			currentNode = value;
		}
		get
		{
			return currentNode;
		}
	}

	[SerializeField, HideInInspector] private GridNode currentNode;
}