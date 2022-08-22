using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ScoreRecord : Data
{
    public string playerName;
    public int charIndex;
    public int score;

    public ScoreRecord(string playerName, int charIndex, int score)
    {
        this.playerName = playerName;
        this.charIndex = charIndex;
        this.score = score;
    }
}
