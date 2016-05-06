using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Utils
{
	/// <summary>
	/// A bunch of reflection utilities.
	/// </summary>
	public static class Reflection
	{
		/// <summary>
		/// Get all types which derive from T.
		/// </summary>
		/// <typeparam name="T">The base type</typeparam>
		/// <param name="_checkInterfaces">Whether or not to check interfaces too.</param>
		/// <returns>All types deriving from T.</returns>
		public static IEnumerable<Type> AllTypesFrom<T>(bool _checkInterfaces = false)
		{
			return AllTypesFrom(typeof(T), _checkInterfaces);
		}

		/// <summary>
		/// Get all types which derive from the specified type.
		/// </summary>
		/// <param name="_type">The base type.</param>
		/// <param name="_checkInterfaces">Whether or not to check interface too.</param>
		/// <returns>All types deriving from the specified type.</returns>
		public static IEnumerable<Type> AllTypesFrom(Type _type, bool _checkInterfaces = false)
		{
			Assembly projAssembly = Assembly.GetAssembly(typeof(Reflection));

			return projAssembly.GetTypes().Where(t =>
			{
				if(t.IsInterface || t.IsAbstract || t == _type)
				{
					return false;
				}

				if(_checkInterfaces)
				{
					foreach(Type interfaceType in t.GetInterfaces())
					{
						if(_type.IsAssignableFrom(interfaceType))
						{
							return true;
						}
					}
				}

				return _type.IsAssignableFrom(t);
			});
		}

		/// <summary>
		/// Get the names of all types which derive from T.
		/// </summary>
		/// <typeparam name="T">The base type.</typeparam>
		/// <param name="_checkInterfaces">Whether or not to check interfaces too.</param>
		/// <returns>The names of all types deriving from T.</returns>
		public static IEnumerable<string> AllTypeStringsFrom<T>(bool _checkInterfaces = false)
		{
			return AllTypeStringsFrom(typeof(T), _checkInterfaces);
		}

		/// <summary>
		/// Get the names of all types which derive from the specified type.
		/// </summary>
		/// <param name="_type">The base type.</param>
		/// <param name="_checkInterfaces">Whether or not to check interface too.</param>
		/// <returns>The names of all types deriving from the specified type.</returns>
		public static IEnumerable<string> AllTypeStringsFrom(Type _type, bool _checkInterfaces = false)
		{
			IEnumerable<Type> types = AllTypesFrom(_type, _checkInterfaces);
			List<string> result = new List<string>();

			foreach(Type t in types)
			{
				result.Add(t.FullName);
			}

			return result;
		}
	}
}
