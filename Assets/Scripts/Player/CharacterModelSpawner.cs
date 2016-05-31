using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Spawns the model of the player and target character.
	/// </summary>
	public class CharacterModelSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject modelMale;
		[SerializeField] private GameObject modelFemale;

		protected void Awake()
		{
			CharacterInfo charInfo = Dependency.Get<CharacterInfo>();
			Entity entity = GetComponentInParent<Entity>();

			if(entity != null && entity.HasTag("Player"))
			{
				FindModel(charInfo.Player);
			}
			else
			{
				FindModel(charInfo.Target);
			}
		}

		private void FindModel(CharacterID characterId)
		{
			switch(characterId)
			{
			case CharacterID.Female:
				Spawn(modelFemale);
				break;
			case CharacterID.Male:
				Spawn(modelMale);
				break;
			}
		}

		private void Spawn(GameObject obj)
		{
			GameObject instance = Instantiate(obj) as GameObject;
			instance.transform.SetParent(transform, false);
		}
	}
}
