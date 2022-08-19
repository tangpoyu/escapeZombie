using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// Model
public class GameData
{
    private ServerData serverData;
    private ClientData clientData;

    public ServerData ServerData { get => serverData; set => serverData = value; }
    public ClientData ClientData { get => clientData; set => clientData = value; }

    public GameData()
    {
        ServerData = new ServerData();
        ClientData = new ClientData();
    }
}
