using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MapMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MapMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Debug.Log("Up key is pressed");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){
            Debug.Log("Right key is pressed");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Debug.Log("Down key is pressed");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Debug.Log("Left key is pressed");
        }
    }
}
