using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0); 
	}
}
