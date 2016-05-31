using UnityEngine;
using System.Collections;

public class ChallengeBase : MonoBehaviour 
{
	public virtual bool getStar()
	{
		return false;
	}

	public virtual string getString()
	{
		return "NULL";
	}
}
