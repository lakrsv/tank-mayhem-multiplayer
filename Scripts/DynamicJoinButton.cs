using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicJoinButton : MonoBehaviour {
    public RoomInfo[] roomsList;
    public int ButtonID;
    public Text ChildText;

	// Use this for initialization
	void Start () {
        ChildText = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //ChildText.text = "Join Room " + (ButtonID + 1) + " - " + roomsList[ButtonID].maxPlayers.ToString() + "Player";
	}
    void OnReceivedRoomListUpdate()
    {
        //roomsList = PhotonNetwork.GetRoomList();
    }
}
