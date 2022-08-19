using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters;
   
    private void Awake()
    {
        Instantiate(characters[GameDataManager.instance.CharIndex]);
    }

}
