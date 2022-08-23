using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField] GameObject scorePrefab;
    [SerializeField] Transform scores;

    public DataPersistenceManager DataPersistenceManager
    {
        get => default;
        set
        {
        }
    }

    public Score Score
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        Dictionary<string, int> leaderBoards = new Dictionary<string, int>();
        foreach(var record in  DataPersistenceManager.instance.GameData
                .ServerData.Leaderboard .Select((value, index) => new { value, index }))
        {
            leaderBoards.Add(record.value.playerName, record.value.score);
        }
        var sortedLeaderBoards = from entry in leaderBoards orderby entry.Value descending select entry;
        foreach(var record in sortedLeaderBoards.Select((value, index) => new { value, index }))
        {
            GameObject obj = Instantiate(scorePrefab);
            obj.GetComponent<Score>().setScore("#" + (record.index + 1), record.value.Key, record.value.Value + "");
            obj.transform.SetParent(scores);
            obj.transform.localScale = new Vector3(1, 1, 1);
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
}
