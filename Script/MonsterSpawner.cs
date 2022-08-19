using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //foreach (MonsterData monster in monsters)
        //{
        //    GameObject m = Instantiate(monsterReference[monster.Type], monster.position);
        //    m.GetComponent<Monster>().Type = monster.Type;
        //}
        StartCoroutine(SpawnMonsters());
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData gameData)
    {
        
    }

    public void LoadData(ClientData clientData)
    {
        this.monsters = clientData.monsters;
    }

    public void LoadData(ServerData serverData)
    {
        
    }

    public void SaveData(ref GameData gameData)
    {
        
    }

    public void SaveData(ref ClientData clientData)
    {
        
    }

    public void SaveData(ref ServerData serverData)
    {
        
    }
}
