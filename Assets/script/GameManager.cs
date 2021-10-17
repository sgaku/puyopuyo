using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] puyos;
    public GameObject Twin;
    GameObject currentpuyos;
    // Start is called before the first frame update
    void Start()
    {
        CreatePuyos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public   void CreatePuyos()
    {

        currentpuyos = Instantiate(Twin, new Vector3(2, 11, 0), Quaternion.identity);

        GameObject puyo1 = Instantiate(puyos[Random.Range(0, puyos.Length)], new Vector3(2, 11, 0),Quaternion.identity);
        puyo1.transform.SetParent(currentpuyos.transform, true);
        GameObject puyo2 = Instantiate(puyos[Random.Range(0, puyos.Length)], new Vector3(2,12,0), Quaternion.identity);
        puyo2.transform.SetParent(currentpuyos.transform, true);
    }
}
