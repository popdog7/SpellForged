using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
