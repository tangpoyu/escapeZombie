using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClientDatas : Data
{
    public SerializableDictionary<string, ClientData> _clientDatas = new SerializableDictionary<string, ClientData>();

    public ClientData ClientData
    {
        get => default;
        set
        {
        }
    }
}
