using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDataPersistence
{
    private int type;

    [HideInInspector]
    private float speed;
    private Rigidbody2D myBody;

    public float Speed { get => speed; set => speed = value; }
    public int Type { get => type; set => type = value; }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(Speed, myBody.velocity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
       
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
        
    }

    public void LoadData(ServerData serverData)
    {
       
    }

    public void SaveData(ref GameData gameData)
    {
        var obj = gameData.ClientData;
        SaveData(ref obj);
    }

    public void SaveData(ref ClientData clientData)
    {
        clientData.monsters.Add(new MonsterData(Type,transform.position));
    }

    public void SaveData(ref ServerData serverData)
    {
        
    }
}
