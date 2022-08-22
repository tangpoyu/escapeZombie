using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private Boolean isGrounded;
    private readonly string ENEMY_TAG = "ENEMY";

    private void Awake()
    {
        // add some extra function
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    private void AnimatePlayer()
    {
        if (movementX != 0)
        {
            if (movementX > 0) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
            animator.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            animator.SetBool(WALK_ANIMATION, false);
        }
        
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        Debug.Log("movementX IS : " + movementX);
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false; 
            myBody.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    public void LoadData(GameData gameData)
    {
       
    }

    public void LoadData(ClientDatas clientDatas)
    {
        ClientData clientData = null;
        clientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        this.transform.position = clientData.currentProfileData.playerPosition;
    }

    public void LoadData(ClientData clientData)
    {
        
    }

    public void LoadData(ServerData serverData)
    {
       
    }

    public void SaveData(ref GameData gameData)
    {
        var obj = gameData.ClientDatas;
        SaveData(ref obj);
    }

    public void SaveData(ref ClientDatas clientDatas)
    {
        ClientData clientData = null;
        clientDatas._clientDatas.TryGetValue(GameDataManager.instance.PlayerName, out clientData);
        clientData.currentProfileData.playerPosition = this.transform.position;
        clientData.currentProfileData.flipX = GetComponent<SpriteRenderer>().flipX;
    }

    public void SaveData(ref ServerData serverData)
    {
        
    }

  
}
