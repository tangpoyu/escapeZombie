using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterData
{
    public int Type;
    public Vector3 monsterPosition;
 

    public MonsterData()
    {
        Type = 0;
        monsterPosition = Vector3.zero;
    }

    public MonsterData(int type, Vector3 position1)
    {
        Type = type;
        monsterPosition = position1;
    }
}
