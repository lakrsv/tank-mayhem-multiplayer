using UnityEngine;
using System.Collections;

public class MasterHammerPivot : MonoBehaviour
{
    public PhotonView MasterView;
    public int PlayerID;

    // Use this for initialization
    void Start()
    {
        MasterView = transform.root.GetComponent<PhotonView>();
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
    }
    void Update()
    {
        if (MasterView != null)
        {
            if (MasterView.isMine)
            {
                if (Input.GetButtonDown("Fire" + PlayerID) && !GetComponent<Animation>().isPlaying)
                {
                    gameObject.GetComponent<Animation>().Play();
                    MasterView.RPC("HammerMasterMovement", PhotonTargets.All);
                }
            }
        }
    }

    // Update is called once per frame
}
