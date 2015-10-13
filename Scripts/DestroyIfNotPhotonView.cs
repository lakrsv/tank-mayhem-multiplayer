using UnityEngine;
using System.Collections;

public class DestroyIfNotPhotonView : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!transform.root.gameObject.GetComponent<PhotonView>().isMine)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
