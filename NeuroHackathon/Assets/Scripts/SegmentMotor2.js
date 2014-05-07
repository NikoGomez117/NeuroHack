

//The object that will be 
var segment : Transform;

var depth : int = 0;

var lifespan : float;
var lifespanFunct : String = "lifespan";

//Initialize all the parameters for the growing of the plant

var rateOfGrowth : float;
var rateOfGrowthFunct : String = "rateOfGrowth";
var maxWidth : float;
var maxWidthFunct : String = "maxWidth";
var splitProb : int;
var splitProbFunct : String = "splitProb";
var stubProb : int;
var stubProbFunct : String = "stubProb";
var maxRotX : float;
var rotXFunct : String = "Random.RandomRange(-maxRotX,maxRotX)";
var maxRotY : float;
var rotYFunct : String = "Random.RandomRange(-maxRotY,maxRotY)";

private var myLifespan : float;
private var hasBranched : boolean = false;

//Initialize Variables
function Start()
{
	depth+=1;
	
	myLifespan = lifespan;
	transform.localPosition = new Vector3(0,transform.parent.Find("Stem").localScale.y*2,0);
	
	transform.Find("Stem").localScale = new Vector3(.1,0.05,.1);
	transform.Find("Stem").localPosition = new Vector3(0,0.05,0);
	transform.Find("Node").localScale = new Vector3(.1,.1,.1);
	transform.Find("Node").localPosition = new Vector3(0,.1,0);
	
	transform.localEulerAngles = new Vector3(Random.RandomRange(-maxRotX,maxRotX),Random.RandomRange(-maxRotY,maxRotY),0);
}

function Update ()
{
	transform.GetComponent(growing).growing = transform.parent.GetComponent(growing).growing;

	//Check if Growing
	if(transform.GetComponent(growing).growing)
	{
		//transform.Find("Particle System").active = true;
	
		//Check the lifespan of this particular segment
		if(myLifespan>0)
		{
			//Grow Plant along y axis
			transform.Find("Stem").localScale.y += rateOfGrowth*Time.deltaTime/2;
			transform.Find("Stem").transform.localPosition.y += rateOfGrowth*Time.deltaTime/2;
			transform.Find("Node").transform.localPosition.y += rateOfGrowth*Time.deltaTime;
			
			//Decrement Life
			myLifespan -= Time.deltaTime;
		}
		
		//Check if the stem as branched yet & if its a stub
		else if (!hasBranched)
		{
			if(Random.Range(0,100) > stubProb)
			{
				//Instantiate Next Segment
				var nextSegment : Transform = Instantiate(segment, Vector3.zero, Quaternion.identity);
			
				//Make child of this object's node
				nextSegment.parent = transform;
				
				//Check for splitting
				if(Random.Range(0,100) < splitProb)
				{
					//Instantiate Next Segment
					nextSegment = Instantiate(segment, Vector3.zero, Quaternion.identity);
					Destroy(nextSegment.GetChild(nextSegment.childCount-1).gameObject);
					//Make child of this object's node
					nextSegment.parent = transform;
				}
			}
			
			hasBranched = true;
		}
		
		//Grow Segment Width
		if(transform.Find("Node").localScale.x < maxWidth)
		{
			transform.Find("Node").localScale.x += rateOfGrowth*Time.deltaTime;
			transform.Find("Node").localScale.y += rateOfGrowth*Time.deltaTime;
			transform.Find("Node").localScale.z += rateOfGrowth*Time.deltaTime;
			transform.Find("Stem").localScale.x += rateOfGrowth*Time.deltaTime;
			transform.Find("Stem").localScale.z += rateOfGrowth*Time.deltaTime;
		}
	}
	else
	{
		transform.Find("Particle System").active = false;
	}
}