using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int currentLevel;
    public int MaxLevel;
    public GameObject[] GameManagerClones;
    public int AlivePlayers;
    public string PlayerName;
    public string ChosenTank;
    public GameObject PreviewCannon, PreviewHammer, PreviewShield;
    public int ChosenPlayerLimit;
    public bool NextLevelHasBeenInvoked = true;
    public bool NeverInvoked = true;

	// Use this for initialization
	void Start () {
        //StartCoroutine(ReadytoCheck());
        ChosenPlayerLimit = 2;
        AlivePlayers = ChosenPlayerLimit;
        DontDestroyOnLoad(gameObject);
        PlayerName = "NoobWithNoName";
        ChosenTank = "OnlinePlayer";
	}
	
	// Update is called once per frame
	void Update () {
        if (currentLevel > 1)
        {
            //Something wrong with this code
            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("Currently " + AlivePlayers + " players are alive!");
                if (AlivePlayers <= 1 && !NextLevelHasBeenInvoked)
                {
                    Invoke("NextLevel", 3F);
                    NextLevelHasBeenInvoked = true;
                }
            }
        }
        GameManagerClones = GameObject.FindGameObjectsWithTag("GameManager");
        currentLevel = Application.loadedLevel;
       // if (Input.GetKeyDown(KeyCode.Y))
        //{
          //  if (currentLevel+1 < Application.levelCount)
            //{
              //  PhotonNetwork.LoadLevel(currentLevel + 1);
            //}
            //else
            //{
              //  PhotonNetwork.LoadLevel(0);
            //}
       // }
        if (GameManagerClones.Length > 1)
        {
            Destroy(GameManagerClones[1]);
        }
	}
    public void GetPlayerName(string value)
    {
        if (value.Length <= 10)
        {
            PlayerName = value;
            Debug.Log("Your Player name is " + PlayerName);
        }
        else
        {
            PlayerName = "NoobWithNoName";
            Debug.Log("Your Player name is " + PlayerName + " , because your name was too long!");
        }
    }
    public void ChoseCannonTank()
    {
        ChosenTank = "OnlinePlayer";
        PreviewHammer.SetActive(false);
        PreviewShield.SetActive(false);
        PreviewCannon.SetActive(true);
    }
    public void ChoseHammerTank()
    {
        ChosenTank = "OnlinePlayerWithHammer";
        PreviewHammer.SetActive(true);
        PreviewShield.SetActive(true);
        PreviewCannon.SetActive(false);
    }
    public void NextLevel()
    {
        if (currentLevel < MaxLevel)
        {
            PhotonNetwork.LoadLevel(currentLevel + 1);
        }
        else
        {
            if (MaxLevel == 10)
            {
                PhotonNetwork.LoadLevel(9);
            }
            if (MaxLevel == 8)
            {
                PhotonNetwork.LoadLevel(2);
            }
        }
    }
    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevel == 0)
        {
            Destroy(gameObject);
        }
        AlivePlayers = ChosenPlayerLimit;
        StartCoroutine(ReadytoCheck());
        if (Application.loadedLevel > 1)
        {
            SetRoomInvisible();
        }

    }
    public IEnumerator ReadytoCheck()
    {
        NextLevelHasBeenInvoked = true;
        yield return new WaitForSeconds(5f);
        NextLevelHasBeenInvoked = false;
    }
    public void SetRoomInvisible()
    {
        PhotonNetwork.room.visible = false;
        PhotonNetwork.room.open = false;
    }
}
