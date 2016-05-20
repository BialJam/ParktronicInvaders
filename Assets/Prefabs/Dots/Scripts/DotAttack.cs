using UnityEngine;
using System.Collections;

public class DotAttack : MonoBehaviour
{
    bool canAttack;
    float attackTimer;
    void Start()
    {
        canAttack = true;
        attackTimer = 0f;
    }

    void Update()
    {
        if (canAttack == false)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= GetComponent<DotStatistics>().attackCooldown)
            {
                attackTimer = 0f;
                canAttack = true;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        if (GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Normal && col.gameObject.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Poisoned || col.gameObject.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Zombie)
        {
            col.gameObject.GetComponent<DotStatistics>().ApplyDamage(GetComponent<DotStatistics>().attackDamage);
        }
        else if (GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Enemy &&  col.gameObject.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Controled)
        {
            col.gameObject.GetComponent<DotStatistics>().ApplyDamage(GetComponent<DotStatistics>().attackDamage);
        }
        else if(GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Poisoned || col.gameObject.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Zombie)
        {
            col.gameObject.GetComponent<DotStatistics>().ApplyDamage(GetComponent<DotStatistics>().attackDamage);
        }
        else if (GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Zombie || col.gameObject.GetComponent<StatusController>().currentStatus != StatusController.DotStatus.Zombie)
        {
            col.gameObject.GetComponent<DotStatistics>().ApplyDamage(GetComponent<DotStatistics>().attackDamage);
        }
    }
}
