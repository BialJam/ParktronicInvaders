using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour
{
    public GameObject DotInControl = null;
    public static int movesAvailable = 1;
    public bool isInstantiated = false;
    new Rigidbody2D rigidbody2D;
    public float multiply;
    void Start()
    {

    }

    public bool takingControl(GameObject human)
    {
        if (human.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Normal && movesAvailable > 0)
        {
            DotInControl = human;
            human.GetComponent<StatusController>().ChangeStatus(StatusController.DotStatus.Controled);
            Debug.Log("Taking Control");
            movesAvailable--;
            rigidbody2D = DotInControl.GetComponent<Rigidbody2D>();
            isInstantiated = true;
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if (DotInControl != null && rigidbody2D != null)
        {
            bool isMoved = false;
            Vector2 tmpvec = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                isMoved = true;
                tmpvec += new Vector2(0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                isMoved = true;
                tmpvec += new Vector2(0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                isMoved = true;
                tmpvec += new Vector2(-1, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                isMoved = true;
                tmpvec += new Vector2(1, 0);
            }

            if (isMoved == true)
            {
                tmpvec.Normalize();
                rigidbody2D.velocity = (tmpvec * multiply * DotInControl.GetComponent<DotStatistics>().movementSpeed * Time.deltaTime);
                rigidbody2D.AddForce(tmpvec * Time.deltaTime * 0.1f);
            }
            else
                rigidbody2D.velocity *= 0.95f;

        }
    }
}
