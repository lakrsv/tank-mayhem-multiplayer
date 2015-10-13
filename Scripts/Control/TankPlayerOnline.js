var frontLeft : Rigidbody;
var rearLeft : Rigidbody;
var frontRight : Rigidbody;
var rearRight : Rigidbody;
var wheelTorque : int;
var fLDir : int;
var rLDir : int;
var fRDir : int;
var rRDir : int;
public var TorqMod : int;
var InputH : float;
var InputV : float;
public var PlayerID : int;

function Start(){
}

function Update () {
    if(transform.root.gameObject.GetComponent(NetworkView).isMine){
        TankMovement();
    }
}

function FixedUpdate(){
    if(transform.root.gameObject.GetComponent(NetworkView).isMine){
        frontLeft.AddRelativeTorque (Vector3.forward * fLDir * wheelTorque);
        rearLeft.AddRelativeTorque (Vector3.forward * rLDir * wheelTorque);
        frontRight.AddRelativeTorque (Vector3.forward * rRDir * wheelTorque);
        rearRight.AddRelativeTorque (Vector3.forward * rRDir * wheelTorque);
    }
}
function TankMovement(){
    InputH = Input.GetAxis("HorizontalLeft" +PlayerID);
    InputV = Input.GetAxis("VerticalLeft" +PlayerID);
    Debug.Log("Your PlayerID is " +PlayerID);
    if(InputV == -1){
        fLDir = 1;
        rLDir = 1;
        fRDir = 1;
        rRDir = 1;
        wheelTorque = 5000 * TorqMod;
    }else{
        fLDir = 0;
        rLDir = 0;
        fRDir = 0;
        rRDir = 0;
        wheelTorque = 0;
    }
    if(InputV == 1){
        fLDir = -1;
        rLDir = -1;
        fRDir = -1;
        rRDir = -1;
        wheelTorque = 5000 * TorqMod;
    }
    if(InputH == -1){
        fLDir = -1;
        rLDir = -1;
        fRDir = 1;
        rRDir = 1;
        wheelTorque = 5000 * TorqMod;
    }
    if(InputH == 1){
        fLDir = 1;
        rLDir = 1;
        fRDir = -1;
        rRDir = -1;
        wheelTorque = 5000 * TorqMod;
    }
    if(Input.GetKeyDown(KeyCode.Y)){
        if(TorqMod < 5){
            TorqMod++;
        }
    }
    if(Input.GetKeyDown(KeyCode.U)){
        if(TorqMod > 1){
            TorqMod--;
        }
    }
    if(wheelTorque > 500){
        if(!GetComponent.<AudioSource>().isPlaying){
            GetComponent.<AudioSource>().Play();
        }
    }else{
        GetComponent.<AudioSource>().Stop();
    }
}