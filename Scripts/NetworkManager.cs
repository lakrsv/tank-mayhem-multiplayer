using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {
    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
    public GameObject playerPrefab;
    public GameObject[] SpawnPoints;
    public int PlayerCount;
    public int RealPlayerCount;
    public int RealID;
    public GameObject myCanvas;
    public Text WaitText;
    public GameObject PlayerNameInput, StartServerButton, JoinServerButton, TwoPlayerButton, FourPlayerButton;
    public bool ServerStarted;
    public GameManager PlayerManager;
    public int ChosenPlayerLimit;
    public int ChosenPlayerLimitLevel;
    public GameObject[] JoinButtons;
    public bool JoinedRoom;
    public string PlayerName;
    public int PosX = 230;
    public int PosY = -115;
    public int i;
	// Use this for initialization
	void Start () {
        if (Application.loadedLevel == 0)
        {
            Destroy(gameObject);
        }
        PlayerName = "NoobWithNoName";
        PhotonNetwork.player.name = PlayerName;
        JoinedRoom = false;
        ChosenPlayerLimit = 2;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().MaxLevel = 8;
        ServerStarted = false;
        PhotonNetwork.ConnectUsingSettings("0.1");
        myCanvas = GameObject.FindGameObjectWithTag("Canvas");
        WaitText = GameObject.FindGameObjectWithTag("WaitforPlayers").GetComponent<Text>();
        PlayerNameInput = GameObject.FindGameObjectWithTag("PlayerInput");
        PlayerManager = GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>();
        WaitText.enabled = false;
        StartServerButton.SetActive(true);
        //PhotonNetwork.sendRate = 30;
        //PhotonNetwork.sendRateOnSerialize = 20;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (JoinedRoom)
        {
            foreach (GameObject JoinButton in JoinButtons)
            {
                JoinButton.SetActive(false);
            }
        }
        switch (ChosenPlayerLimit)
        {
            case 2:
                ChosenPlayerLimitLevel = 2;
                break;
            case 3:
                //Load Player count 3 level
                break;
            case 4:
                ChosenPlayerLimitLevel = 9;
                break;
        }
        PlayerCount = PhotonNetwork.playerList.Length - 1;
        RealPlayerCount = PhotonNetwork.playerList.Length;
        //Debug.Log(RealPlayerCount);
        if (PhotonNetwork.isMasterClient && RealPlayerCount >= ChosenPlayerLimit)
        {
            PhotonNetwork.LoadLevel(ChosenPlayerLimitLevel);
        }
	}
    void OnServerInitialized()
    {
        Debug.Log("Server Initialized");
    }
    void OnGUI()
    {
    if (!PhotonNetwork.connected)
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    else if (PhotonNetwork.room == null)
    {
        if (!ServerStarted)
        {
            StartServerButton.SetActive(true);
        }
        // Create Room
        //if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
        //{
          //  PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
            //WaitText.enabled = true;
            //PlayerNameInput.SetActive(false);
        }
 
        // Join Room
        if (roomsList != null)
        {
            for (i = i ; i < roomsList.Length; i++)
            {
                PosY = PosY - 30;
                GameObject Button = (GameObject)Instantiate(JoinServerButton);
                Button.name = "Join Server " + (i+1);
                Button.GetComponent<DynamicJoinButton>().ButtonID = i;
                RectTransform ButtonRect = Button.GetComponent<RectTransform>();
                Button.transform.SetParent(myCanvas.transform);
                ButtonRect.anchoredPosition = new Vector3(PosX, PosY, 0);
                ButtonRect.localScale = new Vector3(1, 1, 1);
                Button.GetComponentInChildren<Text>().text = "Join Room " + (i+1);
                Button ActualButton = Button.GetComponent<Button>();
                //Adding a Click listener so that we can join room on button clicked.
                ActualButton.onClick.AddListener(() => 
                {
                    PhotonNetwork.JoinRoom(roomsList[i-1].name);
                });


                //if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + "Room " +(i+1)))
                //{
                //   PhotonNetwork.JoinRoom(roomsList[i].name);
                //}
            }
        }
    }
    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }
    void OnJoinedRoom()
    {
        //PhotonNetwork.sendRate = 64;
        //PhotonNetwork.sendRateOnSerialize = 64;
        //Debug.Log("Connected to Room");
        PhotonNetwork.automaticallySyncScene = true;
        PlayerName = PlayerManager.PlayerName;
        PhotonNetwork.player.name = PlayerName;
        print("Player with name " +PlayerName+ " connected!");
        PlayerNameInput.SetActive(false);
        StartServerButton.SetActive(false);
        WaitText.enabled = true;
        JoinedRoom = true;
        if (PhotonNetwork.isMasterClient)
        {
            //PhotonNetwork.room.maxPlayers = 2;
        }
    }
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {

    }
    public void CreateServerPress()
    {
        ServerStarted = true;
        PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
        WaitText.enabled = true;
        PlayerNameInput.SetActive(false);
        StartServerButton.SetActive(false);
        TwoPlayerButton.SetActive(true);
        FourPlayerButton.SetActive(true);
    }
    public void TwoPlayerChosen()
    {
        ChosenPlayerLimit = 2;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().ChosenPlayerLimit = ChosenPlayerLimit;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().MaxLevel = 8;
        //PhotonNetwork.room.maxPlayers = 2;
    }
    public void ThreePlayerChosen(){
        ChosenPlayerLimit = 3;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().ChosenPlayerLimit = ChosenPlayerLimit;
        //PhotonNetwork.room.maxPlayers = 3;
    }
    public void FourPlayerChosen(){
        ChosenPlayerLimit = 4;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().ChosenPlayerLimit = ChosenPlayerLimit;
        GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<GameManager>().MaxLevel = 10;
        //PhotonNetwork.room.maxPlayers = 4;
    }
}
