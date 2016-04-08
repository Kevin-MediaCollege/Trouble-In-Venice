using System;
using System.Collections.Generic;

/// <summary>
/// The GameDependencies handler.
/// 
/// All created Dependencies are available here via <seealso cref="Dependencies.Get{T}"/>.
/// Dependencies get created the first time they are retrieved.
/// </summary>
public class Dependency
{
	private static Dictionary<Type, IDependency> dependencies;

	static Dependency()
	{
		dependencies = new Dictionary<Type, IDependency>();
	}

	/// <summary>
	/// Attempt to retrieve a dependency.
	/// </summary>
	/// <typeparam name="T">The dependency to retrieve.</typeparam>
	/// <returns>The dependency of type <code>T</code>.</returns>
	public static T Get<T>() where T : IDependency, new()
	{
		return (T)Get(typeof(T));
	}

	/// <summary>
	/// Attempt to retrieve a dependency.
	/// </summary>
	/// <param name="type">The dependency to retrieve.</typeparam>
	/// <returns>The dependency of the specified type.</returns>
	public static IDependency Get(Type type)
	{
		if(dependencies.ContainsKey(type))
		{
			return dependencies[type];
		}

		IDependency dependency = Activator.CreateInstance(type) as IDependency;
		dependencies.Add(type, dependency);

		return dependency;
	}
}