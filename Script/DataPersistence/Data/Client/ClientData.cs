using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClientData : Data
{
    public string playname;
    public ProfileData currentProfileData;
    public SerializableDictionary<string, ProfileData> savedProfileData;

    public ClientData()
    {
        playname = "";
        currentProfileData = new ProfileData();
        savedProfileData = new SerializableDictionary<string, ProfileData>();
    }

    public ClientData(string playname, ProfileData currentProfileData, SerializableDictionary<string, ProfileData> savedProfileData)
    {
        this.playname = playname;
        this.currentProfileData = currentProfileData;
        this.savedProfileData = savedProfileData;
    }
}
