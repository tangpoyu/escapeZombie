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
        FindAllDataPersistenceObjects();
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manger in the scene");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, filename);
        GameData = new GameData();
    }

    internal void EnterGame(string text)
    {
        GameData gameData = FileDataHandler.LoadFromClient(text);
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void NewGame()
    {
        GameData = new GameData();
        ServerData serverData = fileDataHandler.LoadFromServer();
        if (serverData == null) return;
        GameData.ServerData = serverData;
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadData(GameData.ServerData);
        }
    }

    public void LoadGame()
    {
        ClientData clientData = fileDataHandler.LoadFromClient();
        if (clientData == null)
        {
            NewGame();
        }else
        {
            this.GameData.ClientData = clientData;
            this.GameData.ServerData = fileDataHandler.LoadFromServer();
            foreach (IDataPersistence dataPersistence in dataPersistenceList)
            {
                dataPersistence.LoadData(GameData);
            }
        }
        // print("Loaded MaxScore :" + GameData.ServerData.maxScore);
    }


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
        foreach(IDataPersistence dataPersistence in dataPersistenceList)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        int charIndex = GameDataManager.instance.CharIndex;
        string playerName = GameDataManager.instance.PlayerName;
        GameData = new GameData();
        GameData.ClientData.ScoreRecord.charIndex = charIndex;
        GameData.ClientData.ScoreRecord.playerName = playerName;
        ServerData serverData = fileDataHandler.LoadFromServer();
        if (serverData != null) GameData.ServerData = serverData;
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadData(GameData);
        }
    }


    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceList = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        this.dataPersistenceList = new List<IDataPersistence>(dataPersistenceList);
    }
}

