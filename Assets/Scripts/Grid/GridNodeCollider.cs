using UnityEngine;

[ExecuteInEditMode]
public class GridNodeCollider : MonoBehaviour
{
	protected void Awake()
	{
		if(GetComponent<Collider>() == null)
		{
			BoxCollider bc = gameObject.AddComponent<BoxCollider>();
			bc.size = new Vector3(1, 0.25f, 1);
		}
	}
}