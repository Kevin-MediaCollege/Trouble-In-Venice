using UnityEngine;
using System.Collections;

public class ScreenBase : MonoBehaviour 
{
	public virtual void OnScreenEnter()
	{
	}

	public virtual IEnumerator OnScreenFadeout()
	{
		yield break;
	}

	public virtual void OnScreenExit()
	{
	}

	public virtual string GetScreenName()
	{
		return "NULL";
	}
}
