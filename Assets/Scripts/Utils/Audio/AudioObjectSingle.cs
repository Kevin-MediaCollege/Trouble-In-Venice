﻿// The MIT License(MIT)
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
using UnityEngine.Audio;

namespace Snakybo.Audio
{
	/// <summary>
	/// A single <see cref="AudioObject"/>, contains most of the AudioClip properties.
	/// </summary>
	public class AudioObjectSingle : AudioObject
	{
		/// <summary>
		/// The type of the audio object.
		/// </summary>
		public AudioType Type
		{
			get
			{
				return type;
			}
		}

		[SerializeField] private AudioClip audioClip;
		[SerializeField] private AudioMixerGroup audioMixerGroup;

		[SerializeField] private AudioType type;

		[SerializeField, Range( 0, 1)] private float volume = 1;
		[SerializeField, Range(-3, 3)] private float pitch = 1;
		[SerializeField, Range(-1, 1)] private float stereoPan;
		[SerializeField, Range( 0, 1)] private float spatialBlend;
		[SerializeField, Range( 0, 1.1f)] private float reverbZoneMix = 1;
		[SerializeField] private bool loop;

		[SerializeField, Range(0, 5)] private float dopplerLevel = 1;
		[SerializeField, Range(0, 360)] private float spread;
		[SerializeField] private float minDistance = 1;
		[SerializeField] private float maxDistance = 500;

		[SerializeField] private bool bypassEffects;
		[SerializeField] private bool bypassListenerEffects;
		[SerializeField] private bool bypassReverbZones;

		internal void ApplySettings(AudioSource _audioSource)
		{
			_audioSource.clip = audioClip;
			_audioSource.outputAudioMixerGroup = audioMixerGroup;

			_audioSource.volume = volume;
			_audioSource.pitch = pitch;
			_audioSource.panStereo = stereoPan;
			_audioSource.spatialBlend = spatialBlend;
			_audioSource.reverbZoneMix = reverbZoneMix;
			_audioSource.loop = loop;

			_audioSource.dopplerLevel = dopplerLevel;
			_audioSource.spread = spread;
			_audioSource.minDistance = minDistance;
			_audioSource.maxDistance = maxDistance;
			
			_audioSource.bypassEffects = bypassEffects;
			_audioSource.bypassListenerEffects = bypassListenerEffects;
			_audioSource.bypassReverbZones = bypassReverbZones;
		}
	}
}
