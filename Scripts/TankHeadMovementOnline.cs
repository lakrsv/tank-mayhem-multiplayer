using UnityEngine;
using System.Collections;

public class TankHeadMovementOnline : Photon.MonoBehaviour {
    public float TurretMoveSpeed;
    public bool Damping;
    public GameObject BulletSpawnPos;
    public GameObject BulletPrefab;
    public GameObject PlayerCamera;
    public Vector3 PlayerCameraStartPos;
    public Quaternion PlayerCameraStartRot;
    public bool ReadytoFire;
    public int BulletCount;
    private float InputH;
    private float InputV;
    public float ReloadSpeed;
    public int PlayerID;
    private PhotonView myPhotonView;
    public GameObject TheBullet;
    public int myActorID;

	// Use this for initialization
	void Start () {
        BulletCount = 10;
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
        TurretMoveSpeed = 0;
        ReloadSpeed = 4.0f;
        ReadytoFire = true;
        PlayerCameraStartPos = PlayerCamera.transform.localPosition;
        PlayerCameraStartRot = PlayerCamera.transform.localRotation;
        myPhotonView = transform.root.gameObject.GetComponent<PhotonView>();
        if (myPhotonView.isMine)
        {
            myActorID = PhotonNetwork.player.ID;
        }
        //myPhotonView.RPC("Head", PhotonTargets.All);
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().volume = 0.5f * Mathf.Abs(TurretMoveSpeed);
        //TankHeadControls();
        if (myPhotonView.isMine)
        {
            TankheadJoystick();
        }
        else
        {
            //SyncedMovement();
        }
	}
    void TankheadJoystick()
    {
        InputH = Input.GetAxis("Horizontal" + PlayerID);
        if (Mathf.Abs(TurretMoveSpeed) < 0.10f)
        {
            TurretMoveSpeed = TurretMoveSpeed + InputH * Time.deltaTime / 4;
        }
        transform.Rotate(Vector3.forward * TurretMoveSpeed * Time.deltaTime * 300);
        if (Mathf.Abs(InputH) < 0.9 && TurretMoveSpeed != 0)
        {
            Damping = true;
        }
        else
        {
            Damping = false;
        }
        if (Damping)
        {
            TurretMoveSpeed = TurretMoveSpeed * (1 - (Time.deltaTime * 6));
            if (Mathf.Abs(TurretMoveSpeed) < 0.01)
            {
                Damping = false;
                TurretMoveSpeed = 0;
            }
        }
        if (BulletSpawnPos != null)
        {
            if (Input.GetButtonDown("Fire" + PlayerID))
            {
                if (ReadytoFire && BulletCount > 0)
                {
                    gameObject.GetComponent<PhotonView>().RPC("FireMissile", PhotonTargets.All, this.BulletSpawnPos.transform.position, this.BulletSpawnPos.transform.rotation);
                    StartCoroutine(Reloading());
                    BulletCount--;
                    if (Input.GetButton("Follow" + PlayerID))
                    {
                        //Disabling artillery mode as of right now because it causes issues.

                        //if (TheBullet != null)
                        //{
                            //PlayerCamera.transform.SetParent(TheBullet.transform);
                            //PlayerCamera.transform.localPosition = new Vector3(0, 5, 30);
                            //PlayerCamera.transform.localRotation = Quaternion.Euler(0, 180, 360);
                        //}
                    }
                }
            }
        }
    }
    [RPC]
    void FireMissile(Vector3 Position, Quaternion Rotation)
    {
        if (PhotonNetwork.isMasterClient)
        {
            //Changed this to PhotonNetwork.Instantiate just to check if it works properly. This means I have to photonnetwork.destroy them in others. So if I change it back change PlayerRPC
            TheBullet = (GameObject)PhotonNetwork.Instantiate("TankBulletPrefab", Position, Rotation, 0);
            //TheBullet.GetComponent<BulletScript>().AttackersID = AttackerID;
        }
    }
    IEnumerator Reloading()
    {
        ReadytoFire = false;
        yield return new WaitForSeconds(ReloadSpeed);
        ReadytoFire = true;
    }
    //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
     //   if (stream.isWriting)
      //  {
        //    stream.SendNext(transform.position);
         //   stream.SendNext(transform.rotation);
        //}
    //
     //   else
      //  {
        //    transform.position = (Vector3)stream.ReceiveNext();
         //   transform.rotation = (Quaternion)stream.ReceiveNext();
        //}
    //}
}
