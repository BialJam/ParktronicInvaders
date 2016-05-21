using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySearching : MonoBehaviour
{

    List<GameObject> humans;
    int indexMin;
    float minimumDistance;
    StatusController status;
    StatusController.DotStatus dotStatus;
    public HumansMovement humansMovement;

    [HideInInspector]
    public bool isUsingGrid;

    // Use this for initialization
    void Start()
    {
        isUsingGrid = true;
        status = GetComponent<StatusController>();
        humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));
    }

    // Update is called once per frame
    void Update()
    {
        humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));
        indexMin = -1;
        minimumDistance = 1000 * 1000 * 1000;
        if (status.currentStatus == StatusController.DotStatus.Zombie)
        {
            //Debug.Log(this.gameObject.name + " " + "Jest Zombie");
            for (int i = 0; i < humans.Count; i++)
            {
                dotStatus = humans[i].GetComponent<StatusController>().currentStatus;
                if (humans[i] != this)
                {
                    if (dotStatus != StatusController.DotStatus.Zombie && Vector2.Distance(transform.position, humans[i].transform.position) < minimumDistance)
                    {
                        var layerMask = (1 << 8);
                        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                            (humans[i].transform.position - transform.position).normalized,
                            Vector3.Distance(transform.position, humans[i].transform.position), layerMask);
                        if (hit.collider == null)
                        {
                            indexMin = i;
                            minimumDistance = Vector2.Distance(transform.position, humans[i].transform.position);
                        }
                    }
                }
            }
            //if (indexMin != -1) //Debug.Log("Zombie " + this.gameObject.name + "znalazl najblizszy cel " + humans[indexMin].name);
            //else Debug.Log("Zombie nie znalazl partnerki");
        }
        else if (status.currentStatus == StatusController.DotStatus.Enemy)
        {
            for (int i = 0; i < humans.Count; i++)
            {
                var layerMask = (1 << 8);
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    (humans[i].transform.position - transform.position).normalized,
                    Vector3.Distance(transform.position, humans[i].transform.position), layerMask);
                if (hit.collider == null)
                {
                    dotStatus = humans[i].GetComponent<StatusController>().currentStatus;
                    if (dotStatus == StatusController.DotStatus.Controled)
                    {
                        indexMin = i;
                        minimumDistance = Vector2.Distance(transform.position, humans[i].transform.position);
                    }
                }
            }
        }
        else if (status.currentStatus == StatusController.DotStatus.Normal || status.currentStatus == StatusController.DotStatus.Poisoned)
        {
            for (int i = 0; i < humans.Count; i++)
            {
                var layerMask = (1 << 8);
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    (humans[i].transform.position - transform.position).normalized,
                    Vector3.Distance(transform.position, humans[i].transform.position), layerMask);
                if (hit.collider == null)
                {
                    dotStatus = humans[i].GetComponent<StatusController>().currentStatus;
                    if (dotStatus == StatusController.DotStatus.Zombie)
                    {
                        indexMin = i;
                        minimumDistance = Vector2.Distance(transform.position, humans[i].transform.position);
                    }
                }
            }
        }


        if (indexMin != -1)
        {
            isUsingGrid = false;

            //Debug.Log(this.gameObject.name + " Widzi przeciwnika " + humans[indexMin].name + " I idzie do niego");
            
            GetComponent<Rigidbody2D>().velocity =
                (Time.deltaTime * GetComponent<DotStatistics>().movementSpeed
                * (humans[indexMin].transform.position - transform.position).normalized
                * 100f);
            if (gameObject.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Enemy) GetComponent<Rigidbody2D>().velocity *= 0.5f;
         }
        else
        {
            //Debug.Log("Zombie niech wraca na siatke");
            isUsingGrid = true;
            
            humansMovement.SetNearestCheckpointAsTarget(gameObject);
            //Debug.Log("BUILDING DETECTED");
        }
    }


}
