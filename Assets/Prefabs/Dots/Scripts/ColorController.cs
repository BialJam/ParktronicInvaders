using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour
{
    public Sprite PoisonedSprite;
    public Sprite NormalSprite;
    public Sprite ControledSprite;
    public Sprite ZombieSprite;
    public Sprite EnemySprite;

    void Start()
    {
        GetComponent< SpriteRenderer >().sprite = NormalSprite;
    }

    public void ChangeMaterial(StatusController.DotStatus status)
    {
        switch(status)
        {
            case StatusController.DotStatus.Controled:
               GetComponent<SpriteRenderer>().sprite = ControledSprite;

                break;
            case StatusController.DotStatus.Enemy:
                GetComponent<SpriteRenderer>().sprite = EnemySprite;

                break;
            case StatusController.DotStatus.Normal:
                GetComponent<SpriteRenderer>().sprite = NormalSprite;

                break;
            case StatusController.DotStatus.Poisoned:
                GetComponent<SpriteRenderer>().sprite = PoisonedSprite;

                break;
            case StatusController.DotStatus.Zombie:
                GetComponent<SpriteRenderer>().sprite = ZombieSprite;

                break;
        }
    }
}
