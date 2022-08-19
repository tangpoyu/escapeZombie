using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controller
public class MainMenuController : MonoBehaviour
{
    
    // add annotation
    public void PlayGame()
    {
        // Service
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (clickedObj)
        {
            case "player1":
                GameDataManager.instance.CharIndex = 0;
                break;
            case "player2":
                GameDataManager.instance.CharIndex = 1;
                break;
        }
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("SampleScene");
       
    }

    public void LoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene("SampleScene");
        
    }
}




