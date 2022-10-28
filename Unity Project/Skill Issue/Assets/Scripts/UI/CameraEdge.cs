using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdge : MonoBehaviour
{
    private Vector3 transform;
    public Camera cam;
    private Vector3 currentTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform = cam.WorldToScreenPoint(transform);
        if (currentTransform != transform)
        {
            Debug.Log(transform);
            currentTransform = transform;
        }
        
    }
}
