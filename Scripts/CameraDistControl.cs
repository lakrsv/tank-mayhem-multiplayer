using UnityEngine;
using System.Collections;

public class CameraDistControl : MonoBehaviour {
    public int Distance;

	// Use this for initialization
	void Start () {
        Distance = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Distance--;
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus)){
            Distance++;
        }
	}
}
