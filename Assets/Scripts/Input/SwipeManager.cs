using UnityEngine;
using System.Collections;

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

	private Vector2 GetPosition(int id)
	{
		if(id == -1)
		{
			return Input.mousePosition;
		}
		else
		{
			foreach(Touch touch in Input.touches)
			{
				if(touch.fingerId == id)
				{
					return touch.position;
				}
			}
		}

		return Vector2.zero;
	}

	private bool IsTouching(int id)
	{
		if(id == -1)
		{
			return Input.GetMouseButton(0);
		}
		else
		{
			foreach(Touch touch in Input.touches)
			{
				if(touch.fingerId == id && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					return true;
				}
			}
		}

		return false;
	}

	private IEnumerator Track(int id, SwipeHandle handle)
	{
		bool notified = false;

		while(IsTouching(id))
		{
			handle.AddPosition(GetPosition(id));

			if(!notified)
			{
				GlobalEvents.Invoke(new SwipeBeganEvent(handle));
				notified = true;
			}

			GlobalEvents.Invoke(new SwipeUpdateEvent(handle));		
			yield return null;
		}

		handle.IsComplete = true;
		GlobalEvents.Invoke(new SwipeEndedEvent(handle));
	}
}