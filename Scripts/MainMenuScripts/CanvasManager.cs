using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LocalMP()
    {
        Debug.Log("LocalMP button pressed!");
    }
    public void OnlineMP()
    {
        Debug.Log("OnlineMP button pressed!");
        Application.LoadLevel(1);
    }
    public void Exit()
    {
        Debug.Log("Exit button pressed!");
        Application.Quit();
    }
}
