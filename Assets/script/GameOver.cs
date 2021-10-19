using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject gameManager;
    public GameObject showScoreText;
    public GameObject sc, highsc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showGameOver()
    {
        gameOverText.SetActive(true);
        gameManager.SetActive(false);
        showScoreText.SetActive(true);
        sc.SetActive(false);
        highsc.SetActive(false);   
         Text showtext = showScoreText.GetComponent<Text>();
        showtext.text = "Your Score:"+ PlayerPrefs.GetInt("Score", 0);

        if (GameManager.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameManager.score);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
    
}
