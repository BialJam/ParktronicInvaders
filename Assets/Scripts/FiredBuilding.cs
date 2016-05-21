using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FiredBuilding : MonoBehaviour {


    public bool isFired;
    public bool isGoingToFire;
    float firingTime;
    ParticleSystem particles;
    public List<GameObject> humans;
    public List<GameObject> buildings;
    float goingFire;

    // Use this for initialization
    void Start () {
        humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));
        buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Building"));
        goingFire = 5f;
        isGoingToFire = false;
        particles = GetComponent<ParticleSystem>();
        isFired = false;
        firingTime = 10f;
        particles.Pause();
    }

	// Update is called once per frame
	void Update () {
        if(isGoingToFire)
        {
            goingFire -= Time.deltaTime;
            if (goingFire <= 0)
            {
                isFired = true;
            }
        }

	    if(isFired)
        {
            if(particles != null) particles.Play();
            firingTime -= Time.deltaTime;
            if (firingTime <= 0) Destroy(this.gameObject);

            foreach(var human in humans)
            {
                if (human != null)
                {
                    if (Vector3.Distance(transform.position, human.transform.position) <= 20f)
                    {
                        human.GetComponent<DotStatistics>().StartFire();
                    }
                }
            }

            foreach(var building in buildings)
            {
                if (building != null)
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <= 20f)
                    {
                        building.GetComponent<FiredBuilding>().isGoingToFire = true;
                    }
                }
            }
        }
	}

   /* void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Human" && isFired && coll.gameObject.GetComponent<DotStatistics>().isBurning == false)
        {
            Debug.Log("Podaj sciezke do skryptu i co uruchomic");
            coll.gameObject.GetComponent<DotStatistics>().StartFire();
        }

        if(coll.gameObject.tag == "Building" && isFired)
        {
            coll.gameObject.GetComponent<FiredBuilding>().isGoingToFire = true;
        }

    }
    */
}
