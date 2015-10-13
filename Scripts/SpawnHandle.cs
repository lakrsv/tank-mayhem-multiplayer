using UnityEngine;
using System.Collections;

public class SpawnHandle : Photon.MonoBehaviour {
    public GameObject[] SpawnPoints;
    public GameObject[] Players;
    public string ChosenTank;
    int PlayerID;
    public bool Spawned;

	// Use this for initialization
	void Start () {
    PlayerID = PhotonNetwork.player.ID;
    SpawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
    Spawned = false;
    SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    void SpawnPlayer()
    {
        if (PhotonNetwork.playerList.Length <= SpawnPoints.Length)
        {
            if (GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().ChosenTank != null)
            {
                ChosenTank = GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().ChosenTank;
            }
            else
            {
                ChosenTank = "OnlinePlayer";
            }
            GameObject myPlayer = PhotonNetwork.Instantiate(ChosenTank, SpawnPoints[PlayerID - 1].transform.position, SpawnPoints[PlayerID - 1].transform.rotation, 0);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            Application.LoadLevel(0);
        }
    }
}
