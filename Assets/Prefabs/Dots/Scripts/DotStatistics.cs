using UnityEngine;
using System.Collections;

public class DotStatistics : MonoBehaviour
{
    public int attackDamage;
    public float attackCooldown;
    public int health;
    public float movementSpeed;
    public int maxHealth;
    bool isPoisoned;
    bool exploding;
    public GameObject dotController;

    public bool isBurning;
    float burningTimer;
    public float poisonTimer;
    public float goingToPoison;

    public ParticleSystem fireParticles;
    void Start()
    {
        goingToPoison = 3f;
        poisonTimer = 0;
        GetComponent<ParticleSystem>().Pause();
        exploding = false;
        fireParticles.Pause();
        isBurning = false;
        burningTimer = 0f;
        isPoisoned = false;
        attackDamage = (int)(Random.Range(0.3f, 0.5f) * 100f);
        attackCooldown = Random.Range(1f, 1.5f);
        health = (int)(Random.Range(1.2f, 1.7f) * 100f);
        maxHealth = health;
        movementSpeed = Random.Range(1f, 1.2f);
    }

    public void Update()
    {
        if (exploding) GetComponentInChildren<ParticleSystem>().startSpeed += 1f;
        if (isBurning)
        {
            SetOnFireOthers();
            burningTimer += Time.deltaTime;
            if (burningTimer > 1f)
            {
                burningTimer = 0f;
                ApplyDamage(20, null);
            }
        }


        if (isPoisoned && GetComponent<StatusController>().currentStatus != StatusController.DotStatus.Zombie)
        {
            GetComponent<StatusController>().ChangeStatus(StatusController.DotStatus.Poisoned);
            poisonTimer += Time.deltaTime;
            if (poisonTimer > 5f)
            {
                isPoisoned = false;
                poisonTimer = 0f;
                ChangeToZombie();
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void ChangeToZombie()
    {
        GetComponent<StatusController>().ChangeStatus(StatusController.DotStatus.Zombie);
        dotController = GameObject.Find("DotController");
        if (dotController.GetComponent<DotController>().DotInControl == gameObject)
            dotController.GetComponent<DotController>().DotInControl = null;


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
        GetComponent<ParticleSystem>().Play();
        exploding = true;

        var humans = GameObject.FindGameObjectsWithTag("Human");

        foreach (var item in humans)
        {
            if (Vector3.Distance(transform.position, item.transform.position) <= 10f)
                item.GetComponent<DotStatistics>().ApplyDamage((int)((1f - Vector3.Distance(transform.position, item.transform.position) / 10f) * 100), null);
        }
        SetOnFireOthers();

        Destroy(gameObject, 0.5f);
    }

    public void ApplyDamage(int amount, GameObject whoAttacked)
    {
        health -= amount;

        if (health <= 0)
        {
            if (whoAttacked != null && whoAttacked.GetComponent<StatusController>().currentStatus == StatusController.DotStatus.Zombie)
            {
                health = maxHealth;
                float chance = Random.Range(0f, 1f);
                if (chance > 0.7f)
                    ChangeToZombie();
                else
                    Destroy(gameObject, 0.1f);
                maxHealth = health;
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }

    void SetOnFireOthers()
    {
        var humans = GameObject.FindGameObjectsWithTag("Human");

        foreach (var item in humans)
        {
            if (Vector3.Distance(transform.position, item.transform.position) <= 5f
                && item.GetComponent<DotStatistics>().isBurning == false && item != gameObject)
                item.GetComponent<DotStatistics>().StartFire();
        }

        var buildings = GameObject.FindGameObjectsWithTag("Building");

        foreach (var item in buildings)
        {
            if (Vector3.Distance(transform.position, item.transform.position) <= 15f
                && item.GetComponent<FiredBuilding>().isFired == false && item.GetComponent<FiredBuilding>().isGoingToFire == false && item != gameObject)
                item.GetComponent<FiredBuilding>().isGoingToFire = true;
        }
    }

    public void StartFire()
    {
        StartCoroutine(SetOnFire());
    }

    public IEnumerator SetOnFire()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameObject != null)
        {
            isBurning = true;
            fireParticles.Clear();
            fireParticles.Play();

            SetOnFireOthers();
        }
    }

    public void HammerTime()
    {
        attackDamage *= 2;
    }


    public void StartPoison()
    {
        isPoisoned = true;
        goingToPoison = 3f;
    }

    public void StopBurning()
    {
        isBurning = false;
        fireParticles.Stop();
    }
}
