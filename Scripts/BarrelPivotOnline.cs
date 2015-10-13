using UnityEngine;
using System.Collections;

public class BarrelPivotOnline : Photon.MonoBehaviour {
    public float MinYRot;
    public float MaxYRot;
    public float InputV;
    public int PlayerID;
    public PhotonView RootPhotonView;

	// Use this for initialization
	void Start () {
        MinYRot = 100;
        MaxYRot = 190;
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
        RootPhotonView = transform.root.gameObject.GetComponent<PhotonView>();

	}
	
	// Update is called once per frame
	void Update () {
        if (RootPhotonView != null)
        {
            if (RootPhotonView.isMine && gameObject != null)
            {
                {
                    InputV = Input.GetAxis("VerticalRight" + PlayerID);
                    //Debug.Log("Moving Barrel!");
                    if (InputV == 1 && transform.localRotation.eulerAngles.y < MaxYRot)
                    {
                        transform.Rotate(Vector3.up, InputV * Time.deltaTime * 20);
                    }
                    if (InputV == -1 && transform.localRotation.eulerAngles.y > MinYRot)
                    {
                        transform.Rotate(Vector3.up, InputV * Time.deltaTime * 20);
                    }
                }
            }
        }
        else
        {
            //SyncedMovement();
        }
  
        }
        //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //        if (stream.isWriting)
         //       {
          //          stream.SendNext(transform.position);
           //         stream.SendNext(transform.rotation);
            //    }
    //
      //          else
        //        {
          //          transform.position = (Vector3)stream.ReceiveNext();
            //        transform.rotation = (Quaternion)stream.ReceiveNext();
              //  }
           // }
	}

