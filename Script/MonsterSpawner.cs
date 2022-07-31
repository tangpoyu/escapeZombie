using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftpos, rightpos;

    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);
            spawnedMonster = Instantiate(monsterReference[randomIndex]);

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
}
