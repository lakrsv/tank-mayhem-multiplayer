using UnityEngine;
using System.Collections;

public class PlayerIDSet : MonoBehaviour {
    public int PlayerID;

	// Use this for initialization
	void Start () {
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
