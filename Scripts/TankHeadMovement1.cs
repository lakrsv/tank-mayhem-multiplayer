using UnityEngine;
using System.Collections;

public class TankHeadMovement1 : MonoBehaviour {
    public float TurretMoveSpeed;
    public bool Damping;
    public GameObject BulletSpawnPos;
    public GameObject BulletPrefab;
    public GameObject PlayerCamera;
    //public Vector3 PlayerCameraStartPos;
    //public Quaternion PlayerCameraStartRot;
    public bool ReadytoFire;
    private float InputH;
    private float InputV;
    public float ReloadSpeed;
    public int PlayerID;

	// Use this for initialization
	void Start () {
        PlayerID = transform.root.GetComponent<PlayerIDSet>().PlayerID;
        TurretMoveSpeed = 0;
        ReloadSpeed = 4.0f;
        ReadytoFire = true;
        //PlayerCameraStartPos = PlayerCamera.transform.localPosition;
        //PlayerCameraStartRot = PlayerCamera.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().volume = 0.5f * Mathf.Abs(TurretMoveSpeed);
        //TankHeadControls();
        TankheadJoystick();
	}
    void TankheadJoystick()
    {
        InputH = Input.GetAxis("Horizontal" + PlayerID);

        TurretMoveSpeed = TurretMoveSpeed + InputH * Time.smoothDeltaTime;
        transform.Rotate(Vector3.forward * TurretMoveSpeed);
        if (Mathf.Abs(InputH) < 0.2 && TurretMoveSpeed != 0)
        {
            Damping = true;
        }
        else
        {
            Damping = false;
        }
        if (Damping)
        {
            TurretMoveSpeed = TurretMoveSpeed * (1 - (Time.deltaTime * 3));
            if (Mathf.Abs(TurretMoveSpeed) < 0.01)
            {
                Damping = false;
                TurretMoveSpeed = 0;
            }
        }
        if (Input.GetButtonDown("Fire" + PlayerID))
        {
            if (ReadytoFire)
            {
                FireMissile();
                StartCoroutine(Reloading());
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Application.LoadLevel(0);
        }
    }
    void TankHeadControls()
    {
        //Movement of Turret;
        transform.Rotate(Vector3.back * TurretMoveSpeed);
        //Turret Head Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (TurretMoveSpeed < 0.5)
            {
                TurretMoveSpeed = TurretMoveSpeed + Time.smoothDeltaTime / 2;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (TurretMoveSpeed > -0.5f)
            {
                TurretMoveSpeed = TurretMoveSpeed - Time.smoothDeltaTime / 2;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || (Input.GetKeyUp(KeyCode.RightArrow)))
        {
            Damping = true;
        }
        if (Damping)
        {
            TurretMoveSpeed = TurretMoveSpeed * (1 - (Time.deltaTime));
            if (Mathf.Abs(TurretMoveSpeed) < 0.01)
            {
                Damping = false;
                TurretMoveSpeed = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ReadytoFire)
            {
                FireMissile();
                StartCoroutine(Reloading());
            }
        }
    }
    void FireMissile()
    {
        //GameObject Bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnPos.transform.position, BulletSpawnPos.transform.rotation);
        //if (Input.GetButton("Follow" + PlayerID))
        //{
            //PlayerCamera.transform.SetParent(Bullet.transform);
            //PlayerCamera.transform.localPosition = new Vector3(0, 5, 30);
            //PlayerCamera.transform.localRotation = Quaternion.Euler(0, 180, 360);
        //}
    }
    IEnumerator Reloading()
    {
        ReadytoFire = false;
        yield return new WaitForSeconds(ReloadSpeed);
        ReadytoFire = true;
    }
}
