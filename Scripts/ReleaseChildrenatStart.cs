using UnityEngine;
using System.Collections;

public class ReleaseChildrenatStart : MonoBehaviour {
    public int ChildCount;

	// Use this for initialization
	void Start () {
        ChildCount = gameObject.transform.childCount;
        ExplodeAtStart();
    }
	
    public void ExplodeAtStart()
    {
        ChildCount = gameObject.transform.childCount;
         for (int i = 0; i < ChildCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<Rigidbody>() != null)
            {
                gameObject.transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(5000, gameObject.transform.GetChild(i).transform.position, 3F);
            }
        }
            gameObject.transform.DetachChildren();
	}
}
