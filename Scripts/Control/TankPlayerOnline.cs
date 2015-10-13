using UnityEngine;
using System.Collections;

public class TankPlayerOnline : Photon.MonoBehaviour {
    public Rigidbody frontLeft;
    public Rigidbody rearLeft;
    public Rigidbody frontRight;
    public Rigidbody rearRight;
    public AudioClip Forward;
    public int wheelTorque;
    public int fLDir;
    public int rLDir;
    public int fRDir;
    public int rRDir;
    public int TorqMod;
    public float InputH;
    public float InputV;
    public int PlayerID;
    public bool HitbyBullet;
    public bool Playing;
    private PhotonView myPhotonView;
    private PhotonView RootPhotonView;
    public int ClassMod;

    //Trying to Optimize this Script, TankPlayerOnline and BarrelPivot are slowest scripts.
    void Start()
    {
        RootPhotonView = transform.root.gameObject.GetComponent<PhotonView>();
        myPhotonView = gameObject.AddComponent<PhotonView>();
        Playing = false;
        if (!transform.root.name.Contains("Hammer"))
        {
            ClassMod = 1;
            TorqMod = 1;
        }
        else if (transform.root.name.Contains("Hammer"))
        {
            ClassMod = 2;
            TorqMod = 1;
            frontLeft.GetComponent<Rigidbody>().maxAngularVelocity = 13;
            rearLeft.GetComponent<Rigidbody>().maxAngularVelocity = 13;
            frontRight.GetComponent<Rigidbody>().maxAngularVelocity = 13;
            rearRight.GetComponent<Rigidbody>().maxAngularVelocity = 13;
        }
    }
    void Update()
    {
        if (RootPhotonView.isMine)
        {
            TankMovement();
        }
        else
        {
            //SyncedMovement();
        }
    }
    void FixedUpdate(){
        if(RootPhotonView.isMine)
        {
            frontLeft.AddRelativeTorque (Vector3.forward * fLDir * wheelTorque * ClassMod);
            rearLeft.AddRelativeTorque (Vector3.forward * rLDir * wheelTorque * ClassMod);
            frontRight.AddRelativeTorque (Vector3.forward * rRDir * wheelTorque * ClassMod);
            rearRight.AddRelativeTorque (Vector3.forward * rRDir * wheelTorque * ClassMod);
        }
        else
        {
        }
    }
    void TankMovement()
    {
        InputH = Input.GetAxis("HorizontalLeft" + PlayerID);
        InputV = Input.GetAxis("VerticalLeft" + PlayerID);
        //Debug.Log("Your PlayerID is " + PlayerID);
        //Slow script, trying to optimize.
        if (InputV == -1)
        {
            if (Mathf.Abs(InputH) != 1)
            {
                fLDir = 1;
                rLDir = 1;
                fRDir = 1;
                rRDir = 1;
                wheelTorque = 5000 * TorqMod;
            }
        }
        else
        {
            fLDir = 0;
            rLDir = 0;
            fRDir = 0;
            rRDir = 0;
            wheelTorque = 0;
        }
        if (InputV == 1 && Mathf.Abs(InputH) != 1)
        {
            fLDir = -1;
            rLDir = -1;
            fRDir = -1;
            rRDir = -1;
            wheelTorque = 5000 * TorqMod;
        }
        if (InputH == -1)
        {
            fLDir = -1;
            rLDir = -1;
            fRDir = 1;
            rRDir = 1;
            wheelTorque = 5000 * TorqMod;
        }
        if (InputH == 1)
        {
            fLDir = 1;
            rLDir = 1;
            fRDir = -1;
            rRDir = -1;
            wheelTorque = 5000 * TorqMod;
        }
    }
    //if(wheelTorque > 100)
    //{
        //if (!Playing)
        //{
        //    myPhotonView.RPC("TankForward", PhotonTargets.All, true);
        //}
    //}else
    //{
      //      myPhotonView.RPC("TankForward", PhotonTargets.All, false);
    //}
//}
   //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   //{
    //   if(stream.isWriting){
     //      stream.SendNext(frontLeft.rigidbody.position);
      //     stream.SendNext(frontRight.rigidbody.position);
       //    stream.SendNext(rearLeft.rigidbody.position);
        //   stream.SendNext(rearRight.rigidbody.position);
    //
      //     stream.SendNext(frontLeft.rigidbody.rotation);
        //   stream.SendNext(frontRight.rigidbody.rotation);
          // stream.SendNext(rearLeft.rigidbody.rotation);
           //stream.SendNext(rearRight.rigidbody.rotation);
            //}else{
           //frontLeft.rigidbody.position = (Vector3)stream.ReceiveNext();
           //frontRight.rigidbody.position = (Vector3)stream.ReceiveNext();
           //rearLeft.rigidbody.position = (Vector3)stream.ReceiveNext();
           //rearRight.rigidbody.position = (Vector3)stream.ReceiveNext();
//
  //         frontLeft.rigidbody.rotation = (Quaternion)stream.ReceiveNext();
    //       frontRight.rigidbody.rotation = (Quaternion)stream.ReceiveNext();
      //     rearLeft.rigidbody.rotation = (Quaternion)stream.ReceiveNext();
        //   rearRight.rigidbody.rotation = (Quaternion)stream.ReceiveNext();
       //}
   //}
    [RPC]
   void TankForward(bool x)
   {
        //Doesn't work properly
       GetComponent<AudioSource>().clip = Forward;
       if (x)
       {
           if (!Playing)
           {
               GetComponent<AudioSource>().Play();
               Playing = true;
           }
       }
       else
       {
           GetComponent<AudioSource>().Stop();
           Playing = false;
       }
   }
}