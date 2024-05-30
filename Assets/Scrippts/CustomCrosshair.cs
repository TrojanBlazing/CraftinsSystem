using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCrosshair : MonoBehaviour
{

    private void Awake()
    {
        transform.position = Input.mousePosition;
    }
    private void Update()
    {
        transform.position=Input.mousePosition;
    }
}
