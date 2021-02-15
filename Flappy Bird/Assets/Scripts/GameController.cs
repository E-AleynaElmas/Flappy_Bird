using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject sky1;

    [SerializeField]
    GameObject sky2;

    [SerializeField]
    float backGroundSpeed;

    [SerializeField]
    GameObject barrier;

    [SerializeField]
    int barriercount;

    Rigidbody2D physics1;
    Rigidbody2D physics2;
    float skyLength;
    GameObject[] barriers;
    Rigidbody2D[] barriersRigidbody;
    float speed;
    int counter = 0;

    void Start()
    {
        physics1 = sky1.GetComponent<Rigidbody2D>();
        physics2 = sky2.GetComponent<Rigidbody2D>();

        physics1.velocity = new Vector2(-backGroundSpeed, 0);
        physics2.velocity = new Vector2(-backGroundSpeed, 0);

        skyLength = sky1.GetComponent<BoxCollider2D>().size.x;

        barriers = new GameObject[barriercount];

        for(int i = 0; i < barriercount; i++)
        {
            barriers[i] = Instantiate(barrier, new Vector2(-20, -20), Quaternion.identity);
            barriersRigidbody[i] = barriers[i].GetComponent<Rigidbody2D>();
            barriersRigidbody[i].velocity = new Vector2(-backGroundSpeed, 0);
        }
    }

    void Update()
    {
        if(sky1.transform.position.x <= -skyLength)
        {
            sky1.transform.position += new Vector3(skyLength * 2, 0);
        }
        if (sky2.transform.position.x <= -skyLength)
        {
            sky2.transform.position += new Vector3(skyLength * 2, 0);
        }
        //--------------------
        speed += Time.deltaTime;
        if (speed > 1.2f)
        {
            speed = 0;
            float verticalAxis = Random.Range(-0.5f, 1.10f);
            barriers[counter].transform.position = new Vector3(3, verticalAxis);
            counter++;
            if (counter >= barriers.Length)
            {
                counter = 0;
            }
        }
    }

    public void GameOver()
    {
        for(int i = 0; i < barriers.Length; i++)
        {
            barriersRigidbody[i].velocity = Vector2.zero;
            physics1.velocity = Vector2.zero;
            physics2.velocity = Vector2.zero;
        }
    }
}
