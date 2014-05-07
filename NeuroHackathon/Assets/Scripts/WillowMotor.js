

//The object that will be 
var segment : Transform;

var depth : int = 0;
var maxDepth : int;

var lifespan : float;

//Initialize all the parameters for the growing of the plant including functions for how they change with time

var rateOfGrowth : float;
var maxWidth : float;
var splitProb : int;
var stubProb : int;
var maxRotX : float;
var maxRotY : float;
var red : float;
var green : float;
var blue : float;

//List of functions to describe how this values change over time
function lifespanFunct () { return lifespan; }
function rateOfGrowthFunct () { return rateOfGrowth; }
function maxWidthFunct () { return maxWidth/depth + maxWidth; }
function splitProbFunct () { return splitProb; }
function stubProbFunct () { return stubProb; }
function rotXFunct () { return Random.RandomRange(Mathf.Min(maxRotX*(depth/3),25)/2,Mathf.Min(maxRotX*(depth/3),25)); }
function rotYFunct () { return Random.RandomRange(-maxRotY,maxRotY); }
function colorFunct () { return new Color(red, green*depth, blue); }

private var myLifespan : float;
private var hasBranched : boolean = false;

//Initialize Variables
function Start()
{
	depth+=1;
	
	if(maxDepth < depth)
	{
		Destroy(this.gameObject);
	}
	
	myLifespan = lifespan;
	if(!transform.parent.name.Equals("Plant"))
	{
		transform.localPosition = new Vector3(0,transform.parent.Find("Stem").localScale.y*2,0);
	}
	
	transform.Find("Stem").localScale = new Vector3(.1,0.05,.1);
	transform.Find("Stem").localPosition = new Vector3(0,0.05,0);
	transform.Find("Node").localScale = new Vector3(.1,.1,.1);
	transform.Find("Node").localPosition = new Vector3(0,.1,0);
	
	transform.localEulerAngles = new Vector3(rotXFunct(),rotYFunct(),0);
	
	for(var childRenderer : Renderer in GetComponentsInChildren.<Renderer>())
	{
		childRenderer.material = new Material(childRenderer.material);
		childRenderer.material.color = colorFunct();
	}
}

function Update ()
{
	transform.GetComponent(growing).growing = transform.parent.GetComponent(growing).growing;
	
	if(transform.GetComponent(growing).growing == 0)
	{
		transform.Find("Particle System").gameObject.active = false;
	}
	else
	{
		transform.Find("Particle System").gameObject.active = true;
	}
	
	//transform.Find("Particle System").active = true;
	//Check the lifespan of this particular segment
	if(myLifespan>0)
	{
		//Grow Plant along y axis
		transform.Find("Stem").localScale.y += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime/2;
		transform.Find("Stem").transform.localPosition.y += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime/2;
		transform.Find("Node").transform.localPosition.y += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
			
		//Decrement Life
		myLifespan -= Time.deltaTime*transform.GetComponent(growing).growing;
	}
		
	//Check if the stem as branched yet & if its a stub
	else if (!hasBranched)
	{
		if(Random.Range(0,100) > stubProbFunct())
		{
			//Instantiate Next Segment
			var nextSegment : Transform = Instantiate(segment, Vector3.zero, Quaternion.identity);
			
			//Make child of this object's node
			nextSegment.parent = transform;
				
			//Check for splitting
			if(Random.Range(0,100) < splitProbFunct())
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
	//Grow Segment Width & Color
	if(transform.Find("Node").localScale.x < maxWidthFunct())
	{
		transform.Find("Node").localScale.x += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
		transform.Find("Node").localScale.y += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
		transform.Find("Node").localScale.z += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
		transform.Find("Stem").localScale.x += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
		transform.Find("Stem").localScale.z += rateOfGrowthFunct()*transform.GetComponent(growing).growing*Time.deltaTime;
	}
}