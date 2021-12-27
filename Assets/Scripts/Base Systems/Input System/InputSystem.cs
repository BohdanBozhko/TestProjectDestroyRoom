using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// This system will read input from the UI panel to which it's attached (make sure this panel not covered by another).
/// </summary>
public class InputSystem : MonoSingleton<InputSystem>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public enum SwipeDirection { Up, Down, Right, Left }

	[SerializeField] FloatingJoystick joystick;

	private Camera mainCamera;
	private Vector3 startTouchPosition;
	private Vector3 endTouchPosition;

	public FloatingJoystick Joystick => joystick;

	public Camera MainCamera
	{
		get
		{
			if (mainCamera == null)
			{
				mainCamera = Camera.main;
			}

			return mainCamera;
		}
	}

	public event Action OnTouch;
	public event Action OnRelease;
	public event Action<SwipeDirection> OnSwipe;
	public event Action<Vector2> OnDragAction;

	protected override void Awake()
	{
		base.Awake();
		mainCamera = Camera.main;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
			OnTouch?.Invoke();
			GetSwipePoints(ref startTouchPosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
			OnRelease?.Invoke();
			GetSwipePoints(ref endTouchPosition);
			Swipe();
	}

	public void OnDrag(PointerEventData eventData)
	{
			OnDragAction?.Invoke(eventData.delta);
	}

	private void GetSwipePoints(ref Vector3 point)
	{
		point = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
	}

	public void Swipe()
	{
		Vector3 delta = endTouchPosition - startTouchPosition;
		Vector3 absDelta; 

		if (delta.x < 0 && delta.y < 0 && delta.z < 0)
        {
			absDelta = delta;
        }			
		else
        {
			absDelta = delta * -1;
        }

		if (delta.x > 0 && absDelta.x > absDelta.y) OnSwipe?.Invoke(SwipeDirection.Right);
		if (delta.x < 0 && absDelta.x > absDelta.y) OnSwipe?.Invoke(SwipeDirection.Left);
		if (delta.y > 0 && absDelta.x < absDelta.y) OnSwipe?.Invoke(SwipeDirection.Up);
		if (delta.y < 0 && absDelta.x < absDelta.y) OnSwipe?.Invoke(SwipeDirection.Down);
	}




}
