using DG;
using DG.Tweening;
using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	[Header("Camera Gameobject")]
	public Camera cam;

	[Header("Camera Angle Settings")]
	public float maxAngle = 75f;
	public float minAngle = 10f;

	[Header("Camera Zoom Settings")]
	public float minZoom = 8f;
	public float maxZoom = 20f;

	[Header("cutscene camera start position")]
	public bool cutsceneUpdateInEditor = false;
	[Range(10f, 80f)]
	public float cutsceneAngle = 40f;
	[Range(-180f, 180f)]
	public float cutsceneRotation = 0f;
	[Range(8f, 20f)]
	public float cutsceneZoom = 10f;

	[Header("final camera start position")]
	public bool startUpdateInEditor = false;
	[Range(10f, 80f)]
	public float startAngle = 40f;
	[Range(-180f, 180f)]
	public float startRotation = 0f;
	[Range(8f, 20f)]
	public float startZoom = 10f;

	private bool cutscene;

	protected void Awake () 
	{
		cutscene = false;
		StartCoroutine ("cutsceneAnimation");
	}

	//KEVIN: moet gecalled worden door een start game event?
	public IEnumerator cutsceneAnimation()
	{
		cutscene = true;

		transform.rotation = SettingsToQuaternion (cutsceneAngle, cutsceneRotation);
		transform.DORotateQuaternion (SettingsToQuaternion (startAngle, startRotation), 5f).SetEase(Ease.InOutCubic);
		cam.transform.DOLocalMoveZ (startZoom, 5f, false).SetEase(Ease.InOutCubic);

		yield return new WaitForSeconds (5f);

		cutscene = false;

		StartCoroutine ("cutsceneAnimation");
	}

	protected void OnValidate()
	{
		if(cutsceneUpdateInEditor && startUpdateInEditor)
		{
			Debug.LogError ("Please turn off cutsceneUpdateInEditor or startUpdateInEditor");
		}
		else if(cutsceneUpdateInEditor)
		{
			transform.rotation = SettingsToQuaternion (cutsceneAngle, cutsceneRotation);
			cam.transform.localPosition = new Vector3 (0f, 0f, cutsceneZoom);
			cam.transform.LookAt (transform);
		}
		else if(startUpdateInEditor)
		{
			transform.rotation = SettingsToQuaternion (startAngle, startRotation);
			cam.transform.localPosition = new Vector3 (0f, 0f, startZoom);
			cam.transform.LookAt (transform);
		}
	}

	private Quaternion SettingsToQuaternion(float _angle, float _rotation)
	{
		return Quaternion.Euler(new Vector3 (-_angle, _rotation, 0f));
	}
}
