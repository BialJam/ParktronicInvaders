using UnityEngine;
using System.Collections;

public class FiredBuilding : MonoBehaviour {


    public bool isFired;
    public bool isGoingToFire;
    float firingTime;
    ParticleSystem particles;

    float goingFire;

    // Use this for initialization
    void Start () {
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
            particles.Play();
            firingTime -= Time.deltaTime;
            if (firingTime <= 0) Destroy(this.gameObject);
        }
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Human" && isFired)
        {
            Debug.Log("Podaj sciezke do skryptu i co uruchomic");
            //coll.gameObject.GetComponent<DotStatistics>().isBurning = true;
        }

        if(coll.gameObject.tag == "Building" && isFired)
        {
            coll.gameObject.GetComponent<FiredBuilding>().isGoingToFire = true;
        }

    }
}
