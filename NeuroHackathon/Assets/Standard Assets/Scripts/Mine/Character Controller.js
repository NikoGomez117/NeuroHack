var speed = 20.0f;

function Start ()
{
}

function Update ()
{
	rigidbody.velocity.x = ((transform.GetChild(0).transform.right*Input.GetAxis("Horizontal")*speed)+(transform.GetChild(0).transform.forward*Input.GetAxis("Vertical")*speed)).x;
	rigidbody.velocity.z = ((transform.GetChild(0).transform.right*Input.GetAxis("Horizontal")*speed)+(transform.GetChild(0).transform.forward*Input.GetAxis("Vertical")*speed)).z;
}