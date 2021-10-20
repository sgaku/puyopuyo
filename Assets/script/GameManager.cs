using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public GameObject[] puyos;
    public GameObject Twin;
    GameObject currentpuyos;
    List<GameObject> checkedpuyos = new List<GameObject>();

    public static  int score;
    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        score = 0;
        PlayerPrefs.DeleteKey("Score");
        highScoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePuyos();
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    //何個繋がっているかを確認
    int Linkcount(int x,int y,int linkCount)
    {
        if ( PuyoMove.grid[x,y]==null || checkedpuyos.Contains(PuyoMove.grid[x, y]))
        {
            return linkCount;
        }
        checkedpuyos.Add(PuyoMove.grid[x, y]);
        linkCount++;
        if (x!=5 && PuyoMove.grid[x+1,y]!=null && PuyoMove.grid[x, y].name == PuyoMove.grid[x + 1, y].name)
        {
            linkCount = Linkcount(x + 1, y, linkCount);
        }
        if (x != 0 && PuyoMove.grid[x - 1, y] != null && PuyoMove.grid[x, y].name == PuyoMove.grid[x - 1, y].name)
        {
            linkCount = Linkcount(x - 1, y, linkCount);
        }
        if (y != 12 && PuyoMove.grid[x , y+1] != null && PuyoMove.grid[x, y].name == PuyoMove.grid[x , y+1].name)
        {
            linkCount = Linkcount(x , y+1, linkCount);
        }
        if (y != 0 && PuyoMove.grid[x , y-1] != null && PuyoMove.grid[x, y].name == PuyoMove.grid[x , y-1].name)
        {
            linkCount = Linkcount(x , y-1, linkCount);
        }
        return linkCount;
    }

    //４個以上繋がりがあるならtrue
    bool HasLink()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 13; y++)
            {
                checkedpuyos.Clear();
                if (Linkcount(x, y, 0) >= 4 && PuyoMove.grid[x, y] != null)
                {
                    return true;
                }


            }
        }
        return false;
    }

    IEnumerator DeletePuyo()
    {
        
        yield return new WaitForSeconds(0.5f);
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 13; y++)
            {
                checkedpuyos.Clear();
                if(Linkcount(x,y,0) >= 4 && PuyoMove.grid[x,y]!=null)
                {
                    Destroy(PuyoMove.grid[x, y]);
                    AddScore();
                }


            }
        }
        yield return new WaitForSeconds(0.5f);
        DropPuyo();
    }

 public  void DropPuyo()
    {


        int nullCount = 0;
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 13; y++)
            {
            
                if(PuyoMove.grid[x,y]== null)
                {
                     nullCount++;
                }
            else     if (nullCount > 0)
                {
                    PuyoMove.grid[x, y].transform.position += new Vector3(0, -nullCount, 0);
                    PuyoMove.grid[x, y-nullCount] = PuyoMove.grid[x, y];
                    PuyoMove.grid[x, y] = null;
          
                }
               
            }
            nullCount = 0;
        }

        if (HasLink())
        {
            StartCoroutine("DeletePuyo");
        }
        else if (!HasLink())
        {
            CreatePuyos();
        }
    }

  public  void CreatePuyos()
    {

        currentpuyos = Instantiate(Twin, new Vector3(2, 11, 0), Quaternion.identity);

        GameObject puyo1 = Instantiate(puyos[Random.Range(0, puyos.Length)], new Vector3(2, 11, 0),Quaternion.identity);
        puyo1.transform.SetParent(currentpuyos.transform, true);
        GameObject puyo2 = Instantiate(puyos[Random.Range(0, puyos.Length)], new Vector3(2,12,0), Quaternion.identity);
        puyo2.transform.SetParent(currentpuyos.transform, true);
    }

    void AddScore()
    {
        score += 10;
        scoreText.text = "Score:" + score.ToString();
        PlayerPrefs.SetInt("Score", score);
    }

    
}
