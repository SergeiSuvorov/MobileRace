using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTouchView : TrailView
{
    protected override void onUpdate()
    {
        if (Input.touchCount > 0)
        {
            SetTrailPossition(Input.GetTouch(0).position);

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
