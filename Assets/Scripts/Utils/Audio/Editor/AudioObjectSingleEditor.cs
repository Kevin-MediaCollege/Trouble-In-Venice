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

using UnityEngine;
using UnityEditor;

namespace Snakybo.Audio
{
	/// <summary>
	/// Inspector for <see cref="AudioObjectSingle"/>.
	/// </summary>
	[CustomEditor(typeof(AudioObjectSingle))]
	[CanEditMultipleObjects]
	public class AudioObjectSingleEditor : Editor
	{
		private SerializedProperty prop_audioClip;
		private SerializedProperty prop_audioMixerGroup;

		private SerializedProperty prop_type;

		private SerializedProperty prop_volume;
		private SerializedProperty prop_pitch;
		private SerializedProperty prop_stereoPan;
		private SerializedProperty prop_spatialBlend;
		private SerializedProperty prop_reverbZoneMix;
		private SerializedProperty prop_loop;

		private SerializedProperty prop_dopplerLevel;
		private SerializedProperty prop_spread;
		private SerializedProperty prop_minDistance;
		private SerializedProperty prop_maxDistance;

		private SerializedProperty prop_bypassEffects;
		private SerializedProperty prop_bypassListenerEffects;
		private SerializedProperty prop_bypassReverbZones;

		protected void OnEnable()
		{
			prop_audioClip = serializedObject.FindProperty("audioClip");
			prop_audioMixerGroup = serializedObject.FindProperty("audioMixerGroup");

			prop_type = serializedObject.FindProperty("type");

			prop_volume = serializedObject.FindProperty("volume");
			prop_pitch = serializedObject.FindProperty("pitch");
			prop_stereoPan = serializedObject.FindProperty("stereoPan");
			prop_spatialBlend = serializedObject.FindProperty("spatialBlend");
			prop_reverbZoneMix = serializedObject.FindProperty("reverbZoneMix");
			prop_loop = serializedObject.FindProperty("loop");

			prop_dopplerLevel = serializedObject.FindProperty("dopplerLevel");
			prop_spread = serializedObject.FindProperty("spread");
			prop_minDistance = serializedObject.FindProperty("minDistance");
			prop_maxDistance = serializedObject.FindProperty("maxDistance");

			prop_bypassEffects = serializedObject.FindProperty("bypassEffects");
			prop_bypassListenerEffects = serializedObject.FindProperty("bypassListenerEffects");
			prop_bypassReverbZones = serializedObject.FindProperty("bypassReverbZones");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_audioClip, new GUIContent("Clip"));
			EditorGUILayout.PropertyField(prop_audioMixerGroup, new GUIContent("Mixer"));
			EditorGUILayout.PropertyField(prop_type);
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_volume);
			EditorGUILayout.PropertyField(prop_pitch);
			EditorGUILayout.PropertyField(prop_stereoPan);
			EditorGUILayout.PropertyField(prop_spatialBlend);
			EditorGUILayout.PropertyField(prop_reverbZoneMix);
			EditorGUILayout.PropertyField(prop_loop);
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("3D Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_dopplerLevel);
			EditorGUILayout.PropertyField(prop_spread);
			EditorGUILayout.PropertyField(prop_minDistance);
			EditorGUILayout.PropertyField(prop_maxDistance);
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Bypass Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_bypassEffects, new GUIContent("Effects"));
			EditorGUILayout.PropertyField(prop_bypassListenerEffects, new GUIContent("Listener Effects"));
			EditorGUILayout.PropertyField(prop_bypassReverbZones, new GUIContent("Reverb Zones"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}