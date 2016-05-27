using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Game Camera controls
	/// </summary>
	public class GameCamera : MonoBehaviour
	{
		[Header("Camera Gameobject")]
		public Camera cam;

		[Header("Camera Angle Settings")]
		public float maxAngle = 90f;
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
		[Range(8f, 30f)]
		public float cutsceneZoom = 10f;

		[Header("final camera start position")]
		public bool startUpdateInEditor = false;
		[Range(10f, 80f)]
		public float startAngle = 40f;
		[Range(-180f, 180f)]
		public float startRotation = 0f;
		[Range(8f, 30f)]
		public float startZoom = 10f;

		[Header("orthographic mode")]
		public bool orthographicUpdateInEditor = false;
		[Range(4f, 30f)]
		public float orthographicSize = 10f;
		[Range(4f, 30f)]
		public float orthographicDistance = 10f;

		[Header("top down mode")]
		public bool topdownUpdateInEditor = false;
		[Range(4f, 30f)]
		public float topDownDistance = 10f;

		private bool cutscene;
		private CameraInput cameraInput;

		private float currentAngle;
		private float currentRotation;
		private float currentZoom;
		
		private bool inputEnabled;
		private int cameraMode;

		protected void Awake()
		{
			cutscene = false;
			cameraInput = new CameraInput();
			StartCoroutine("CutsceneAnimation");

			currentAngle = startAngle;
			currentRotation = startRotation;
			currentZoom = startZoom;

			inputEnabled = true;
			SetCameraPosition(cutsceneAngle, cutsceneRotation, cutsceneZoom);
		}

		protected void OnEnable()
		{
			GlobalEvents.AddListener<SetInputEvent>(OnSetInputEvent);
			
			cameraMode = 0;
			cam.orthographic = false;
		}

		protected void OnDisable()
		{
			GlobalEvents.AddListener<SetInputEvent>(OnSetInputEvent);
		}

		/// <summary>
		/// Starts the cutscene camera animation
		/// </summary>
		public IEnumerator CutsceneAnimation()
		{
			cutscene = true;

			float fixedRotation = cutsceneRotation;
			fixedRotation = fixedRotation > startRotation + 180f ? fixedRotation - 360f : fixedRotation < startRotation - 180f ? fixedRotation + 360f : fixedRotation;
			transform.rotation = SettingsToQuaternion(cutsceneAngle, fixedRotation);
			transform.DORotateQuaternion(SettingsToQuaternion(startAngle, startRotation), 5f).SetEase(Ease.InOutCubic);
			cam.transform.DOLocalMoveZ(startZoom, 5f, false).SetEase(Ease.InOutCubic);

			yield return new WaitForSeconds(5f);

			cutscene = false;
		}

		protected void Update()
		{
			if(!inputEnabled || ScreenManager.GetCurrentScreenName() != "ScreenGame")
			{
				return;
			}

			if(!cutscene)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					cameraMode = cameraMode == 0 ? 1 : cameraMode == 1 ? 2 : 0;

					if(cameraMode == 1)
					{
						cam.orthographic = true;
						cam.orthographicSize = 15f;
					}
					else if(cameraMode == 2)
					{
						cam.orthographic = false;
					}
					else
					{
						cam.orthographic = false;
						SetCameraPosition(currentAngle, currentRotation, currentZoom);
					}
				}

				if (cameraMode == 1 || cameraMode == 2)
				{
					cameraInput.UpdateInput();

					//currentRotation += cameraInput.moveX * 0.02f * (currentZoom);// * (currentZoom * 0.08f);
					//currentRotation %= 360f;

					SetCameraPosition(90f, 0f, 0f);
					cam.transform.position = new Vector3 (cam.transform.position.x, cameraMode == 1 ? orthographicDistance : topDownDistance, cam.transform.position.z);
				} 
				else
				{
					cameraInput.UpdateInput();

					currentAngle -= cameraInput.moveY * 0.02f * (currentZoom);//* (currentZoom * 0.08f);
					currentAngle = currentAngle < minAngle ? minAngle : currentAngle > maxAngle ? maxAngle : currentAngle;

					currentRotation += cameraInput.moveX * 0.02f * (currentZoom);// * (currentZoom * 0.08f);
					currentRotation %= 360f;

					currentZoom -= cameraInput.deltaZoom / 40f;
					currentZoom = currentZoom < minZoom ? minZoom : currentZoom > maxZoom ? maxZoom : currentZoom;

					SetCameraPosition(currentAngle, currentRotation, currentZoom);
				}
			}
		}

		protected void OnValidate()
		{
			int c = 0;
			if(topdownUpdateInEditor) { c++; }
			if(cutsceneUpdateInEditor) { c++; }
			if(orthographicUpdateInEditor) { c++; }
			if(startUpdateInEditor) { c++; }

			if(c > 1)
			{
				Debug.LogError("Please turn off cutsceneUpdateInEditor, startUpdateInEditor, orthographicUpdateInEditor or topdownUpdateInEditor");
			}
			else if(orthographicUpdateInEditor)
			{
				cam.orthographic = true;
				cam.orthographicSize = orthographicSize;
				SetCameraPosition(90f, 0f, 0f);
				cam.transform.position = new Vector3 (cam.transform.position.x, orthographicDistance, cam.transform.position.z);
			}
			else if(topdownUpdateInEditor)
			{
				cam.orthographic = false;
				SetCameraPosition(90f, 0f, 0f);
				cam.transform.position = new Vector3 (cam.transform.position.x, topDownDistance, cam.transform.position.z);
			}
			else if(cutsceneUpdateInEditor)
			{
				cam.orthographic = false;
				SetCameraPosition(cutsceneAngle, cutsceneRotation, cutsceneZoom);
			}
			else if(startUpdateInEditor)
			{
				cam.orthographic = false;
				SetCameraPosition(startAngle, startRotation, startZoom);
			}
		}

		/// <summary>
		/// Sets the camera to these settings
		/// </summary>
		/// <param name="_angle">Camera X rotation</param>
		/// <param name="_rotation">Camera Y rotation</param>
		/// <param name="_zoom">Camera distance to center</param>
		public void SetCameraPosition(float _angle, float _rotation, float _zoom)
		{
			transform.rotation = SettingsToQuaternion(_angle, _rotation);
			cam.transform.localPosition = new Vector3(0f, 0f, _zoom);
			cam.transform.LookAt(transform);
		}

		/// <summary>
		/// Converts Euler rotation to Quaternion
		/// </summary>
		/// <param name="_angle">Camera X rotation</param>
		/// <param name="_rotation">Camera Y rotation</param>
		/// <returns></returns>
		private Quaternion SettingsToQuaternion(float _angle, float _rotation)
		{
			return Quaternion.Euler(new Vector3(-_angle, _rotation, 0f));
		}

		private void OnSetInputEvent(SetInputEvent _evt)
		{
			inputEnabled = _evt.Enabled;
		}
	}
}
