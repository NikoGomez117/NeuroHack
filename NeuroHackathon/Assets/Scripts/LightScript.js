#pragma strict

var player : Transform;

function Start () {
}

function Update ()
{
	var myLight : Light = transform.GetComponent("Light");
	
	if(Vector3.Distance(player.position,transform.position) < 50)
	{
		myLight.range = 20*player.GetComponent(UnityOSCEmotivListener).frustration;
	}
	else
	{
		myLight.range = 0;
	}
}