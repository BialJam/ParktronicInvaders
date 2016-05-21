using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class StatusController : MonoBehaviour
{
    public enum DotStatus
    {
        Normal,
        Controled,
        Zombie,
        Poisoned,
        Enemy
    }

    public DotStatus currentStatus;
    float EnemyDetectionDistance;

    void Start()
    {
        EnemyDetectionDistance = 15f;
        ChangeStatus(DotStatus.Normal);
    }

    void Update()
    {
        checkIfControledDied();
    }

    void checkIfControledDied()
    {
        if (currentStatus == StatusController.DotStatus.Enemy)
        {
            bool found = false;
            List<GameObject> humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));

            foreach (var human in humans)
            {
                if (human.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Controled) found = true;
            }

            if (found == false)
            {
                ChangeStatus(StatusController.DotStatus.Normal);
            }
        }
    }

    public void MakeEnemy()
    {
        ChangeStatus(StatusController.DotStatus.Enemy);
    }

    public void TagEnemies()
    {
        List<GameObject> humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));

        foreach(var human in humans)
        {
            if (human.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Normal)
            {
                var layerMask = (1 << 8);
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    (human.transform.position - transform.position).normalized, EnemyDetectionDistance
                    , layerMask);
                if (hit.collider == null && Vector2.Distance(human.transform.position, transform.position) <= EnemyDetectionDistance)
                {
                    human.GetComponent<StatusController>().MakeEnemy();
                }
            }
        }
    }


    public void ChangeStatus(DotStatus status)
    {
        currentStatus = status;
        
        GetComponent<ColorController>().ChangeMaterial(currentStatus);
    }
}
