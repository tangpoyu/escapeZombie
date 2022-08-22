using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawner : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftpos, rightpos;

    private int randomIndex;
    private int randomSide;

    private List<MonsterData> monsters;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "SampleScene")
        {
            StartCoroutine(SpawnMonsters());
        }
    }


    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
       
        foreach (MonsterData monster in monsters)
        {
            GameObject m = Instantiate(monsterReference[monster.Type]);
            m.GetComponent<Monster>().Type = monster.Type;
            m.GetComponent<SpriteRenderer>().flipX = monster.flipX;
            m.transform.position = monster.monsterPosition;
            m.GetComponent<Monster>().Speed = monster.speed;
        }
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Additive);
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);
            spawnedMonster = Instantiate(monsterReference[randomIndex]);
            spawnedMonster.GetComponent<Monster>().Type = randomIndex;

            if (randomSide == 0)
            {
                spawnedMonster.transform.position = leftpos.position;
                spawnedMonster.GetComponent<Monster>().Speed = Random.Range(4, 10);
                spawnedMonster.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                spawnedMonster.transform.position = rightpos.position;
                spawnedMonster.GetComponent<Monster>().Speed = -Random.Range(4, 10);
                spawnedMonster.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void LoadData(GameData gameData)
    {
        LoadData(gameData.ClientDatas);
    }

    public void LoadData(ClientDatas clientDatas)
    {
        ClientData clientData = null;
        clientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        this.monsters = clientData.currentProfileData.monsters;
    }

    public void LoadData(ClientData clientData)
    {

    }

    public void LoadData(ServerData serverData)
    {
        
    }

    public void SaveData(ref GameData gameData)
    {
        
    }

    public void SaveData(ref ClientDatas clientDatas)
    {
        
    }

    public void SaveData(ref ServerData serverData)
    {

    }
}
