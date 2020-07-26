﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves the camera based on mouse click
public class MoveCamera : MonoBehaviour
{
    public float speed = 4f;
    private Vector3 startPos;
    private bool moving;

    void FixedUpdate(){
        // 1 - right, 0 - left
        if (Input.GetMouseButtonDown(1)) {
            startPos = Input.mousePosition;
            moving = true;
        }
        if (Input.GetMouseButtonUp(1) && moving) {
            moving = false;
        }
        if (moving) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - startPos);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, 0);
            transform.Translate(move, Space.Self);
        }
    }
}
