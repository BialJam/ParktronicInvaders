using UnityEngine;
using System.Collections;

public class DotStatistics : MonoBehaviour
{
    public int attackDamage;
    public float attackCooldown;
    public int health;
    public float movementSpeed;

    bool isBurning;
    float burningTimer;

    void Start()
    {
        isBurning = false;
        burningTimer = 0f;
        isPoisoned = false;
        attackDamage = (int)(Random.Range(0.3f, 0.5f) * 100f);
        attackCooldown = Random.Range(1f, 1.5f);
        health = (int)(Random.Range(1.2f, 1.7f) * 100f);
        movementSpeed = Random.Range(1f, 1.2f);
    }

    public void Update()
    {
        if(isBurning)
        {
            burningTimer += Time.deltaTime;
            if (burningTimer > 1f)
            {
                burningTimer = 0f;
                ApplyDamage(20);
            }
        }


        if(isPoisoned && GetComponent<StatusController>().currentStatus != StatusController.DotStatus.Zombie)
        {
            poisonTimer += Time.deltaTime;
            if(poisonTimer>10f)
            {
                isPoisoned = false;
                poisonTimer = 0f;
                GetComponent<StatusController>().ChangeStatus(StatusController.DotStatus.Zombie);
            }
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void ChangeToZombie()
    {
        GetComponent<StatusController>().ChangeStatus(StatusController.DotStatus.Zombie);
        attackDamage *= 15;
        attackDamage /= 10;
        attackCooldown *= 15;
        attackCooldown /= 10;
        health *= 15;
        health /= 10;
        movementSpeed *= 10;
        movementSpeed /= 15;
    }

    public void Explode()
    {
        //TODO: explosion
        GetComponentInChildren<Collider2D>().enabled = true;
        Destroy(gameObject, 0.5f);
    }

    public void ApplyDamage(int amount)
    {
        health -= amount;
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }

    public void SetOnFire()
    {
        isBurning = true;
        //TODO: burning particles
    }


    bool isPoisoned;
    float poisonTimer = 0f;
    public void StartPoison()
    {
        isPoisoned = true;
    }
}
