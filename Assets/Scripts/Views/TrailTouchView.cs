using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTouchView : TrailView
{
    protected override void TrailMoving()
    {
        if (Input.touchCount > 0)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position = pos;

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    gameObject.SetActive(true);
                    break;

                case TouchPhase.Ended:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
