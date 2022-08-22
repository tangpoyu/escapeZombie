using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotMenuController : MonoBehaviour
{
    private SaveSlot[] saveSlots;

    public DataPersistenceManager DataPersistenceManager
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    private void Start()
    {
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        ClientData profilesGameData;
        bool haveData = DataPersistenceManager.instance.GameData.ClientDatas
            ._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out profilesGameData);
        if(!haveData) profilesGameData = new ClientData();
        
        foreach (SaveSlot saveSlot in saveSlots)
        {
            ProfileData profileData = null;
            profilesGameData.savedProfileData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
        }
    }

    public void BackMainMenu()
    {
        
    }

    public void PlayGame()
    {
        // Service
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (clickedObj)
        {
            case "player1":
                DataPersistenceManager.instance.setCharacter(0);
                break;
            case "player2":
                DataPersistenceManager.instance.setCharacter(1);
                break;
        }
    }
}
