using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoMove : MonoBehaviour
{
   // private float previousTime;
  //  public float fallTime = 5f;

    public static  int width = 6, height = 12;
    public static Transform[,] grid = new Transform[width, height];
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
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                this.enabled = false;
                AddGrid();
                FindObjectOfType<GameManager>().CreatePuyos();
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.position, new Vector3(0, 0, -1), 90);
            }
        }
    }

    void AddGrid()
    {
        foreach (Transform childblocks in transform)
        {
            int roundX = Mathf.RoundToInt(childblocks.transform.position.x);
            int roundY = Mathf.RoundToInt(childblocks.transform.position.y);

            grid[roundX, roundY] = childblocks;
        }
    }


    bool ValidMove()
    {
        foreach(Transform childblocks in transform)
        {
          int  roundX = Mathf.RoundToInt(childblocks.transform.position.x);
          int  roundY = Mathf.RoundToInt(childblocks.transform.position.y);

            if(roundX <0 || roundX >= width || roundY < 0|| roundY > height)
            {
                return false;
            }

            if(grid[roundX,roundX] != null)
            {
                return false;
            }

        }
        return true;
    }


   
}
