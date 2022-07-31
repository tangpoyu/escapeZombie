using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUiController : MonoBehaviour
{
   

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        
        SceneManager.LoadScene("ui");
    }

    private void Update()
    {

        if (GameObject.FindWithTag("Player") != null)
        {
            GameObject.FindWithTag("Score").GetComponent<UnityEngine.UI.Text>().text = "Score : " + ScoreManager.instance.Score1;
            GameObject.FindWithTag("MaxScore").GetComponent<UnityEngine.UI.Text>().text = "Max Score : " + ScoreManager.instance.MaxScore1;
        } else
        {
            GameObject.FindWithTag("Hint").GetComponent<UnityEngine.UI.Text>().text = "You died......";
        }
        
   
    }

    private void Start()
    {
        Debug.Log("start");
    }
}
