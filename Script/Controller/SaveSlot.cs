using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profieId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent, hasDataContent, BG_SC;
    [SerializeField] private TextMeshProUGUI scoreText;

    private ProfileData profileData;


    public string ProfieId { get => profieId; set => profieId = value; }
    public ProfileData ProfileData { get => profileData; set => profileData = value; }


    public void SetData(ProfileData data)
    {
        if(data == null)
        {
            ProfileData = null;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            ProfileData = data;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            scoreText.text = "Score : " + data.score;
        }
    }

    public string GetProfileId()
    {
        return ProfieId; 
    }

    public void loadSaveSlotSavedGame()
    {
        GameDataManager.instance.ProfileId = profieId;
        if(DataPersistenceManager.instance.LoadGame(this))
        {
            BG_SC.SetActive(true);
        }
    }
}
