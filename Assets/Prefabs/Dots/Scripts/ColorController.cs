using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour
{
    public Material PoisonedMaterial;
    public Material NormalMaterial;
    public Material ControledMaterial;
    public Material ZombieMaterial;
    public Material EnemyMaterial;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = ZombieMaterial;
    }

    public void ChangeMaterial(StatusController.DotStatus status)
    {
        switch(status)
        {
            case StatusController.DotStatus.Controled:
                gameObject.GetComponent<MeshRenderer>().material = ControledMaterial;

                break;
            case StatusController.DotStatus.Enemy:
                gameObject.GetComponent<MeshRenderer>().material = EnemyMaterial;

                break;
            case StatusController.DotStatus.Normal:
                gameObject.GetComponent<MeshRenderer>().material = NormalMaterial;

                break;
            case StatusController.DotStatus.Poisoned:
                gameObject.GetComponent<MeshRenderer>().material = PoisonedMaterial;

                break;
            case StatusController.DotStatus.Zombie:
                gameObject.GetComponent<MeshRenderer>().material = ZombieMaterial;

                break;
        }
    }
}
