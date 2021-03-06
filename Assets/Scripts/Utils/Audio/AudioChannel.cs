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

namespace Snakybo.Audio
{
	/// <summary>
	/// An audio channel.
	/// </summary>
	public class AudioChannel : MonoBehaviour
	{
		/// <summary>
		/// The current <see cref="AudioObjectSingle"/> assigned to this channel.
		/// </summary>
		public AudioObjectSingle AudioObject { private set; get; }

		/// <summary>
		/// The volume of the channel.
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> is clamped to [0, 1].
		/// </remarks>
		public float Volume
		{
			set { audioSource.volume = Mathf.Clamp01(value); }
			get { return audioSource.volume; }
		}

		/// <summary>
		/// The pitch of the channel.
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> is clamped to [0, 1].
		/// </remarks>
		public float Pitch
		{
			set { audioSource.pitch = Mathf.Clamp01(value); }
			get { return audioSource.pitch; }
		}

		/// <summary>
		/// The stereo pan of the channel.
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> is clamped to [0, 1].
		/// </remarks>
		public float StereoPan
		{
			set { audioSource.panStereo = Mathf.Clamp01(value); }
			get { return audioSource.panStereo; }
		}

		/// <summary>
		/// The spatial blend of the channel.
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> is clamped to [0, 1].
		/// </remarks>
		public float SpatialBlend
		{
			set { audioSource.spatialBlend = Mathf.Clamp01(value); }
			get { return audioSource.spatialBlend; }
		}

		/// <summary>
		/// The reverb zone mix of the channel.
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> is clamped to [0, 1].
		/// </remarks>
		public float ReverbZoneMix
		{
			set { audioSource.reverbZoneMix = Mathf.Clamp(value, 0, 1.1f); }
			get { return audioSource.reverbZoneMix; }
		}

		/// <summary>
		/// Whether or not the channel is muted.
		/// </summary>
		public bool Mute
		{
			set { audioSource.mute = value; }
			get { return audioSource.mute; }
		}

		/// <summary>
		/// Whether or not the channel should loop the current <see cref="AudioObject"/>
		/// </summary>
		public bool Loop
		{
			set { audioSource.loop = value; }
			get { return audioSource.loop; }
		}

		/// <summary>
		/// Whether or not the channel is currently playing something.
		/// </summary>
		public bool IsPlaying
		{
			get { return audioSource.isPlaying && !paused; }
		}

		private AudioSource audioSource;
		private bool paused;

		protected void Awake()
		{
			audioSource = GetComponent<AudioSource>();
			audioSource.playOnAwake = false;
		}

		protected void Update()
		{
			if(!IsPlaying && AudioObject != null)
			{
				AudioObject = null;
			}
		}

		internal void Play(AudioObjectSingle _audioObject)
		{
			AudioObject = _audioObject;
			AudioObject.ApplySettings(audioSource);

			audioSource.Play();
		}

		/// <summary>
		/// Pause the channel.
		/// </summary>
		public void Pause()
		{
			audioSource.Pause();
			paused = true;
		}

		/// <summary>
		/// Resume the channel.
		/// </summary>
		public void UnPause()
		{
			audioSource.UnPause();
			paused = false;
		}

		/// <summary>
		/// Stop the channel.
		/// </summary>
		public void Stop()
		{
			audioSource.Stop();
		}
	}
}
