using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    // To load coverted Game Data in the GameDataManager.
    void LoadData(GameData gameData);
    // To load coverted Client Data in the GameDataManager.
    void LoadData(ClientDatas clientDatas);
    void LoadData(ClientData clientData);
    // To load coverted Server Data in the GameDataManager.
    void LoadData(ServerData serverData);
    // To save GameDataManager Data in the Game Data which will be converted to different data format and stored in the client or Server.
    void SaveData(ref GameData gameData);
    // To save GameDataManager Data in the Client Data which will be converted to different data format and stored in the client or Server.
    void SaveData(ref ClientDatas clientDatas);
    // To save GameDataManager Data in the Server Data which will be converted to different data format and stored in the client or Server.
    void SaveData(ref ServerData serverData);
}
