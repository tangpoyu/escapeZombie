
using UnityEngine;


public class CammaFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    [SerializeField]
    private float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
            return;
        tempPos = transform.position;
        try
        {
            tempPos.x = player.position.x;

            if (tempPos.x < minX)
            {
                tempPos.x = minX;
            }

            if (tempPos.x > maxX)
            {
                tempPos.x = maxX;
            }
        } catch (MissingReferenceException e)
        {

        }
  
        transform.position = tempPos;
        
    }

}
