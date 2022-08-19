using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ScoreRecord
{
   public string playerName;
   public int charIndex, score;

    public ScoreRecord()
    {
    }

    public ScoreRecord(string playerName,int charIndex, int score)
    {
        this.playerName = playerName;
        this.charIndex = charIndex;
        this.score = score;
    }
}
