using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnlineCanvasManager : Photon.MonoBehaviour {
    public string Actor1Name, Actor2Name, Actor3Name, Actor4Name;
    public int Actor1ScoreInt, Actor2ScoreInt, Actor3ScoreInt, Actor4ScoreInt;
    public string Actor1ScoreString, Actor2ScoreString, Actor3ScoreString, Actor4ScoreString;
    public Text Actor1Text, Actor2Text, BulletCount, Actor3Text, Actor4Text;
    public GameObject[] AllCanvas;
    public GameObject[] Players;
    public GameObject MyPlayer;
    public int MyBulletCount;
    public float ReloadTime;
    public GameObject ReloadBarFill, ReloadBarOutline;
    public Image ReloadBarFillImg;
    private TankHeadMovementOnline TankHead;
    public float ActualFillAmount;
    public GameObject ESCMenu;
    public RectTransform ESCMenuRect;
    public bool MenuOpen = false;

	// Use this for initialization
	void Start () {
        if (Application.loadedLevel == 0)
        {
            Destroy(gameObject);
        }
        ActualFillAmount = 1;
        MyBulletCount = 10;
        Actor1Name = "Player1";
        Actor2Name = "Player2";
        Actor3Name = "Player3";
        Actor4Name = "Player4";
        ReloadTime = 0;
        DontDestroyOnLoad(gameObject);
        AllCanvas = GameObject.FindGameObjectsWithTag("Canvas");
        if (AllCanvas.Length > 1)
        {
            Destroy(AllCanvas[1]);
        }
        if (GameObject.FindGameObjectsWithTag("OnlinePlayer") != null)
        {
            Players = GameObject.FindGameObjectsWithTag("OnlinePlayer");
            if (Players != null)
            {
                foreach (GameObject FoundPlayers in Players)
                {
                    if (FoundPlayers.transform.root.gameObject.GetComponent<PhotonView>() != null)
                    {
                        if (FoundPlayers.transform.root.gameObject.GetComponent<PhotonView>().isMine)
                        {
                            MyPlayer = FoundPlayers;
                            TankHead = MyPlayer.GetComponent<NetworkChar>().TurretHead.GetComponent<TankHeadMovementOnline>();
                        }
                    }
                }
            }
            if (TankHead != null)
            {
                if (TankHead.BulletSpawnPos == null && ReloadBarFill != null && ReloadBarOutline != null)
                {
                    Destroy(ReloadBarFill);
                    Destroy(ReloadBarOutline);
                }
                if (ReloadBarFill != null)
                {
                    ReloadBarFillImg = ReloadBarFill.GetComponent<Image>();
                }
            }
        }

        //myCanvasPhotonView.RPC("GiveNameAndScore", PhotonTargets.OthersBuffered, PlayerName, MyScoreString);
	}
	
	// Update is called once per frame
    void Update()
    {
        EscapeMenu();
        if (MyPlayer == null)
        {
            if (GameObject.FindGameObjectsWithTag("OnlinePlayer") != null)
            {
                Players = GameObject.FindGameObjectsWithTag("OnlinePlayer");
                if (Players != null)
                {
                    foreach (GameObject FoundPlayers in Players)
                    {
                        if (FoundPlayers.transform.root.gameObject.GetComponent<PhotonView>().isMine)
                        {
                            MyPlayer = FoundPlayers;
                        }
                    }
                }
            }
        }
        if (MyPlayer != null && ReloadBarFill != null && TankHead != null)
        {
            ReloadBarFillImg.fillAmount = ActualFillAmount;
            if(!TankHead.ReadytoFire)
            {
                ReloadTime += Time.deltaTime * 0.25f;
                ActualFillAmount = Mathf.Lerp(0, 1, ReloadTime);
            }
            if (ActualFillAmount == 1)
            {
                ReloadTime = 0;
            }
        }
        if (MyPlayer != null && MyPlayer.GetComponent<NetworkChar>().TurretHead != null) { 
                {
                    if (TankHead.BulletSpawnPos != null)
                    {
                        {
                            if (MyPlayer.GetComponent<NetworkChar>().TurretHead != null)
                            {
                                if (MyPlayer.GetComponent<NetworkChar>().TurretHead.GetComponent<TankHeadMovementOnline>().BulletSpawnPos != null)
                                {
                                    MyBulletCount = MyPlayer.GetComponent<NetworkChar>().TurretHead.GetComponent<TankHeadMovementOnline>().BulletCount;
                                    BulletCount.text = "Bullets Left : " + MyBulletCount.ToString();
                                }

                            }
                        }
                    }
                    }
                }
    }
    public void ScoreUpdate()
    {
        if (PhotonPlayer.Find(1) != null)
        {
            Actor1Name = PhotonPlayer.Find(1).name;
            Actor1ScoreString = Actor1ScoreInt.ToString();
            Actor1Text.text = Actor1Name + " : " + Actor1ScoreString + " |";
        }
        if (PhotonPlayer.Find(2) != null) 
        { 
            Actor2Name = PhotonPlayer.Find(2).name;
            Actor2ScoreString = Actor1ScoreInt.ToString();
            Actor2Text.text = "| " + Actor2ScoreString + " : " + Actor2Name;
        }
        if (PhotonPlayer.Find(3) != null) 
        { 
            Actor3Name = PhotonPlayer.Find(3).name;
            Actor3ScoreString = Actor1ScoreInt.ToString();
            Actor3Text.text = Actor3Name + " : " + Actor3ScoreString + " |";
        }
        if (PhotonPlayer.Find(4) != null) 
        { 
            Actor4Name = PhotonPlayer.Find(4).name;
            Actor4ScoreString = Actor1ScoreInt.ToString();
            Actor4Text.text = "| " + Actor4ScoreString + " : " + Actor4Name;
        }
    }
    void OnLevelWasLoaded(int level)
    {
        Invoke("Start", 0.5F);
    }
    public void BacktoMainMenu()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.Disconnect();
    }
    public void ExitGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        Application.Quit();
    }
    public void EscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOpen = !MenuOpen;
            Debug.Log("Is the menu open? " + MenuOpen);
        }
        if (MenuOpen)
        {
            ESCMenuRect.localScale = Vector3.Lerp(ESCMenuRect.localScale, new Vector3(1, 1, 1), 0.2f);
        }
        if (!MenuOpen)
        {
            ESCMenuRect.localScale = Vector3.Lerp(ESCMenuRect.localScale, new Vector3(0, 0, 0), 0.2f);
        }
    }
    void OnDisconnectedFromPhoton()
    {
        Application.LoadLevel(0);
    }
}
