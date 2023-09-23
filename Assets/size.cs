using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class size : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var p1 = transform.TransformPoint(0, 0, 0);
        var p2 = transform.TransformPoint(0, 0, 1);
        var w = p2.x - p1.x;
        var h = p2.y - p1.y;

        Debug.Log(w + " " + h);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
