using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resize camera based on display resolution
public class PixelPerfectCamera : MonoBehaviour
{
    public static float pixelsToUnits = 1f;
    public static float scale = 1f;

    //mimic original gameboy screen
    public Vector2 nativeResolution = new Vector2 (160, 144);
    void Awake(){
        var camera = GetComponent<Camera>();
        if (camera.orthographic) {
            // for landscape games, calc height
            var dir = Screen.height;
            var res = nativeResolution.y;

            scale = dir / res;
            pixelsToUnits *= scale;
            camera.orthographicSize = (dir / 2.0f) / pixelsToUnits;
        }
    }
}
