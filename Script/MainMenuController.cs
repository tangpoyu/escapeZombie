using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    // add annotation
    public void PlayGame()
    {
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (clickedObj)
        {
            case "player1":
                GameManager.instance.CharIndex = 0;
                break;
            case "player2":
                GameManager.instance.CharIndex = 1;
                break;
        }

        SceneManager.LoadScene("SampleScene");
    }
}




