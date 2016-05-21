using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {

    public DotStatistics dotStats;
    Slider slider;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();

	}
	
	// Update is called once per frame
	void Update () {
        slider.maxValue = dotStats.maxHealth;
        slider.value = dotStats.health;
	}
}
