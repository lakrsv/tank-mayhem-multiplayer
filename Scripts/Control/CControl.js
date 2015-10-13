var tpos : Transform;

function Update () {
	var p : Vector3;
	p = tpos.position;
	p.y = 0;
	transform.position = p;
	
}