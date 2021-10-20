using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoMove : MonoBehaviour
{
    private float previousTime;

    GameObject ghostpuyos;
    public static  int width = 6, height = 13;
    public static GameObject[,] grid = new GameObject[width, height];
    // Start is called before the first frame update
    void Start()
    {
        CreatGhostPuyos();
        previousTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGhost();

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

        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - previousTime >0.4)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddGrid();
                Destroy(ghostpuyos);
                this.gameObject.transform.DetachChildren();
                Destroy(this.gameObject);
                this.enabled = false;
                
                FindObjectOfType<GameManager>().DropPuyo();
            }
            previousTime = Time.time;
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

            grid[roundX, roundY] = childblocks.gameObject;

            if(roundY > 11)
            {
               FindObjectOfType<GameOver>().showGameOver();
         }
        }
    }

    
    void CreatGhostPuyos()
    {
        ghostpuyos = Instantiate(this.gameObject);
        ghostpuyos.transform.position = this.gameObject.transform.position;
        Destroy(ghostpuyos.GetComponent<PuyoMove>());
        foreach(Transform children in ghostpuyos.transform)
        {
            children.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,0.5f);
            children.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
   
    void MoveGhost()
    {
        ghostpuyos.transform.position = this.gameObject.transform.position;
        ghostpuyos.transform.rotation = this.gameObject.transform.rotation;

        while (ValidMoveGhost())
        {
            ghostpuyos.transform.position += new Vector3(0, -1, 0);
        }
        if (!ValidMoveGhost())
        {
            ghostpuyos.transform.position += new Vector3(0, 1, 0);
        }
    }

    bool ValidMoveGhost()
    {
        foreach (Transform childblocks in ghostpuyos.transform)
        {
            int roundX = Mathf.RoundToInt(childblocks.transform.position.x);
            int roundY = Mathf.RoundToInt(childblocks.transform.position.y);

            if (roundX < 0 || roundX >= width || roundY < 0 || roundY > height)
            {
                return false;
            }

            if (grid[roundX, roundY] != null)
            {
                return false;
            }

        }
        return true;
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

            if(grid[roundX,roundY] != null)
            {
                return false;
            }

        }
        return true;
    }


   
}
