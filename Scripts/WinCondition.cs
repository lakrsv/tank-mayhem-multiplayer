using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {
    public int PlayerID;
    public int currentLevel;
    public int MaxLevel;
    public bool AddedScore;
    public PhotonView RootPhotonView;
    public bool NeverFired = true;

	// Use this for initialization
	void Start () {
        StopCoroutine(PlayerWins());
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
        MaxLevel = GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().MaxLevel;
        RootPhotonView = gameObject.transform.root.GetComponent<PhotonView>();
        AddedScore = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y < -40){
            StartCoroutine(PlayerWins());
            if (NeverFired)
            {
                RootPhotonView.RPC("PlayerHitByHammer", PhotonTargets.All);
                NeverFired = false;
            }
        }
    }
    IEnumerator PlayerWins(){
        yield return new WaitForSeconds(1.5f);
        if (!AddedScore && PhotonNetwork.isMasterClient)
        {
            RootPhotonView.RPC("AddScoreToEnemy", PhotonTargets.All);
            AddedScore = true;
        }
        yield return new WaitForSeconds(0.5f);
        currentLevel = Application.loadedLevel;
        //WARNING THIS HAS TO BE FIXED IF I ADD MORE LEVELS THAT ARE NOT 2 PLAYER ONLINE MP!!! NEED TO DEFINE A MAX LEVEL IN THAT CASE!!
    }
}
