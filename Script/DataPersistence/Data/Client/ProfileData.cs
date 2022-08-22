using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProfileData : Data
{
    public string profileId;
    public int charIndex, score;
    public bool flipX;
    public Vector3 playerPosition;
    public List<MonsterData> monsters;

    public ProfileData()
    {
        this.profileId = "";
        this.charIndex = 0;
        this.score = 0;
        this.monsters = new List<MonsterData>();
    }

    public ProfileData(string profileId)
    {
        this.profileId = profileId;
        this.monsters = new List<MonsterData>();
    }

    public ProfileData(string profileId, int charIndex, int score, bool flipX, Vector3 playerPosition, List<MonsterData> monsters)
    {
        this.profileId = profileId;
        this.charIndex = charIndex;
        this.score = score;
        this.flipX = flipX;
        this.playerPosition = playerPosition;
        this.monsters = monsters;
    }


}
