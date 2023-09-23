using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder : MonoBehaviour
{
    int length = 100;
    int width = 100;
    [SerializeField]
    GameObject groundPrefab;
    const float multiplier = 3.3333f * 3.9f;
    float S;
    float H = Mathf.Sin(30 * 2 * 3.14f / 180);
    float L = Mathf.Cos(30 * 2 * 3.14f / 180);

    void Start()
    {
        S = multiplier * groundPrefab.transform.localScale.x;

        float x;
        int y = 1;

        float ploty = S;
        for (y = 1; y < length; y += 1)
        {
            if (y % 2 == 1)
            {
                for (x = 1; x < width * S;)
                {

                    Vector3 pos = new Vector3(x, 0, ploty);

                    Instantiate(groundPrefab, pos, Quaternion.identity);
                    x += 2 * S * Mathf.Cos(3.14f / 6);
                    //y += 1;

                }

            }
            else
            {

                for (x = 1 + S * Mathf.Cos(3.14f / 6); x < width * S;)
                {



                    Vector3 pos = new Vector3(x, 0, ploty);

                    Instantiate(groundPrefab, pos, Quaternion.identity);
                    x += 2 * S * Mathf.Cos(3.14f / 6);
                    //y += 1;
                }

            }


            ploty += 3 * S / 2;

        }



        //vinay
        //for (int row = 0; row < width; row++)
        //{
        //    float x, y;
        //    //float y = 1;
        //    //for (y = 1; y < length * S;)
        //    for (int col = 0; col < length; col++)
        //    {
        //        x = 2 * L * col + L;
        //        y = (S + H) * row + H + S / 2.0f;
        //        if (row % 2 == 1)
        //        {
        //            x += L;

        //        }

        //        Vector3 pos = new Vector3(y, 0, x);

        //        Instantiate(groundPrefab, pos, Quaternion.identity);

        //    }
        //}

    }

}
