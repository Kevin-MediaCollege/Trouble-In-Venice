using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of entity utilities.
/// </summary>
public static class EntityUtils
{
	/// <summary>
	/// Get all entities with the specified tag.
	/// </summary>
	/// <param name="tag">The tag.</param>
	/// <returns>All entities with the specified tag.</returns>
	public static IEnumerable<Entity> GetEntitiesWithTag(string tag)
	{
		HashSet<Entity> result = new HashSet<Entity>();

		foreach(Entity entity in Entity.All)
		{
			if(entity.HasTag(tag))
			{
				result.Add(entity);
			}
		}

		return result;
	}

	/// <summary>
	/// Get the first entity with the specified tag.
	/// </summary>
	/// <param name="tag">The tag.</param>
	/// <returns>The first entity with the specified tag.</returns>
	public static Entity GetEntityWithTag(string tag)
	{
		foreach(Entity entity in Entity.All)
		{
			if(entity.HasTag(tag))
			{
				return entity;
			}
		}

		Debug.LogError("[EntityUtils] No entity with tag: " + tag + " found");
		return null;
	}
}