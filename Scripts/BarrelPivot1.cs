using UnityEngine;
using System.Collections;

public class BarrelPivot1 : MonoBehaviour {
    public float MinYRot;
    public float MaxYRot;
    public float InputV;
    public int PlayerID;

	// Use this for initialization
	void Start () {
        MinYRot = 100;
        MaxYRot = 190;
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
	}
	
	// Update is called once per frame
	void Update () {
        InputV = Input.GetAxis("VerticalRight" + PlayerID);
            //Debug.Log("Moving Barrel!");
            if (InputV == 1 && transform.localRotation.eulerAngles.y < MaxYRot)
            {
                transform.Rotate(Vector3.up, InputV * Time.deltaTime * 20);
            }
            if (InputV == -1 && transform.localRotation.eulerAngles.y > MinYRot)
            {
                transform.Rotate(Vector3.up, InputV * Time.deltaTime * 20);
            }
  
        }
	}

