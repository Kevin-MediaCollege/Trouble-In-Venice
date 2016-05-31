using Snakybo.Audio;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Plays footstep audio if the player enters the attached <see cref="GridNode"/>.
	/// </summary>
	public class FootstepAudio : MonoBehaviour
	{
		[SerializeField] private new AudioObject audio;
		[SerializeField] private GridNode node;

		protected void OnEnable()
		{
			node.onEntityEnteredEvent += OnEntityEntered;
		}

		protected void OnDisable()
		{
			node.onEntityEnteredEvent -= OnEntityEntered;
		}

		protected void Reset()
		{
			node = GetComponent<GridNode>();
		}

		private void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				audio.Play();
			}
		}
	}
}
