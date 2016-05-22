using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayConditions : MonoBehaviour
{
    List<GameObject> humans;
    List<PoisonedLake> poisonedLakes;
    List<FiredBuilding> firedBuildings;

    float timer = 0f;

    public void OnStart()
    {
        Time.timeScale = 1f;
        AfterCanvasController.currentScore = 0;

        humans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Human"));

        List<Object> lakes = new List<Object>(FindObjectsOfType(typeof(PoisonedLake)));
        poisonedLakes = new List<PoisonedLake>();

        for (int i = 0; i < lakes.Count; i++)
        {
            poisonedLakes.Add((lakes[i] as PoisonedLake));
        }

        List<Object> buildings = new List<Object>(FindObjectsOfType(typeof(FiredBuilding)));
        firedBuildings = new List<FiredBuilding>();

        for (int i = 0; i < buildings.Count; i++)
        {
            firedBuildings.Add((buildings[i] as FiredBuilding));
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            timer = 0f;

            int numberOfNormal = 0;
            int numberOfZombies = 0;
            int numberOfEnemy = 0;
            int numberOfPoisoned = 0;
            int numberOfControled = 0;
            for (int i = 0; i < humans.Count; i++)
            {
                {
                    if (humans[i] != null)
                    {
                        switch (humans[i].GetComponent<StatusController>().currentStatus)
                        {
                            case StatusController.DotStatus.Controled:
                                numberOfControled++;
                                break;
                            case StatusController.DotStatus.Enemy:
                                numberOfEnemy++;
                                break;
                            case StatusController.DotStatus.Normal:
                                numberOfNormal++;
                                break;
                            case StatusController.DotStatus.Poisoned:
                                numberOfPoisoned++;
                                break;
                            case StatusController.DotStatus.Zombie:
                                numberOfZombies++;
                                break;
                        }
                    }
                    else
                    {
                        humans.RemoveAt(i);
                        i--;
                    }
                }
            }
            Debug.Log("controled: " + numberOfControled);
            Debug.Log("enemy: " + numberOfEnemy);
            Debug.Log("normal: " + numberOfNormal);
            Debug.Log("poisoned: " + numberOfPoisoned);
            Debug.Log("zombie: " + numberOfZombies);

            //wygrana gdy nie ma poisoned, normal, enemy i mozemy 
            if (numberOfPoisoned == 0 && numberOfNormal == 0 && numberOfEnemy == 0)
            {
                AfterCanvasController.currentScore += 10000;
                Time.timeScale = 0f;
                (FindObjectOfType(typeof(InventoryPanel)) as InventoryPanel).czyWidoczne = true;

            }
            else if (numberOfPoisoned == 0 && numberOfEnemy == 0 && numberOfZombies == 0 && numberOfControled == 0
                && (FindObjectOfType(typeof(InventoryPanel)) as InventoryPanel).controlsLeft == 0)
            {
                int burningHumans = 0;
                int burningBuildings = 0;
                int poisonedLakesNum = 0;
                for (int i = 0; i < humans.Count; i++)
                {
                    if (humans[i] != null)
                    {
                        if (humans[i].GetComponent<DotStatistics>().isBurning)
                        {
                            burningHumans++;
                        }
                    }
                    else
                    {
                        humans.RemoveAt(i);
                        i--;
                    }
                }


                for (int i = 0; i < firedBuildings.Count; i++)
                {
                    if (firedBuildings[i] != null)
                    {
                        if (firedBuildings[i].isFired || firedBuildings[i].isGoingToFire)
                        {
                            burningBuildings++;
                        }
                    }
                    else
                    {
                        firedBuildings.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < poisonedLakes.Count; i++)
                {
                    if (poisonedLakes[i] != null)
                    {
                        if (poisonedLakes[i].isPoisoned)
                        {
                            poisonedLakesNum++;
                        }
                    }
                    else
                    {
                        poisonedLakes.RemoveAt(i);
                        i--;
                    }
                }


                if (burningHumans == 0 && burningBuildings == 0 && poisonedLakesNum == 0)
                {
                    AfterCanvasController.currentScore -= 10000;
                    Time.timeScale = 0f;
                    (FindObjectOfType(typeof(InventoryPanel)) as InventoryPanel).czyWidoczne = true;

                }
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndLevel()
    {
        SceneManager.LoadScene("AfterMisiion");
    }
}
