using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// Model
public class GameData : Data
{
    private ServerData serverData;
    private ClientDatas clientDatas;

    public ServerData ServerData { get => serverData; set => serverData = value; }
    public ClientDatas ClientDatas { get => clientDatas; set => clientDatas = value; }

    public GameData()
    {
        ServerData = new ServerData();
        ClientDatas = new ClientDatas();
    }
}
