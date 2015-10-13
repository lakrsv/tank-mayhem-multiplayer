using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinButtonRoomID : MonoBehaviour {
    public int RoomID;
    private RoomInfo[] roomsList;
    public Button thisButton;


	// Use this for initialization
	void Start () 
    {
        thisButton = gameObject.GetComponent<Button>();

        thisButton.onClick.AddListener(() =>
        {
            PhotonNetwork.JoinRoom(roomsList[RoomID].name);
        });
	}
    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }
}
