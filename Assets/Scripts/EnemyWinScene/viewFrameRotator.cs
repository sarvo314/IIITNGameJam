using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewFrameRotator : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 1.0f;

    Transform trasnform;

    void Start()
    {
        trasnform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
    }
}
