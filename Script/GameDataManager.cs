using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// Service : To hold GamePlayUI Data
public class GameDataManager : MonoBehaviour, IDataPersistence
{
    public static GameDataManager instance;
    private string playerName;
    private int charIndex;
    private int score;
  
    private int maxScore = 0;

    public string PlayerName { get => playerName; set => playerName = value; }
    public int CharIndex { get => charIndex; set => charIndex = value; }
    public int Score { get => score; set => score = value; }
    public int MaxScore { get => maxScore; set => maxScore = value; }

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
        if (scene.name == "SampleScene")
        {
            StopCoroutine(AddScore());
            StartCoroutine(AddScore());
        }
    }

    public void LoadData(GameData gameData)
    {
        LoadData(gameData.ClientData);
        LoadData(gameData.ServerData);
    }
    public void LoadData(ClientData clientData)
    {
        playerName = clientData.ScoreRecord.playerName;
        CharIndex = clientData.ScoreRecord.charIndex;
        Score = clientData.ScoreRecord.score;
    }

    public void LoadData(ServerData serverData)
    {
        if(serverData.Leaderboard.Count > 0)
        {
            MaxScore = serverData.Leaderboard.Last().score;
        }
    }
    public void SaveData(ref GameData gameData)
    {
        var client = gameData.ClientData;
        SaveData(ref client);
        var server = gameData.ServerData;
        SaveData(ref server);
    }

    public void SaveData(ref ClientData clientData)
    {
        clientData.ScoreRecord.playerName = playerName;
        clientData.ScoreRecord.score = Score;
        clientData.ScoreRecord.charIndex = CharIndex;
    }

    public void SaveData(ref ServerData serverData)
    {
        ScoreRecord scoreRecord = new ScoreRecord(playerName,CharIndex, Score);
        if (serverData.Leaderboard.Count > 0)
        {
            foreach (ScoreRecord record in serverData.Leaderboard)
            {
                if (score < record.score)
                {
                    serverData.Leaderboard.Insert(serverData.Leaderboard.IndexOf(record), scoreRecord);
                    return;
                }
            }
        }
        serverData.Leaderboard.Add(scoreRecord);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(10);
        if (GameObject.FindWithTag("Player") != null)
        {
            Score++;
            if (Score >= MaxScore)
            {
                MaxScore = Score;

            }
            StartCoroutine(AddScore());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}



