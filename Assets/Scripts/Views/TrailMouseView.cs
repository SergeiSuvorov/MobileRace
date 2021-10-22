using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMouseView : TrailView
{
   
    protected override void TrailMoving()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.SetActive(false);
        }
    }
}
