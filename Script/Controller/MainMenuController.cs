using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controller
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject saveSlotMenu, contuineButton, welcomeMessage;

    private void Awake()
    {
        ClientData clientData = new ClientData();

        DataPersistenceManager.instance.GameData.ClientDatas._clientDatas
            .TryGetValue(GameDataManager.instance.PlayerName, out clientData);

        if (clientData.savedProfileData.Count == 0)
        {
            contuineButton.GetComponent<Button>().interactable = false;
        }
        welcomeMessage.GetComponent<TextMeshProUGUI>().text = "Welcome, " + GameDataManager.instance.PlayerName;
    }

    public void LoadSaveSlotMenu()
    {
        this.gameObject.SetActive(false);
        saveSlotMenu.SetActive(true);        
    }

    public void Continue()
    {
        SceneManager.LoadScene("LoadScene");
    }
}




