using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridNodeDebug : MonoBehaviour
{
	protected void Awake()
	{
#if UNITY_EDITOR
		if(!Application.isPlaying)
		{
			if(GetComponent<Collider>() == null)
			{
				BoxCollider bc = gameObject.AddComponent<BoxCollider>();
				bc.size = new Vector3(1, 0.25f, 1);
			}
		}
		else
		{
			Destroy(GetComponent<Collider>());
		}
#endif
	}
}