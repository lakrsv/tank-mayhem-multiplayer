using UnityEngine;
using System.Collections;

public class JointbreakOnline : Photon.MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (transform.root.GetComponent<PhotonView>().isMine)
        {
            if (!transform.root.name.Contains("Hammer"))
            {
                GetComponent<HingeJoint>().breakTorque = 100;
                GetComponent<HingeJoint>().breakForce = 100;
            }
            else if (transform.root.name.Contains("Hammer"))
            {
                GetComponent<HingeJoint>().breakTorque = Mathf.Infinity;
                GetComponent<HingeJoint>().breakForce = Mathf.Infinity;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
