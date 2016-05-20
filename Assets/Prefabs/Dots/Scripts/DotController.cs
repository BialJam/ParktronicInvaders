using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour
{
    public GameObject DotInControl;
    new Rigidbody2D rigidbody2D;
    void Start()
    {
        //TODO: dodac kropke do kontroli na poczatek gry
        rigidbody2D = DotInControl.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody2D.AddForce(new Vector2(0, 1) * DotInControl.GetComponent<DotStatistics>().movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody2D.AddForce(new Vector2(0, -1) * DotInControl.GetComponent<DotStatistics>().movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.AddForce(new Vector2(-1, 0) * DotInControl.GetComponent<DotStatistics>().movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.AddForce(new Vector2(1, 0) * DotInControl.GetComponent<DotStatistics>().movementSpeed);
        }
    }
}
