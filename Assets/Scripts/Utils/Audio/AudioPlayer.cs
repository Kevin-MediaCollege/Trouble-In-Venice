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
using System.Collections.Generic;
using System;

namespace Snakybo.Audio
{
	public static class AudioPlayer
	{
		private static class AudioManager
		{
			public const int NUM_CHANNELS = 64;

			internal static IEnumerable<AudioChannel> Channels
			{
				get
				{
					return channels;
				}
			}

			private static HashSet<AudioChannel> channels;

			static AudioManager()
			{
				channels = new HashSet<AudioChannel>();

				GameObject obj = new GameObject("Audio Manager");
				UnityEngine.Object.DontDestroyOnLoad(obj);

				for(int i = 0; i < NUM_CHANNELS; i++)
				{
					GameObject channel = new GameObject("AudioChannel " + i);
					channel.transform.SetParent(obj.transform);

					channel.AddComponent<AudioSource>();
					channels.Add(channel.AddComponent<AudioChannel>());
				}
			}

			internal static AudioChannel GetNext()
			{
				foreach(AudioChannel channel in channels)
				{
					if(!channel.IsPlaying)
					{
						return channel;
					}
				}

				return null;
			}

			internal static IEnumerable<AudioChannel> GetOfType(AudioType type)
			{
				HashSet<AudioChannel> result = new HashSet<AudioChannel>();

				foreach(AudioChannel channel in channels)
				{
					if(channel.IsPlaying && channel.AudioObject.Type == type)
					{
						result.Add(channel);
					}
				}

				return result;
			}
		}

		public static AudioChannel Play(this AudioObject audioObject)
		{
			if(audioObject == null)
			{
				throw new ArgumentException("AudioObject is null");
			}

			if(audioObject is AudioObjectSingle)
			{
				AudioChannel channel = AudioManager.GetNext();

				if(channel != null)
				{
					channel.Play(audioObject as AudioObjectSingle);
					return channel;
				}
				else
				{
					Debug.LogWarning("No free AudioChannels");
				}
			}
			else if(audioObject is AudioObjectMultiple)
			{
				AudioObjectMultiple group = audioObject as AudioObjectMultiple;
				AudioObjectSingle next = group.Next();

				if(next != null)
				{
					return next.Play();
				}
			}
			else
			{
				Debug.LogError("Unknown AudioObject type: " + audioObject.GetType());
			}

			return null;
		}

		public static void StopAll()
		{
			foreach(AudioChannel audioChannel in AudioManager.Channels)
			{
				audioChannel.Stop();
			}
		}

		public static void StopAll(AudioType type)
		{
			foreach(AudioChannel audioChannel in AudioManager.GetOfType(type))
			{
				audioChannel.Stop();
			}
		}

		public static void PauseAll()
		{
			foreach(AudioChannel audioChannel in AudioManager.Channels)
			{
				audioChannel.Pause();
			}
		}
		
		public static void PauseAll(AudioType type)
		{
			foreach(AudioChannel audioChannel in AudioManager.GetOfType(type))
			{
				audioChannel.Pause();
			}
		}

		public static void UnPauseAll()
		{
			foreach(AudioChannel audioChannel in AudioManager.Channels)
			{
				audioChannel.UnPause();
			}
		}

		public static void UnPauseAll(AudioType type)
		{
			foreach(AudioChannel audioChannel in AudioManager.GetOfType(type))
			{
				audioChannel.UnPause();
			}
		}
	}
}
