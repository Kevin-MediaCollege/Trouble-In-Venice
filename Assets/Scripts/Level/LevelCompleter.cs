using UnityEngine;
using System.Collections;

public class LevelCompleter : MonoBehaviour
{
	private LevelUnlocker levelUnlocker;
	private GridNode node;

	private int levelIndex;

	protected void Awake()
	{
		string[] levelName = transform.root.gameObject.name.Split('_');

		if(levelName.Length > 1)
		{
			levelIndex = int.Parse(levelName[levelName.Length - 1]);
			Debug.Log("Level index: " + levelIndex);
		}

		levelUnlocker = Dependency.Get<LevelUnlocker>();
		node = GetComponent<GridNode>();
	}

	protected void OnEnable()
	{
		node.onEntityEnteredEvent += OnEntityEntered;
	}

	protected void OnDisable()
	{
		node.onEntityEnteredEvent -= OnEntityEntered;
	}

	private void OnEntityEntered(Entity _entity)
	{
		if(_entity.HasTag("Player"))
		{
			int nextLevelIndex = levelIndex + 1;

			if(!levelUnlocker.IsUnlocked(nextLevelIndex))
			{
				Debug.Log("Unlocking level " + nextLevelIndex);
				levelUnlocker.Unlock(nextLevelIndex);
			}
			else
			{
				Debug.Log("Level " + nextLevelIndex + " has already been unlocked");
			}
		}
	}
}
