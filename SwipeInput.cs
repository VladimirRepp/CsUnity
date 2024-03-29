using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : Singleton<SwipeInput>, IInitialized
{
    private bool _tap, _swipeLeft, _swipeRight, _swipeDown, _swipeUp;
    private bool _isDragging = false;
    private Vector2 _startTouch, _swipeDelta;

    public Vector2 SwipeDelta { get { return _swipeDelta; } }
    public bool SwipeLeft { get { return _swipeLeft; } }
    public bool SwipeRight { get { return _swipeRight; } }
    public bool SwipeUp { get { return _swipeUp; } }
    public bool SwipeDown { get { return _swipeDown; } }
    public bool Tap { get { return _tap; } }

    private void Update()
    {
        _tap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

        #region Windows Inputs

        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            _isDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }

        #endregion

        #region Mobile inputs

        if (Input.touches.Length > 0)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }

        #endregion

        // Calculate the distance
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            if (Input.touches.Length > 0)
            {
                _swipeDelta = Input.touches[0].position - _startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        // Did we cross the deadzone?
        if (_swipeDelta.magnitude > 125)
        {

            // Which direction?
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                if (x < 0)
                    _swipeLeft = true;
                else
                    _swipeRight = true;
            }
            else
            {
                // Up or down
                if (y < 0)
                    _swipeDown = true;
                else
                    _swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        _startTouch = Vector2.zero;
        _isDragging = false;
    }

    public void Initialize()
    {
    }
}
