#pragma strict

var angVelX : float;
var angVelY : float;
var angVelZ : float;

function Start ()
{
}

function Update ()
{
	rigidbody.angularVelocity = (new Vector3(angVelX,angVelY,angVelZ))*transform.parent.GetComponent(growing).growing;
}