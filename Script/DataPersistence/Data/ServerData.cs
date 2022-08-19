using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ServerData
{
    public List<ScoreRecord> Leaderboard;

    public ServerData()
    {
        Leaderboard = new List<ScoreRecord>();
    }
}
