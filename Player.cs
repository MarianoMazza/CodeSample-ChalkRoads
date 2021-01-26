using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mousePosition;
    Camera cameraMain;
    void Start()
    {
        cameraMain = Camera.main;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = cameraMain.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -7);
    }
}
