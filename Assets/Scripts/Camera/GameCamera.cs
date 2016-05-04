using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
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
	private CameraInput cameraInput;

	private float currentAngle;
	private float currentRotation;
	private float currentZoom;

	protected void Awake () 
	{
		cutscene = false;
		cameraInput = new CameraInput ();
		StartCoroutine ("cutsceneAnimation");

		currentAngle = startAngle;
		currentRotation = startRotation;
		currentZoom = startZoom;
	}

	public IEnumerator cutsceneAnimation()
	{
		cutscene = true;

		float fixedRotation = cutsceneRotation;
		fixedRotation = fixedRotation > startRotation + 180f ? fixedRotation - 360f : fixedRotation < startRotation - 180f ? fixedRotation + 360f : fixedRotation;
		transform.rotation = SettingsToQuaternion (cutsceneAngle, fixedRotation);
		transform.DORotateQuaternion (SettingsToQuaternion (startAngle, startRotation), 5f).SetEase(Ease.InOutCubic);
		cam.transform.DOLocalMoveZ (startZoom, 5f, false).SetEase(Ease.InOutCubic);

		yield return new WaitForSeconds (5f);

		cutscene = false;
	}

	protected void Update()
	{
		if(!cutscene)
		{
			cameraInput.UpdateInput();

			currentAngle -= cameraInput.moveY * 0.02f * (currentZoom);//* (currentZoom * 0.08f);
			currentAngle = currentAngle < minAngle ? minAngle : currentAngle > maxAngle ? maxAngle : currentAngle;

			currentRotation += cameraInput.moveX * 0.02f * (currentZoom);// * (currentZoom * 0.08f);
			currentRotation %= 360f;

			currentZoom -= cameraInput.deltaZoom / 40f;
			currentZoom = currentZoom < minZoom ? minZoom : currentZoom > maxZoom ? maxZoom : currentZoom;

			setCameraPosition(currentAngle, currentRotation, currentZoom);
		}
	}

	protected void OnValidate()
	{
		if(cutsceneUpdateInEditor && startUpdateInEditor)
		{
			Debug.LogError ("Please turn off cutsceneUpdateInEditor or startUpdateInEditor");
		}
		else if(cutsceneUpdateInEditor)
		{
			setCameraPosition(cutsceneAngle, cutsceneRotation, cutsceneZoom);
		}
		else if(startUpdateInEditor)
		{
			setCameraPosition(startAngle, startRotation, startZoom);
		}
	}

	public void setCameraPosition(float _angle, float _rotation, float _zoom)
	{
		transform.rotation = SettingsToQuaternion (_angle, _rotation);
		cam.transform.localPosition = new Vector3 (0f, 0f, _zoom);
		cam.transform.LookAt (transform);
	}

	private Quaternion SettingsToQuaternion(float _angle, float _rotation)
	{
		return Quaternion.Euler(new Vector3 (-_angle, _rotation, 0f));
	}
}