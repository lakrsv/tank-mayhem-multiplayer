using UnityEngine;
using System.Collections;

public class AudioRPC : MonoBehaviour {
    public AudioClip TankForward, TurretHead, BulletFire, BulletHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [RPC]
    void Forward()
    {
        GetComponent<AudioSource>().clip = TankForward;
        GetComponent<AudioSource>().Play();
    }
    [RPC]
    void Head()
    {
        GetComponent<AudioSource>().clip = TurretHead;
        GetComponent<AudioSource>().Play();
    }
    [RPC]
    void Fire()
    {
        GetComponent<AudioSource>().clip = BulletFire;
        GetComponent<AudioSource>().Play();
    }
    [RPC]
    void Hit()
    {
        GetComponent<AudioSource>().clip = BulletHit;
        GetComponent<AudioSource>().Play();
    }
}
