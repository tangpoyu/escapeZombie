using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector]
    private float speed;
    private Rigidbody2D myBody;

    public float Speed { get => speed; set => speed = value; }

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
}
