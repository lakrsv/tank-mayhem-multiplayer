using UnityEngine;
using System.Collections;

public class HammerHitBox : Photon.MonoBehaviour {
    public int myActorID;

	// Use this for initialization
	void Start () {
        if (transform.root.gameObject.GetComponent<PhotonView>().isMine)
        {
            myActorID = PhotonNetwork.player.ID;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Belts")
        {
            PhotonView HitPhotonView = other.transform.root.gameObject.GetComponent<PhotonView>();
            if (HitPhotonView.isMine)
            {
                HitPhotonView.RPC("PlayerHitByHammer", PhotonTargets.All);
            }
        }
    }
}
