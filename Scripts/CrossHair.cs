using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour
{

    public Texture2D cursorImage;
    private int cursorWidth = 32;
    private int cursorHeight = 32;
    private Transform myTransform;
    private Camera myCamera;
    public GameObject PlayerCamera;
    public int PlayerID;
    public bool CrossHairEnabled;

    void Start()
    {
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
        myCamera = PlayerCamera.GetComponent<Camera>();
        myTransform = transform;    //So you don't GetComponent your transform with every OnGUI call
        CrossHairEnabled = false;

    }
    void Update()
    {
    }


    void OnGUI()
    {
        if (transform.root.gameObject.GetComponent<PhotonView>().isMine)
        {
            if (myCamera != null)
            {
                Vector3 screenPos = myCamera.WorldToScreenPoint(myTransform.position);
                screenPos.y = Screen.height - screenPos.y; //The y coordinate on screenPos is inverted so we need to set it straight
                if (CrossHairEnabled)
                {
                    GUI.DrawTexture(new Rect(screenPos.x - 0.5f, screenPos.y, cursorWidth, cursorHeight), cursorImage);
                }
            }
        }
        }
    }