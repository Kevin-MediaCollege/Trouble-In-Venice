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
using UnityEngine.Audio;

namespace Snakybo.Audio
{
	public class AudioObjectSingle : AudioObject
	{
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

		internal void ApplySettings(AudioSource audioSource)
		{
			audioSource.clip = audioClip;
			audioSource.outputAudioMixerGroup = audioMixerGroup;

			audioSource.volume = volume;
			audioSource.pitch = pitch;
			audioSource.panStereo = stereoPan;
			audioSource.spatialBlend = spatialBlend;
			audioSource.reverbZoneMix = reverbZoneMix;
			audioSource.loop = loop;

			audioSource.dopplerLevel = dopplerLevel;
			audioSource.spread = spread;
			audioSource.minDistance = minDistance;
			audioSource.maxDistance = maxDistance;
			
			audioSource.bypassEffects = bypassEffects;
			audioSource.bypassListenerEffects = bypassListenerEffects;
			audioSource.bypassReverbZones = bypassReverbZones;
		}
	}
}
