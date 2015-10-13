var t : float;
var tor : float;

function Awake(){
	t = 5000;
}

function Update(){
	if(Input.GetKey(KeyCode.E)){
		tor = 1;
		t = 5000;
	}else if(Input.GetKey(KeyCode.D)){
		tor = -1;
		t = 300;
	}else{
		tor = 0;
	}
}

function FixedUpdate () {
    GetComponent.<Rigidbody>().AddRelativeTorque (Vector3.forward * t * tor);
}