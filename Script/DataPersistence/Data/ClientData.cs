using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClientData
{ 
    public ScoreRecord ScoreRecord;
    public Vector3 playerPosition;
    public List<MonsterData> monsters;

    public ClientData()
    {
        ScoreRecord = new ScoreRecord();
        playerPosition = Vector3.zero;
        monsters = new List<MonsterData>();
    }
}
