using UnityEngine;
using System.Collections;

public class ParentScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.FindGameObjectWithTag("Tank").transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
