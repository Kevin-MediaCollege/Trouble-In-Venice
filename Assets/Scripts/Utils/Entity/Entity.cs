using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic entity
/// </summary>
[RequireComponent(typeof(LocalEvents))]
public class Entity : MonoBehaviour
{
	/// <summary>
	/// Get an enumerable of all registered entities.
	/// </summary>
	public static IEnumerable<Entity> All
	{
		get
		{
			return all;
		}
	}

	private static HashSet<Entity> all = new HashSet<Entity>();

	private LocalEvents events;

	/// <summary>
	/// The event dispactcher of this entity.
	/// </summary>
	public IEventDispatcher Events
	{
		get
		{
			if(events == null)
			{
				events = GetComponent<LocalEvents>();
			}

			return events;
		}
	}

	[SerializeField] private string[] startingTags;

	private HashSet<string> tags;

	#region Unity Callbacks
	protected void Awake()
	{
		tags = new HashSet<string>();

		// Add the starting tags
		foreach(string tag in startingTags)
		{
			tags.Add(tag);
		}
	}

	protected void OnEnable()
	{
		all.Add(this);

		GlobalEvents.Invoke(new EntityActivatedEvent(this));
	}

	protected void OnDisable()
	{
		all.Remove(this);

		GlobalEvents.Invoke(new EntityDeactivatedEvent(this));
	}
	#endregion

	/// <summary>
	/// Add a tag to the entity.
	/// </summary>
	/// <param name="tag">The new tag.</param>
	public void AddTag(string tag)
	{
		tags.Add(tag);
	}

	/// <summary>
	/// Remove a tag from the entity.
	/// </summary>
	/// <param name="tag">The tag to remove.</param>
	public void RemoveTag(string tag)
	{
		tags.Remove(tag);
	}

	/// <summary>
	/// Check if this entity has the specified tag.
	/// </summary>
	/// <param name="tag">The tag to check.</param>
	/// <returns>Whether or not this entity has the specified tag.</returns>
	public bool HasTag(string tag)
	{
		return tags.Contains(tag);
	}
}