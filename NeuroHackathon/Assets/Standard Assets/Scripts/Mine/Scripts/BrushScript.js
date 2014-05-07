#pragma strict
	
var brushTime = 10;
private var killTime = brushTime + Time.time;

function Update ()
{
	if(Time.time > killTime)
	{
		Destroy(gameObject);
	}
}