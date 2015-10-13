using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitTextScript : MonoBehaviour {
    public Text thisText;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator TextAnimate()
    {
        while (true)
        {
            thisText.text = "Waiting for Players.";
            yield return new WaitForSeconds(1f);
            thisText.text = "Waiting for Players..";
            yield return new WaitForSeconds(1f);
            thisText.text = "Waiting for Players...";
            yield return new WaitForSeconds(1f);
        }
    }
    void OnEnable()
    {
        thisText = gameObject.GetComponent<Text>();
        StartCoroutine(TextAnimate());
    }
}
