using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;

// Service : To covert object to json format and store in the Client or Server .
public class FileDataHandler
{
    //[DllImport("__Internal")]
    //private static extern void SyncFiles();

    //[DllImport("__Internal")]
    //private static extern void WindowAlert(string message);

    private string dataDirPath = "";
    private string dataFileName = "";
    private string ServerDataFullPath = Path.Combine(Directory.GetCurrentDirectory(),"ServerData", "ServerData.json");

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public ClientData LoadFromClient()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        ClientData loadedData = null;
        if (File.Exists(fullPath))
        {
           loadedData = new ClientData();
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                    loadedData = JsonUtility.FromJson<ClientData>(dataToLoad);
                }
            }
            catch (Exception ex)
            {
               // PlatformSafeMessage("Error occured when trying to load file to data: " + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    internal static GameData LoadFromClient(string text)
    {
        throw new NotImplementedException();
    }

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
                    }
                    loadedData = JsonUtility.FromJson<ServerData>(dataToLoad);
                }
               // PlatformSafeMessage("load at: " + ServerDataFullPath);
            }
            catch (Exception ex)
            {
               // PlatformSafeMessage("Error occured when trying to load file to data: " + ServerDataFullPath + "\n" + ex);
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
            string clientData = JsonUtility.ToJson(data.ClientData, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(clientData);
                }
            }
            //if (Application.platform == RuntimePlatform.WebGLPlayer)
            //{
            //    SyncFiles();
            //    PlatformSafeMessage("Save at Client: " + fullPath);
            //}
        } catch (Exception ex)
        {
          //  PlatformSafeMessage("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
        }
    }

    public void SaveToServer(GameData data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ServerDataFullPath));
            string serverData = JsonUtility.ToJson(data.ServerData, true);
            using (FileStream stream = new FileStream(ServerDataFullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(serverData);
                }
            }
        //    PlatformSafeMessage("Save at server: " + ServerDataFullPath);
        }
        catch (Exception ex)
        {
        //    PlatformSafeMessage("Error occured when trying to save data to file: " + ServerDataFullPath + "\n" + ex);
        }
    }

    //private static void PlatformSafeMessage(string message)
    //{
    //    if (Application.platform == RuntimePlatform.WebGLPlayer)
    //    {
    //      //  WindowAlert(message);
    //    }
    //    else
    //    {
    //        Debug.Log(message);
    //    }
    //}
}
