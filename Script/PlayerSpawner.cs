using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject[] characters;
    private Vector3 position;
    private bool flipX;

    public void LoadData(GameData gameData)
    {
        ClientData clientData = null;
        gameData.ClientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        position = clientData.currentProfileData.playerPosition;
        flipX = clientData.currentProfileData.flipX;
    }

    public void LoadData(ClientDatas clientDatas)
    {
        throw new System.NotImplementedException();
    }

    public void LoadData(ClientData clientData)
    {
        throw new System.NotImplementedException();
    }

    public void LoadData(ServerData serverData)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData gameData)
    {
       
    }

    public void SaveData(ref ClientDatas clientDatas)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref ServerData serverData)
    {
       
    }

    private void Start()
    {
        GameObject m = Instantiate(characters[GameDataManager.instance.CharIndex]);
        m.transform.position = position;
        m.GetComponent<SpriteRenderer>().flipX = flipX;
    }
}
