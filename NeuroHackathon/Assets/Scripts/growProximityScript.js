#pragma strict

var player : Transform;
var proximity : float = 50;

function Start ()
{
	
}

function Update ()
{
	if(Vector3.Distance(player.position,transform.position)<proximity)
	{
		transform.GetComponent(growing).growing = 1;//player.GetComponent(UnityOSCEmotivListener).frustration;
	}
	else
	{
		transform.GetComponent(growing).growing = 0;
	}
}