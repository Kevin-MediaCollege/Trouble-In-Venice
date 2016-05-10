// The MIT License(MIT)
//
// Copyright(c) 2016 Kevin Krol
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Snakybo.Audio
{
	/// <summary>
	/// Editor class to create <see cref="AudioObject"/>s.
	/// </summary>
	public static class AudioObjectCreator
	{
		[MenuItem("Audio/Create AudioObjects from AudioClips")]
		private static void CreateAudioAssets()
		{
			if(Selection.objects.Length > 0)
			{
				foreach(Object obj in Selection.objects)
				{
					if(obj.GetType() == typeof(AudioClip))
					{
						string path = AssetDatabase.GetAssetPath(obj);

						if(!string.IsNullOrEmpty(path))
						{
							path = Path.ChangeExtension(path, ".asset");

							SerializedObject asset = new SerializedObject(CreateAudioObjectSingleAtPath(path));

							asset.FindProperty("audioClip").objectReferenceValue = obj;
							asset.ApplyModifiedProperties();
							asset.Dispose();
						}
					}
				}

				AssetDatabase.SaveAssets();
			}
		}

		[MenuItem("Audio/Group AudioObjects")]
		private static void CreateAudioAssetGroups()
		{
			if(Selection.objects.Length > 0)
			{
				List<AudioObjectSingle> targets = new List<AudioObjectSingle>();

				foreach(Object obj in Selection.objects)
				{
					if(obj.GetType() == typeof(AudioObjectSingle))
					{
						targets.Add(obj as AudioObjectSingle);
					}
				}

				if(targets.Count > 0)
				{
					string path = AssetDatabase.GetAssetPath(targets[0]);

					if(!string.IsNullOrEmpty(path))
					{
						int lastIndex = path.LastIndexOf(Path.DirectorySeparatorChar);

						if(lastIndex <= 0)
						{
							lastIndex = path.LastIndexOf(Path.AltDirectorySeparatorChar);
						}

						if(lastIndex > 0)
						{
							path = path.Substring(0, lastIndex + 1);
							path += "New AudioObjectMultiple.asset";
						}
						else
						{
							path = EditorUtility.SaveFilePanelInProject("Create Audio Object Group", "New AudioObjectMultiple", "asset", "Create a new Audio Object Group");

							if(string.IsNullOrEmpty(path))
							{
								return;
							}
						}

						CreateAudioObjectMultipleAtPath(path, targets);
					}
				}
			}
		}

		private static AudioObjectSingle CreateAudioObjectSingleAtPath(string _path)
		{
			AudioObjectSingle asset = ScriptableObject.CreateInstance<AudioObjectSingle>();

			AssetDatabase.CreateAsset(asset, _path);
			AssetDatabase.SaveAssets();

			return asset;
		}

		private static AudioObjectMultiple CreateAudioObjectMultipleAtPath(string _path, List<AudioObjectSingle> _audioObjects = null)
		{
			AudioObjectMultiple asset = ScriptableObject.CreateInstance<AudioObjectMultiple>();

			AssetDatabase.CreateAsset(asset, _path);
			AssetDatabase.SaveAssets();

			if(_audioObjects != null && _audioObjects.Count > 0)
			{
				SerializedObject so = new SerializedObject(asset);
				SerializedProperty element = so.FindProperty("available");

				for(int i = 0; i < _audioObjects.Count; i++)
				{
					element.InsertArrayElementAtIndex(i);
					element.GetArrayElementAtIndex(i).objectReferenceValue = _audioObjects[i];
				}

				so.ApplyModifiedProperties();
			}

			Selection.activeObject = asset;
			return asset;
		}
	}
}