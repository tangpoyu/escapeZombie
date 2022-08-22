using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// Service : To hold GamePlayUI Data
public class GameDataManager : MonoBehaviour, IDataPersistence
{
    public static GameDataManager instance;
    private string profileId;
    private string playerName;
    private int charIndex;
    private int score;
  
    private int maxScore = 0;

    public string ProfileId { get => profileId; set => profileId = value; }
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
            StartCoroutine(AddScore());
        }
    }

    public void LoadData(GameData gameData)
    {
        LoadData(gameData.ClientDatas);
        LoadData(gameData.ServerData);
    }
    public void LoadData(ClientDatas clientDatas)
    {

        ClientData clientData = null;
        clientDatas._clientDatas.TryGetValue(playerName, out clientData);
        profileId = clientData.currentProfileData.profileId;
        CharIndex = clientData.currentProfileData.charIndex;
        Score = clientData.currentProfileData.score;
    }

    public void LoadData(ClientData clientData)
    {
        playerName = clientData.playname;
    }

    public void LoadData(ServerData serverData)
    {
        if(serverData.Leaderboard.Count > 0)
        {
            int maxScore = 0;
            foreach (ScoreRecord record in serverData.Leaderboard)
            {
                if (record.score > maxScore) maxScore = record.score;
            }
            this.maxScore = maxScore;
        }
    }
    public void SaveData(ref GameData gameData)
    {
        var client = gameData.ClientDatas;
        SaveData(ref client);
        var server = gameData.ServerData;
        SaveData(ref server);
    }

    public void SaveData(ref ClientDatas clientDatas)
    {
        ClientData clientData = null;
        clientDatas._clientDatas.TryGetValue(playerName,out clientData);
        clientData.playname = playerName;
        clientData.currentProfileData.profileId = profileId;
        clientData.currentProfileData.charIndex = CharIndex;
        clientData.currentProfileData.score = Score;
        if (clientData.savedProfileData.ContainsKey(profileId)) clientData.savedProfileData[profileId] = clientData.currentProfileData;
        else clientData.savedProfileData.Add(profileId, clientData.currentProfileData);
    }

    public void SaveData(ref ServerData serverData)
    {
        ScoreRecord scoreRecord = new ScoreRecord(playerName, CharIndex, Score);
        if (serverData.Leaderboard.Count > 0)
        {
            foreach (ScoreRecord record in serverData.Leaderboard)
            {
                if (record.playerName == playerName)
                {
                    if (Score > record.score)
                    {
                        record.score = Score;
                        record.charIndex = CharIndex;
                    }
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



