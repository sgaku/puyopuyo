using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float previousTime;
    public float fallTime = 0.8f;
    public Vector3 rotationPoint;

    private int width = 6, height = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!VaildMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!VaildMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!VaildMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                
            }
            previousTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!VaildMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }
    bool VaildMove()
    {
        foreach (Transform block in transform)
        {
            int roundX = Mathf.RoundToInt(block.transform.position.x);
            int roundY = Mathf.RoundToInt(block.transform.position.y);

            if (roundX <= 0 || roundX > width || roundY < 0 || roundY >= height)
            {
                return false;
            }

           
        }



        return true;
    }
}
