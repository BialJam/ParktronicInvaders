using UnityEngine;
using System.Collections;

public class PoisonedLake : MonoBehaviour
{
    public bool isPoisoned;

    // Use this for initialization
    void Start()
    {
        isPoisoned = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Human")
        {
            coll.gameObject.GetComponent<DotStatistics>().StartPoison();
        }
    }
}