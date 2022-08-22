using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject inputField, mainMenu;

    public DataPersistenceManager DataPersistenceManager
    {
        get => default;
        set
        {
        }
    }

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
        DataPersistenceManager.instance.EnterGame(inputField.GetComponent<Text>().text);
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
