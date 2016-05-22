using UnityEngine;
using System.Collections;

public class PoisonedLake : MonoBehaviour
{
    public bool isPoisoned;
    public Sprite poisonedLake;
    public Sprite Lake;

    float poisonedTime;
    // Use this for initialization
    void Start()
    {
        poisonedTime = 5f;
        isPoisoned = false;
        GetComponent<SpriteRenderer>().sprite = Lake;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPoisoned)
        {
            poisonedTime -= Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = poisonedLake;
        }

        if (isPoisoned && poisonedTime <= 0)
        {
            poisonedTime = 5f;
            GetComponent<SpriteRenderer>().sprite = Lake;
            isPoisoned = false;      
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Lake")
        {
            coll.gameObject.GetComponent<PoisonedLake>().isPoisoned = true;
        }
        coll.gameObject.GetComponent<DotStatistics>().StopBurning();
        if (coll.gameObject.tag == "Human" && isPoisoned)
        {
            coll.gameObject.GetComponent<DotStatistics>().goingToPoison -= Time.deltaTime;
            if(coll.gameObject.GetComponent<DotStatistics>().goingToPoison <= 0)
            {
                coll.gameObject.GetComponent<DotStatistics>().StartPoison();
            }

        }
    }


}



