using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementController : MonoBehaviour
{

    public Map map;
    public Vector2 tileSize;
    private int tmpX;
    private int tmpY;

    // move to tile index
    public void MoveTo(int index){
        PosUtil.CalculatePos(index, map.columns, out tmpX, out tmpY);
        tmpX *= (int)tileSize.x;
        tmpY *= -(int)tileSize.y;

        transform.position = new Vector3(tmpX, tmpY, 0);

    }

}
