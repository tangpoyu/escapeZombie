using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterData : Data
{
    public int Type;
    public Vector3 monsterPosition;
    public float speed;
    public bool flipX;

    public MonsterData()
    {
        Type = 0;
        monsterPosition = Vector3.zero;
        speed = 0;
        flipX = false;
    }

    public MonsterData(int type, Vector3 position1, float speed, bool flipX)
    {
        Type = type;
        monsterPosition = position1;
        this.speed = speed;
        this.flipX = flipX;
    }
}
