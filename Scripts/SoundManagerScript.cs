using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {
    public AudioClip[] AudioClips;
    public GameObject[] PlayerTanks;

	// Use this for initialization
	void Start () 
    {
        GetComponent<AudioSource>().volume = 1;
        PlayerTanks = GameObject.FindGameObjectsWithTag("Tank");
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
}
