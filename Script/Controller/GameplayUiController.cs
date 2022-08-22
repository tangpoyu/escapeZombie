using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Controller : To use data of GameDataManger to display them in the GamePlayUI timely.
public class GameplayUiController : MonoBehaviour
{
    public DataPersistenceManager DataPersistenceManager
    {
        get => default;
        set
        {
        }
    }

    public void Restart()
    {
        DataPersistenceManager.instance.Restart();
    }

    public void GoHome()
    {
        Destroy(DataPersistenceManager.instance.gameObject);
        Destroy(GameDataManager.instance.gameObject);
        SceneManager.LoadScene("ui");
    }

    public void Save()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
          print(DataPersistenceManager.instance.SaveGame()); // Service
        }
        else
        {
            print("Player is died");
        }
      
    }

    private void Update()
    { 
        if (GameObject.FindWithTag("Player") != null)
        {
            GameObject.FindWithTag("Score").GetComponent<UnityEngine.UI.Text>().text = "Score : " + GameDataManager.instance.Score;
            GameObject.FindWithTag("MaxScore").GetComponent<UnityEngine.UI.Text>().text = "Max Score : " + GameDataManager.instance.MaxScore;
        } else
        {
            DataPersistenceManager.instance.UpdateLeaderboard();
            GameObject.FindWithTag("Hint").GetComponent<UnityEngine.UI.Text>().text = "You died......";
            enabled = false;
        }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
}
