using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject inputField, mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterGame()
    {
        GameDataManager.instance.PlayerName = inputField.GetComponent<Text>().text;
        // DataPersistenceManager.EnterGame(inputField.GetComponent<Text>().text);
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
