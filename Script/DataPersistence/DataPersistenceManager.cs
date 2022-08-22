using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

// Service : To load data which is converted by DataHandler from Client or Server in IDataPersistence
// or Save data which is converted by DataHandler from IDataPersistence in Client or Server in somehow
// 平衡遊戲資料及實際儲存資料
public class DataPersistenceManager : MonoBehaviour
{
    [Header(("File Storage Config"))]
    [SerializeField] private string filename;

    private GameData gameData; // Model
    private List<IDataPersistence> dataPersistenceList;
    private FileDataHandler fileDataHandler; // Service

    public static DataPersistenceManager instance { get; private set; }
    public GameData GameData { get => gameData; set => gameData = value; }

    public IDataPersistence IDataPersistence
    {
        get => default;
        set
        {
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

        private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {   
        if(scene.name == "LoadScene")  Load(gameData, "gameData");
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manger in the scene");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, filename);
        GameData = new GameData();
        ClientDatas clientDatas = fileDataHandler.LoadFromClient();
        if (clientDatas != null) gameData.ClientDatas = clientDatas;
        ServerData serverData = fileDataHandler.LoadFromServer();
        if (serverData != null) GameData.ServerData = serverData;
    }

    private void Start()
    {
        
    }

    public void EnterGame(string playerName)
    {
        ClientData clientData = new ClientData();
        if (!gameData.ClientDatas._clientDatas.ContainsKey(playerName))
        {
            clientData.playname = playerName;
            gameData.ClientDatas._clientDatas.Add(playerName, clientData);
        }
        GameData.ClientDatas._clientDatas.TryGetValue(playerName, out clientData);
        Load(clientData, "clientData");
    }

    public void setCharacter(int charIndex)
    {
        ClientData clientData;
        gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        clientData.currentProfileData.charIndex = charIndex;
        SceneManager.LoadScene("LoadScene");
    }

    public void NewGame()
    {
        //var obj = GameData.ClientDatas;
        //foreach (IDataPersistence dataPersistence in dataPersistenceList)
        //{
        //    dataPersistence.SaveData(ref obj);
        //}
        //fileDataHandler.SaveToClient(GameData);
        //ServerData serverData = fileDataHandler.LoadFromServer();
        //if (serverData == null)
        //{
        //    SceneManager.LoadScene("SampleScene");
        //    return;
        //}
        //GameData.ServerData = serverData;
        //foreach (IDataPersistence dataPersistence in dataPersistenceList)
        //{
        //    dataPersistence.LoadData(GameData.ServerData);
        //}
        SceneManager.LoadScene("");
    }

    public bool LoadGame(SaveSlot saveSlot)
    {
        if(saveSlot.ProfileData == null)
        {
            ClientData clientData;
            gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
            ProfileData profileData = new ProfileData(saveSlot.ProfieId);
            clientData.currentProfileData = profileData;
            return true;
        }
        else
        {
            ClientData clientData;
            gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
            clientData.currentProfileData = saveSlot.ProfileData;
            SceneManager.LoadScene("LoadScene");
            return false;
        }
    }


    public void Load(Data data, string type)
    {
        FindAllDataPersistenceObjects();
        switch(type){
            case "clientData":
                foreach (IDataPersistence dataPersistence in dataPersistenceList)
                {
                    dataPersistence.LoadData((ClientData)data);
                }
                break;

            case "gameData":
                foreach (IDataPersistence dataPersistence in dataPersistenceList)
                {
                    dataPersistence.LoadData((GameData)data);
                }
                break;
        }
    }

    //public void LoadGame(string playerName , string selectedProfileId)
    //{
    //    ClientData clientData = fileDataHandler.LoadFromClient(playerName, selectedProfileId);
    //    if (clientData == null)
    //    {
    //        NewGame();
    //    }else
    //    {
    //        this.GameData.ClientData = clientData;
    //        this.GameData.ServerData = fileDataHandler.LoadFromServer();
    //        foreach (IDataPersistence dataPersistence in dataPersistenceList)
    //        {
    //            dataPersistence.LoadData(GameData);
    //        }
    //    }
    //    // print("Loaded MaxScore :" + GameData.ServerData.maxScore);
    //}


    public string UpdateLeaderboard()
    {
        var obj = gameData.ServerData;
        // To save dataPersistence data in gameData;
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.SaveData(ref obj);
        }
        // print("Saved MaxScore :" + GameData.ServerData.maxScore);

        // To convert saved gameData to json format and store in server
        fileDataHandler.SaveToServer(GameData);
        return "save suceed";
    }

    public string SaveGame()
    {
        FindAllDataPersistenceObjects();
        ClientData clientData;
        gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        clientData.currentProfileData = new ProfileData();
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.SaveData(ref gameData);
        }
        // print("Saved MaxScore :" + GameData.ServerData.maxScore);
        fileDataHandler.SaveToClient(GameData);
        fileDataHandler.SaveToServer(GameData);
        return "save suceed";
    }

    public void Restart()
    {
        if(GameDataManager.instance.MaxScore > gameData.ServerData.Leaderboard.Last().score)
        {
            gameData.ServerData.Leaderboard.Last().score = GameDataManager.instance.MaxScore;
            fileDataHandler.SaveToServer(gameData);
        }
        resetCurrentProfileData();
        SceneManager.LoadScene("LoadScene");
    }

    public void resetCurrentProfileData()
    {
        ClientData clientData;
        gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        clientData.currentProfileData.score = 0;
        clientData.currentProfileData.flipX = false;
        clientData.currentProfileData.playerPosition = Vector3.zero;
        clientData.currentProfileData.monsters = new List<MonsterData>();
    }

    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceList = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        this.dataPersistenceList = new List<IDataPersistence>(dataPersistenceList);
    }

    //public Dictionary<string, ClientDatas> GetAllProfilesGameData(string playerName)
    //{
    //    return fileDataHandler.LoadAllProfilesFromClient(playerName);
    //}
}

