using UnityEngine;
using System.Collections;

public class MasterShield : MonoBehaviour
{
    public PhotonView MasterView;
    public int PlayerID;
    public bool ReadytoPlay;

    // Use this for initialization
    void Start()
    {
        ReadytoPlay = true;
        MasterView = transform.root.GetComponent<PhotonView>();
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
    }
    void Update()
    {
        if (MasterView != null)
        {
            if (MasterView.isMine)
            {
                if (Input.GetButtonDown("Shield" + PlayerID) && ReadytoPlay)
                {
                    ReadytoPlay = false;
                    gameObject.GetComponent<Animation>().Play();
                    MasterView.RPC("ShieldMasterMovement", PhotonTargets.All);
                }
            }
        }
    }
}