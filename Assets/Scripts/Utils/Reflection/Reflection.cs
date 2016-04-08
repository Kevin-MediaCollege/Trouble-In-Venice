using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

/// <summary>
/// A bunch of reflection utilities.
/// </summary>
public static class Reflection
{
	/// <summary>
	/// Get all types which derive from T.
	/// </summary>
	/// <typeparam name="T">The base type</typeparam>
	/// <param name="checkInterfaces">Whether or not to check interfaces too.</param>
	/// <returns>All types deriving from T.</returns>
	public static IEnumerable<Type> AllTypesFrom<T>(bool checkInterfaces = false)
	{
		return AllTypesFrom(typeof(T), checkInterfaces);
	}

	/// <summary>
	/// Get all types which derive from the specified type.
	/// </summary>
	/// <param name="type">The base type.</param>
	/// <param name="checkInterfaces">Whether or not to check interface too.</param>
	/// <returns>All types deriving from the specified type.</returns>
	public static IEnumerable<Type> AllTypesFrom(Type type, bool checkInterfaces = false)
	{
		Assembly projAssembly = Assembly.GetAssembly(typeof(Reflection));

		return projAssembly.GetTypes().Where(t =>
		{
			if(t.IsInterface || t.IsAbstract || t == type)
			{
				return false;
			}

			if(checkInterfaces)
			{
				foreach(Type interfaceType in t.GetInterfaces())
				{
					if(type.IsAssignableFrom(interfaceType))
					{
						return true;
					}
				}
			}

			return type.IsAssignableFrom(t);
		});
	}

	/// <summary>
	/// Get the names of all types which derive from T.
	/// </summary>
	/// <typeparam name="T">The base type.</typeparam>
	/// <param name="checkInterfaces">Whether or not to check interfaces too.</param>
	/// <returns>The names of all types deriving from T.</returns>
	public static IEnumerable<string> AllTypeStringsFrom<T>(bool checkInterfaces = false)
	{
		return AllTypeStringsFrom(typeof(T), checkInterfaces);
	}

	/// <summary>
	/// Get the names of all types which derive from the specified type.
	/// </summary>
	/// <param name="type">The base type.</param>
	/// <param name="checkInterfaces">Whether or not to check interface too.</param>
	/// <returns>The names of all types deriving from the specified type.</returns>
	public static IEnumerable<string> AllTypeStringsFrom(Type type, bool checkInterfaces = false)
	{
		IEnumerable<Type> types = AllTypesFrom(type, checkInterfaces);
		List<string> result = new List<string>();

		foreach(Type t in types)
		{
			result.Add(t.FullName);
		}

		return result;
	}
}