using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

// Service : To covert object to json format and store in the Client or Server .
public class FileDataHandler
{
    // For WebGL
    [DllImport("__Internal")]
    private static extern void SyncFiles();
    [DllImport("__Internal")]
    private static extern void WindowAlert(string message);

    private string dataDirPath = "";
    private string dataFileName = "";
    private string ServerDataFullPath = Path.Combine(Directory.GetCurrentDirectory(),"ServerData", "ServerData.json");
    private bool useEncryption = false;
    private readonly string encryptionCode = "zombieEncryption";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public ClientDatas LoadFromClient()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        ClientDatas loadedData = null;
        if (File.Exists(fullPath))
        {
           loadedData = new ClientDatas();
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                        // optionally decrypt the data
                        if (useEncryption)
                        {
                            dataToLoad = EncryptDecrypt(dataToLoad);
                        }
                    }
                    loadedData = JsonUtility.FromJson<ClientDatas>(dataToLoad);
                }
            }
            catch (Exception ex)
            { 
                PlatformSafeMessage("Error occured when trying to load data from Client: " + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    //public Dictionary<string, ProfileData> LoadAllProfilesFromClient(string playerName)
    //{
    //    Dictionary<string, ProfileData> result = new Dictionary<string, ProfileData>();
    //    IEnumerable<DirectoryInfo> dirInfos = null;
    //    try
    //    {
    //        dirInfos = new DirectoryInfo(Path.Combine(dataDirPath, playerName)).EnumerateDirectories();
    //    }catch (Exception ex)
    //    {
    //        return result;
    //    }
        
    //    foreach (DirectoryInfo dirInfo in dirInfos)
    //    {
    //        string profileId = dirInfo.Name;
    //        string fullPath = Path.Combine(dataDirPath, playerName, profileId);
    //        if (!File.Exists(fullPath)) continue;
    //        ClientData data = LoadFromClient(playerName, profileId);
    //        if (profileId != null)
    //        {
    //            result.Add(profileId, data.);
    //        }
    //        else
    //        {
    //            Debug.LogError("Tried to load profile but something went wrond. " + playerName + ", " + profileId);
    //        }
    //    }

    //    return result;
    //}


    public ServerData LoadFromServer()
    {
        ServerData loadedData = null;
        if (File.Exists(ServerDataFullPath))
        {
            loadedData = new ServerData();
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(ServerDataFullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                        // optionally decrypt the data
                        if (useEncryption)
                        {
                            dataToLoad = EncryptDecrypt(dataToLoad);
                        }
                    }
                    loadedData = JsonUtility.FromJson<ServerData>(dataToLoad);
                }
           
            }
            catch (Exception ex)
            {
                // For WebGL
                PlatformSafeMessage("Error occured when trying to load data from server: " + ServerDataFullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    public void SaveToClient(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string clientData = JsonUtility.ToJson(data.ClientDatas, true);

            // optionally encrypt the data
            if (useEncryption)
            {
                clientData = EncryptDecrypt(clientData);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(clientData);
                }
            }

            // For WebGL
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
                PlatformSafeMessage("Save at Client: " + fullPath);
            }
        }
        catch (Exception ex)
        {
            //For WebGL
              PlatformSafeMessage("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
        }
    }

    public void SaveToServer(GameData data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ServerDataFullPath));
            string serverData = JsonUtility.ToJson(data.ServerData, true);

            // optionally encrypt the data
            if (useEncryption)
            {
                serverData = EncryptDecrypt(serverData);
            }

            using (FileStream stream = new FileStream(ServerDataFullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(serverData);
                }
            }

            // For WebGL
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
                PlatformSafeMessage("Save at Server: " + ServerDataFullPath);
            }

        }
        catch (Exception ex)
        {
           // For WebGL
                PlatformSafeMessage("Error occured when trying to save data to file: " + ServerDataFullPath + "\n" + ex);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        foreach(var a in data.Select((value, index) => new {value, index}))
        {
            modifiedData += (char)(a.value ^ encryptionCode[a.index % encryptionCode.Length]);
        }

        return modifiedData;
    }


    // For WebGL
    private static void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WindowAlert(message);
        }
        else
        {
            Debug.Log(message);
        }
    }
}
