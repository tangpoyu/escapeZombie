using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int Score;
    private int MaxScore;

    public int Score1 { get => Score; set => Score = value; }
    public int MaxScore1 { get => MaxScore; set => MaxScore = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            Score++;
            if (Score >= MaxScore)
            {
                MaxScore = Score;
            }
        }
        else
        {
            Score = 0;
        }
    }
}
