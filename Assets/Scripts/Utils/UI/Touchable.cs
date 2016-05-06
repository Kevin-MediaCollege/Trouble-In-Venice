using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Utils
{
	/// <summary>
	/// 
	/// </summary>
	public enum TouchableState
	{
		Normal,
		Pressed,
		Disabled
	}

	/// <summary>
	/// 
	/// </summary>
	public class Touchable : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler
	{
		//--------------------------------------------------------------------------------//
		public Sprite PressedSprite = null;
		public Sprite DisabledSprite = null;
		public bool CancelPointerWhenItExits = true;
		//--------------------------------------------------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sender"></param>
		/// <param name="_eventData"></param>
		public delegate void OnPointerDownHandler(Touchable _sender, PointerEventData _eventData);

		/// <summary>
		/// 
		/// </summary>
		public event OnPointerDownHandler OnPointerDownEvent = delegate { };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sender"></param>
		/// <param name="_eventData"></param>
		public delegate void OnPointerUpHandler(Touchable _sender, PointerEventData _eventData);

		/// <summary>
		/// 
		/// </summary>
		public event OnPointerUpHandler OnPointerUpEvent = delegate { };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sender"></param>
		/// <param name="_eventData"></param>
		public delegate void OnPointerExitHandler(Touchable _sender, PointerEventData _eventData);

		/// <summary>
		/// 
		/// </summary>
		public event OnPointerExitHandler OnPointerExitEvent = delegate { };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sender"></param>
		/// <param name="_eventData"></param>
		public delegate void OnPointerMoveHandler(Touchable _sender, PointerEventData _eventData);

		/// <summary>
		/// 
		/// </summary>
		public event OnPointerMoveHandler OnPointerMoveEvent = delegate { };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sender"></param>
		/// <param name="_eventData"></param>
		public delegate void OnPointerEnterHandler(Touchable _sender, PointerEventData _eventData);

		/// <summary>
		/// 
		/// </summary>
		public event OnPointerEnterHandler OnPointerEnterEvent = delegate { };

		public Canvas Canvas { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Camera Camera { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Image Image { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public TouchableState State { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public bool Interactable
		{
			get { return State != TouchableState.Disabled; }
			set
			{
				if(!value)
					SetState(TouchableState.Disabled);
				else if(value && State == TouchableState.Disabled)
					SetState(TouchableState.Normal);
			}
		}

		private Sprite NormalSprite = null;

		protected List<int> pointers = new List<int>();

		/// <summary>
		/// 
		/// </summary>
		protected virtual void Start()
		{
			Canvas = GetComponent<Graphic>().canvas;
			if(Canvas.renderMode != RenderMode.ScreenSpaceOverlay)
				Camera = Canvas.worldCamera;
			else
				Camera = null;

			if(GetComponent<Image>() != null)
				Image = GetComponent<Image>();
			if(Image != null)
				NormalSprite = Image.sprite;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		public virtual void OnPointerDown(PointerEventData _eventData)
		{
			if(!Interactable)
				return;

			SetState(TouchableState.Pressed);
			pointers.Add(_eventData.pointerId);

			OnPointerDownEvent(this, _eventData);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		public virtual void OnPointerUp(PointerEventData _eventData)
		{
			if(!pointers.Contains(_eventData.pointerId))
				return;

			pointers.Remove(_eventData.pointerId);

			if(!Interactable)
				return;

			SetState(TouchableState.Normal);
			OnPointerUpEvent(this, _eventData);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		public virtual void OnPointerEnter(PointerEventData _eventData)
		{
			if(Interactable)
				OnPointerEnterEvent(this, _eventData);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		public virtual void OnPointerExit(PointerEventData _eventData)
		{
			if(Interactable)
			{
				SetState(TouchableState.Normal);
				OnPointerExitEvent(this, _eventData);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		public virtual void OnDrag(PointerEventData _eventData)
		{
			if(!pointers.Contains(_eventData.pointerId))
				return;

			if(Interactable)
				OnPointerMoveEvent(this, _eventData);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_state"></param>
		protected virtual void SetState(TouchableState _state)
		{
			State = _state;
			if(_state == TouchableState.Normal)
			{
				if(Image != null && NormalSprite != null)
					Image.sprite = NormalSprite;
			}
			else if(_state == TouchableState.Pressed)
			{
				if(Image != null && PressedSprite != null)
					Image.sprite = PressedSprite;
			}
			else if(_state == TouchableState.Disabled)
			{
				if(Image != null && DisabledSprite != null)
					Image.sprite = DisabledSprite;
			}

			if(Image != null && Image.type == Image.Type.Simple)
				Image.SetNativeSize();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		/// <returns></returns>
		public Vector3 GetCanvasPos(PointerEventData _eventData)
		{
			return Canvas.transform.InverseTransformPoint(GetWorldPos(_eventData));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_eventData"></param>
		/// <returns></returns>
		public Vector3 GetWorldPos(PointerEventData _eventData)
		{
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, _eventData.position, Camera, out pos);

			return transform.TransformPoint(pos);
		}
	}
}
