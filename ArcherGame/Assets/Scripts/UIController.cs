using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayAgain;
    private TMP_Text Score;
    private ArcherController archer;
    void Start()
    {
        Score = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        archer = GameObject.FindObjectOfType<ArcherController>();
        PlayAgain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + archer.score;
        if(archer.GameOver)
        {
            PlayAgain.SetActive(true); // we show the game over image

            if(Input.GetKey(KeyCode.Return))
            {
                Time.timeScale = 1;
                archer.GameOver = false;
                SceneManager.LoadScene(0); // restart the game
            }
        }
    }
}
