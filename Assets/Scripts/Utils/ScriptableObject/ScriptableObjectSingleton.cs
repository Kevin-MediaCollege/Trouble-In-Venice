using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Base class for all scriptable objects which should be loadable.
	/// </summary>
	/// <typeparam name="T">The type of the scritpable object.</typeparam>
	public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
	{
		private static T instance;

		/// <summary>
		/// 
		/// </summary>
		protected static T Instance
		{
			get
			{
				if(instance == null)
				{
					instance = Load();
				}

				return instance;
			}
		}

#if UNITY_EDITOR
		/// <summary>
		/// Create an asset of the scriptable object.
		/// </summary>
		/// <param name="_title">The title of the save file planel window.</param>
		/// <param name="_defaultName">The default name of the asset.</param>
		/// <param name="_message">The message of the save file panel window.</param>
		protected static void CreateAsset(string _title, string _defaultName, string _message)
		{
			string path = UnityEditor.EditorUtility.SaveFilePanelInProject(_title, _defaultName, "asset", _message);

			if(!string.IsNullOrEmpty(path))
			{
				CreateAssetAt(path);
			}
		}

		/// <summary>
		/// Create an asset of the scriptable object at the specified path.
		/// </summary>
		/// <param name="_path">The path to save the asset.</param>
		protected static void CreateAssetAt(string _path)
		{
			T obj = CreateInstance<T>();

			UnityEditor.AssetDatabase.CreateAsset(obj, _path + ".asset");
			UnityEditor.AssetDatabase.SaveAssets();

			UnityEditor.Selection.activeObject = obj;
		}
#endif

		/// <summary>
		/// Load an instance of the scriptable object.
		/// </summary>
		/// <returns>An instance of the scriptable object.</returns>
		private static T Load()
		{
			string[] elements = typeof(T).ToString().Split('.');
			string typeName = elements[elements.Length - 1];

			T asset = Resources.Load(typeName, typeof(T)) as T;

			if(asset == null)
			{
				T[] allObjects = Resources.FindObjectsOfTypeAll<T>();

				if(allObjects.Length == 0)
				{
					throw new Exception("Unable to find resource of type: " + typeof(T));
				}

				asset = allObjects[0];
			}

			return asset;
		}
	}
}
