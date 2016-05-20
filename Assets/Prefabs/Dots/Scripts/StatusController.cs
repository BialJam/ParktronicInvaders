using UnityEngine;
using System.Collections;
using System;

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

    void Start()
    {
        ChangeStatus(DotStatus.Normal);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    ChangeStatus(DotStatus.Controled);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    ChangeStatus(DotStatus.Enemy);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ChangeStatus(DotStatus.Normal);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ChangeStatus(DotStatus.Poisoned);
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    ChangeStatus(DotStatus.Zombie);
        //}
    }

    public void ChangeStatus(DotStatus status)
    {
        currentStatus = status;
    }
}
