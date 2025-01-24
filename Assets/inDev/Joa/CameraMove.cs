using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform camPos;

    private void Update()
    {
        transform.position = camPos.position;
    }
}
