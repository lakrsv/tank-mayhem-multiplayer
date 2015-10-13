using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour {
    public Material OwnerMaterial;

	// Use this for initialization
	void Start () {
        if (transform.root.GetComponent<PhotonView>().isMine)
        {
            gameObject.GetComponent<Renderer>().material = OwnerMaterial;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
