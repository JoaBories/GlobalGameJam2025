using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform eyes;

    private void Update()
    {
        transform.position = eyes.position;
    }
}
