using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Human" || col.gameObject.tag == "Building")
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            int damage = (int)((distance / 4f) * 100);

            if(col.gameObject.tag == "Human")
            {
                col.gameObject.GetComponent<DotStatistics>().SetOnFire();
            }
            if (col.gameObject.tag == "Building")
            {
                //TODO: col.gameObject.GetComponent<FiredBuilding>().isFired = true;
            }
        }
    }
}
