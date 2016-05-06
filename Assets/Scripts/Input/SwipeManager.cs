using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class SwipeManager : MonoBehaviour
	{
		protected void Update()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			foreach(Touch touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began)
				{
					StartCoroutine(Track(touch.fingerId, new SwipeHandle()));
				}
			}
#else
			if(Input.GetMouseButtonDown(0))
			{
				StartCoroutine(Track(-1, new SwipeHandle()));
			}
#endif
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_id"></param>
		/// <returns></returns>
		private Vector2 GetPosition(int _id)
		{
			if(_id == -1)
			{
				return Input.mousePosition;
			}
			else
			{
				foreach(Touch touch in Input.touches)
				{
					if(touch.fingerId == _id)
					{
						return touch.position;
					}
				}
			}

			return Vector2.zero;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_id"></param>
		/// <returns></returns>
		private bool IsTouching(int _id)
		{
			if(_id == -1)
			{
				return Input.GetMouseButton(0);
			}
			else
			{
				foreach(Touch touch in Input.touches)
				{
					if(touch.fingerId == _id && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_handle"></param>
		/// <returns></returns>
		private IEnumerator Track(int _id, SwipeHandle _handle)
		{
			bool notified = false;

			while(IsTouching(_id))
			{
				_handle.AddPosition(GetPosition(_id));

				if(!notified)
				{
					GlobalEvents.Invoke(new SwipeBeganEvent(_handle));
					notified = true;
				}

				GlobalEvents.Invoke(new SwipeUpdateEvent(_handle));
				yield return null;
			}

			_handle.IsComplete = true;
			GlobalEvents.Invoke(new SwipeEndedEvent(_handle));
		}
	}
}
