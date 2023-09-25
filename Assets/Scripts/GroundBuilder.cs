using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder : MonoBehaviour
{
    int length = 100;
    int width = 100;
    [SerializeField]
    GameObject groundPrefab;
    // a multiplier to sync sidelengthperfectly
    const float multiplier = 3.3333f * 3.9f;
    const float pi = 3.141592f;
    float sideLength;
    float H = Mathf.Sin(30 * 2 * 3.14f / 180);
    float L = Mathf.Cos(30 * 2 * 3.14f / 180);

    void Start()
    {
        sideLength = multiplier * groundPrefab.transform.localScale.x;

        float x;
        int y = 1;

        float ploty = sideLength;
        for (y = 1; y < length; y += 1)
        {
            if (y % 2 == 1)
            {
                for (x = 1; x < width * sideLength;)
                {

                    Vector3 pos = new Vector3(x, 0, ploty);

                    Instantiate(groundPrefab, pos, Quaternion.identity);
                    x += 2 * sideLength * Mathf.Cos(pi / 6);
                }

            }
            else
            {

                for (x = 1 + sideLength * Mathf.Cos(3.14f / 6); x < width * sideLength;)
                {
                    Vector3 pos = new Vector3(x, 0, ploty);
                    Instantiate(groundPrefab, pos, Quaternion.identity);
                    x += 2 * sideLength * Mathf.Cos(pi / 6);
                }

            }
            ploty += 3 * sideLength / 2;

        }

    }

}
