using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject rank, name, score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScore(string rank, string name, string score)
    {
        this.rank.GetComponent<TMP_Text>().text = rank;
        this.name.GetComponent<TMP_Text>().text = name;
        this.score.GetComponent<TMP_Text>().text = score;
    }
}
