using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumansMovement : MonoBehaviour
{
    List<GameObject> checkpoints;
    List<GameObject> humans;
    List<GameObject> targets;
    List<float> timers;
    List<bool> isBlockedMoving;
    List<Vector2> randomVectors;

    public List<List<GameObject>> possibleCheckpoints;

    public Transform leftBot;
    public Transform rightTop;

    public GameObject checkpointGameObject;
    public GameObject dotGameObject;

    Vector2 lewyDolny;
    Vector2 prawyGorny;
    public int GridValue;

    public static bool isRandom;
    public static int randomDifficult = 2;

    void Start()
    {
        if (isRandom)
        {
            lewyDolny = leftBot.position;
            prawyGorny = rightTop.position;
            for (int i = 0; i < (prawyGorny.x - lewyDolny.x); i += GridValue)
            {
                for (int j = 0; j < (prawyGorny.y - lewyDolny.y); j += GridValue)
                {
                    Instantiate(checkpointGameObject, new Vector2(lewyDolny.x + i, lewyDolny.y + j), new Quaternion(0, 0, 0, 0));
                }
            }
        }

        checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));
        targets = new List<GameObject>();
        possibleCheckpoints = new List<List<GameObject>>();

        for (int i = 0; i < checkpoints.Count; i++)
        {
            possibleCheckpoints.Add(new List<GameObject>());
            foreach (var target in checkpoints)
            {
                if (checkpoints[i] != target)
                {
                    var layerMask = (1 << 8);
                    RaycastHit2D hit = new RaycastHit2D();
                    hit = Physics2D.Raycast(checkpoints[i].transform.position,
                        (target.transform.position - checkpoints[i].transform.position).normalized,
                        Vector3.Distance(target.transform.position, checkpoints[i].transform.position), layerMask);

                    if (hit.collider == null)
                    {
                        //Debug.Log(checkpoints[i].name + "  " + target.name);

                        possibleCheckpoints[i].Add(target);
                    }
                    //else
                    //Debug.Log(hit.collider.tag);
                }
            }
        }

        for (int i = 0; i < possibleCheckpoints.Count; i++)
        {
            while (possibleCheckpoints[i].Count == 0)
            {
                checkpoints.RemoveAt(i);
                possibleCheckpoints.RemoveAt(i);
            }
        }

        if (isRandom)
        {
            int number = checkpoints.Count;
            switch (randomDifficult)
            {
                case 0:
                    number = (int)((float)number * 0.11f);
                    break;
                case 1:
                    number = (int)((float)number * 0.17f);
                    break;
                case 2:
                    number = (int)((float)number * 0.25f);
                    break;
            }
            List<int> nums = new List<int>();
            for (int i = 0; i < checkpoints.Count; i++)
            {
                nums.Add(i);
            }
            int x;
            for (int i = 0; i < number; i++)
            {
                x = Random.Range(0, nums.Count - 1);
                nums.RemoveAt(x);
                Instantiate(dotGameObject, checkpoints[x].transform.position, new Quaternion(0, 0, 0, 0));
            }
        }

        humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));

        GameObject tmpTarget = new GameObject();
        float minDistance = 1000000f;
        foreach (var human in humans)
        {
            human.GetComponent<EnemySearching>().humansMovement = this;

            tmpTarget = checkpoints[0];
            foreach (var checkpoint in checkpoints)
            {
                if (Vector3.Distance(human.transform.position, checkpoint.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(human.transform.position, checkpoint.transform.position);
                    tmpTarget = checkpoint;
                }
            }
            minDistance = 1000000f;
            targets.Add(tmpTarget);
        }



        timers = new List<float>();
        isBlockedMoving = new List<bool>();
        randomVectors = new List<Vector2>();

        foreach (var item in humans)
        {
            timers.Add(0f);
            isBlockedMoving.Add(false);
            randomVectors.Add(Vector2.zero);
        }


        //gameObject.GetComponent<EnemySearching>().isBeingControlled




        //for (int i = 0; i < possibleCheckpoints.Count; i++)
        //{
        //    Debug.Log(i + "   " + possibleCheckpoints[i].Count);
        //    if (possibleCheckpoints[i].Count == 0)
        //        Debug.Log(checkpoints[i].name);
        //}


        Object[] ES = FindObjectsOfType(typeof(FiredBuilding));
        foreach (var item in ES)
        {
            (item as FiredBuilding).OnStart();
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < humans.Count; i++)
        {
            if (humans[i] == null)
            {
                humans.RemoveAt(i);
                targets.RemoveAt(i);
                timers.RemoveAt(i);
                isBlockedMoving.RemoveAt(i);
                continue;
            }
            if (humans[i].GetComponent<EnemySearching>().isUsingGrid)
            {
                if (humans[i].GetComponent<StatusController>().currentStatus != StatusController.DotStatus.Controled)
                {
                    if (Vector3.Distance(humans[i].transform.position, targets[i].transform.position) < 0.6f)
                    {
                        int x = checkpoints.IndexOf(targets[i]);
                        targets[i] = possibleCheckpoints[x][Random.Range(0, possibleCheckpoints[x].Count - 1)];
                    }

                    if (humans[i].GetComponent<Rigidbody2D>().velocity.magnitude < 1f)
                    {
                        isBlockedMoving[i] = true;
                    }
                    else if (isBlockedMoving[i] == false)
                    {
                        humans[i].GetComponent<Rigidbody2D>().velocity =
                            (Time.deltaTime * humans[i].GetComponent<DotStatistics>().movementSpeed
                            * (targets[i].transform.position - humans[i].transform.position).normalized * 200f);
                    }
                }
            }
            else
            {
                isBlockedMoving[i] = false;
                timers[i] = 0f;
            }
        }


        for (int i = 0; i < humans.Count; i++)
        {
            if (isBlockedMoving[i])
            {
                if (timers[i] == 0f)
                {
                    randomVectors[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    randomVectors[i].Normalize();
                    timers[i] = 0.001f;
                }
                else if (timers[i] < 1 && timers[i] > 0)
                {
                    timers[i] += Time.deltaTime;
                    humans[i].GetComponent<Rigidbody2D>().velocity = (Time.deltaTime * humans[i].GetComponent<DotStatistics>().movementSpeed
                            * randomVectors[i] * 200f);
                }
                else if (timers[i] >= 1f)
                {
                    isBlockedMoving[i] = false;
                    timers[i] = 0f;
                }
            }
        }
    }

    public void SetNearestCheckpointAsTarget(GameObject anyone)
    {
        int x = humans.IndexOf(anyone);
        GameObject tmpTarget = checkpoints[0];
        float minDistance = 1000000f;

        foreach (var checkpoint in checkpoints)
        {
            if (Vector3.Distance(humans[x].transform.position, checkpoint.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(humans[x].transform.position, checkpoint.transform.position);
                tmpTarget = checkpoint;
            }
        }
        minDistance = 1000000f;
        targets.Add(tmpTarget);
    }

}
