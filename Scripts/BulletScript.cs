using UnityEngine;
using System.Collections;

//Don't attempt PhotonNetwork.Destroy on Tank as it doesn't have a PhotonView!
public class BulletScript : Photon.MonoBehaviour
{
    public int ForceMultiplier;
    public float ExplosionForce;
    public float ExplosionRadius;
    public HingeJoint[] hingeJoints;
    public AudioClip BulletFire, BulletImpact;
    public GameObject BeltPartsPrefab;
    private int BeltPartChildCount;
    public GameObject[] PlaceHolders;
    public PhotonView myPhotonView;
    public GameObject[] Bullets;
    public int UniqueID;
    public int myViewID;
    public bool NeverFired;
    public bool NotBlocked;
    public int AttackersID;
    // Use this for initialization
    void Start()
    {
        NotBlocked = true;
        NeverFired = true;
        Invoke("EnableCollider", 0.1F);
        //Sending myViewID instead of UniqueID, trying it out.
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * (1000 * ForceMultiplier));
        //audio.PlayOneShot(BulletFire);
        FireBullet();
        PlaceHolders = GameObject.FindGameObjectsWithTag("Placeholder");
        BeltPartChildCount = BeltPartsPrefab.transform.childCount;
        myPhotonView = gameObject.GetComponent<PhotonView>();
        myViewID = myPhotonView.viewID;
        Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        UniqueID = Bullets.Length;
        //Don't think I need to RPC the Audio because the bullet is already RPCed and excists on all other players.
        //myPhotonView.RPC("FireBullet", PhotonTargets.All);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-GetComponent<Rigidbody>().velocity);
        if (transform.position.y < -40)
        {
            if (gameObject != null)
            {
                gameObject.transform.DetachChildren();
                if (PhotonNetwork.isMasterClient && myPhotonView != null)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        ApplyExplosionForce();
        if (other.transform.tag == "BulletStopper")
        {
            PhotonView HitPhotonView = other.transform.root.gameObject.GetComponent<PhotonView>();
            HitPhotonView.RPC("BulletStopped", PhotonTargets.All, myViewID);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        ApplyExplosionForce();
        if (other.transform.tag == "BulletStopper")
        {
            PhotonView HitPhotonView = other.transform.root.gameObject.GetComponent<PhotonView>();
            HitPhotonView.RPC("BulletStopped", PhotonTargets.All, myViewID);
        }
        if (other.transform.tag == "Belts")
        {
            ApplyExplosionForce();
            PhotonView HitPhotonView = other.transform.root.gameObject.GetComponent<PhotonView>();
            if (HitPhotonView.isMine)
            {
                HitPhotonView.RPC("PlayerHitByBullet", PhotonTargets.All, myViewID);
                Invoke("ApplyExplosionForce", 0.5F);
            }
            //int HitPlayerID = other.transform.root.gameObject.GetComponent<PhotonView>().viewID;
            //GameObject RootofObject = other.transform.root.gameObject;
            //ApplyExplosionForce();

            //THIS IS IMPORTANT!! BELTPREFABS DONT SPAWN SOMETIMES, SO IM TRYING TO ADD IT DIRECTLY TO PLAYERHIT RPC

            //if (PhotonNetwork.isMasterClient)
            //{
            //PhotonNetwork.Instantiate(BeltPartsPrefab.name, transform.position, Quaternion.identity, 0);
            //}
            //foreach (GameObject Placeholder in PlaceHolders)
            //{
            //  if (Placeholder.transform.root == other.transform.root)
            //{
            //Placeholder.SetActive(false);
            //}
            //}
            //other.transform.GetChild(5).GetChild(1).GetChild(0).GetChild(3).SetParent(null);
            //if (other.transform.root.GetComponentInChildren<Camera>() != null)
            //{
            //  other.transform.root.GetComponentInChildren<Camera>().gameObject.transform.SetParent(null);
            //}
            //ApplyExplosionForce();
            //if (other.gameObject.GetComponentInChildren<HingeJoint>() != null)
            //{
            //  hingeJoints = other.gameObject.GetComponentsInChildren<HingeJoint>();
            //foreach (HingeJoint hinges in hingeJoints)
            //{
            //  hinges.breakTorque = 1;
            //hinges.breakForce = 1;
            //}
            //}
            //for (int i = 0; i < 6; i++)
            //{
            //Destroy(other.transform.GetChild(i).gameObject);
            //}
            //other.transform.DetachChildren();
            //other.transform.root.DetachChildren();
            //ApplyExplosionForce();
            //PhotonNetwork.Destroy(other.transform.root.gameObject);
            //PhotonNetwork.Destroy(other.transform.gameObject);
            //}
            //Destroy(other.transform.gameObject);
            //GameObject TempHitPlayer = other.gameObject;
            //PhotonNetwork.Destroy(other.transform.gameObject);
        }
    }
    public IEnumerator DestroyObject()
    {
        if (gameObject != null && NeverFired)
        {
            NeverFired = false;
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Instantiate("Explosion03b", transform.position, Quaternion.identity, 0);
            }
            //Don't need to RPC the audio because the bullet is RPCed

            //myPhotonView.RPC("BulletHit", PhotonTargets.Others);
            BulletHit();
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            yield return new WaitForSeconds(5f);
            if (this != null)
            {
                if (gameObject != null)
                {
                    transform.DetachChildren();
                    //Changed because I PhotonNetwork.Instantiated the bullet now, change back if that doesn't work.
                    //If I am PhotonNetwork Destroying, make sure the masterclient destroys it.
                    if (this != null && gameObject != null && myPhotonView != null && PhotonNetwork.isMasterClient)
                    {
                        PhotonNetwork.Destroy(gameObject);
                    }
                }
            }
        }
    }
    public void ApplyExplosionForce()
    {
        Vector3 ExplosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(ExplosionPos, ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            if (!hit)
                continue;
            if (hit && hit.GetComponent<Rigidbody>())
            {
                if (!hit.transform.root.name.Contains("Hammer"))
                {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, ExplosionPos, ExplosionRadius, 3.0F);
                }
                if (hit.transform.root.name.Contains("Hammer"))
                {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce / 2.5f, ExplosionPos, ExplosionRadius, 3.0F);
                }
            }
        }
        //Putting this in the PlayerHit RPC
        StartCoroutine(DestroyObject());
    }
    public void FireBullet()
    {
        GetComponent<AudioSource>().clip = BulletFire;
        GetComponent<AudioSource>().Play();

    }
    public void BulletHit()
    {
        GetComponent<AudioSource>().clip = BulletImpact;
        GetComponent<AudioSource>().Play();
    }
    public void EnableCollider()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
