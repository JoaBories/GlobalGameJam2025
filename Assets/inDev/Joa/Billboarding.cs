using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Vector3 cameraViewDir;

    private void Update()
    {
        cameraViewDir = Camera.main.transform.forward;
        cameraViewDir.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraViewDir);
    }
}
