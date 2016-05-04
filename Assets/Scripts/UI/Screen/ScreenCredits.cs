using UnityEngine;
using System.Collections;

public class ScreenCredits : ScreenBase 
{
	public override void OnScreenEnter()
	{
	}

	public override IEnumerator OnScreenFadeout()
	{
		yield break;
	}

	public override void OnScreenExit()
	{
	}

	public override string GetScreenName()
	{
		return "ScreenCredits";
	}
}