using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using Leap;

public class PalmScript : MonoBehaviour {
	
    Controller controller;
	
	bool powerState = false;
	float charge = 0;
	
	string [] powers = {"Vortex","ShockWave","Teleport","Scout","GravityGun","PowerBeam","Null"};
	int powerIndex = 0;
	
	bool powerSelectionState = false;
	
	//GUI HUD
	public Texture powerSelectionTexture;
	
	//GUI Icons
    public Texture templateTexture;
    public Texture vortexTexture;
	public Texture teleportTexture;
	public Texture shockWaveTexture;
	
	//Power Particle Effects
	public GameObject teleportParticleEffect;
	
    void Start ()
    {
        controller = new Controller();
		//controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
    }
	
	//POWERS
	
	void vortex()
	{
		if(powerState)
		{
			transform.audio.mute = false;
			//transform.audio.Play();
			foreach(object obj in GameObject.FindGameObjectsWithTag("Clutter"))
			{
				((GameObject)obj).rigidbody.AddForce(
					(transform.position - ((GameObject)obj).transform.position).normalized * 
					(
						2.0f/
						(
							Vector3.Distance(transform.position,((GameObject)obj).transform.position)
							//* Vector3.Distance(transform.position,((GameObject)obj).transform.position)
							* Time.smoothDeltaTime
						)
					)
				);
				//((GameObject)obj).rigidbody.AddForce((transform.position - ((GameObject)obj).transform.position).normalized * (10000.0f/(Vector3.Distance(transform.position,((GameObject)obj).transform.position)*Vector3.Distance(transform.position,((GameObject)obj).transform.position))) * Time.smoothDeltaTime);;
			}
		}
		else
		{
			transform.audio.mute = true;
		}
	}
	void shockWave()
	{
		if(powerState)
		{
			foreach(object obj in GameObject.FindGameObjectsWithTag("Clutter"))
			{
				((GameObject)obj).rigidbody.AddForce(
					(transform.position - ((GameObject)obj).transform.position).normalized * 
					(
						-2.0f/
						(
							Vector3.Distance(transform.position,((GameObject)obj).transform.position)
							//* Vector3.Distance(transform.position,((GameObject)obj).transform.position)
							* Time.smoothDeltaTime
						)
					)
				);
				//((GameObject)obj).rigidbody.AddForce((transform.position - ((GameObject)obj).transform.position).normalized * (10000.0f/(Vector3.Distance(transform.position,((GameObject)obj).transform.position)*Vector3.Distance(transform.position,((GameObject)obj).transform.position))) * Time.smoothDeltaTime);;
			}
		}
	}
	
	void teleport()
	{
		if(powerState)
		{
			charge+=100.0f*Time.smoothDeltaTime;
		}
		else if(charge > 1)
		{
			charge-=100.0f*Time.smoothDeltaTime;
			charge = Mathf.Max(charge,1.0f);
		}
		
		if(charge>=50.0f)
		{
			transform.parent.transform.parent.position = transform.position;
			transform.parent.parent.rigidbody.velocity = new Vector3(transform.parent.parent.rigidbody.velocity.x,0.0f,transform.parent.parent.rigidbody.velocity.z);
			charge = 0.0f;
			transform.parent.parent.audio.Play();
			GameObject effect = (GameObject)Instantiate(teleportParticleEffect,transform.parent.parent.position,Quaternion.identity);
		}
	}
	
	void OnGUI ()
	{
		//HUD
		
		if(powerSelectionState)
		{		
			GUI.DrawTexture(
				new Rect
				(
					UnityEngine.Screen.width/2 - 32,
					UnityEngine.Screen.height/2 - 32,
					64,
					64
				)
				,powerSelectionTexture, ScaleMode.StretchToFill, true, 10.0F
			);
		}
		
		//PowerState Icons
		
		//--List Indicators
		
		for(int i = 0; i < powers.Length; i++)
		{	
			if(i == powerIndex)
			{
				GUI.DrawTexture(new Rect(10+i*70, 10, 60, 60), powerSelectionTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
			else
			{
				GUI.DrawTexture(new Rect(10+i*70, 10, 60, 60), templateTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
			
			if(powers[i].Equals("Vortex"))
			{
				GUI.DrawTexture(new Rect(10+i*70, 5, 60, 55), vortexTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
			
			if(powers[i].Equals("Teleport"))
			{
				GUI.DrawTexture(new Rect(17+i*70, 15, 50, 50), teleportTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
			
			if(powers[i].Equals("ShockWave"))
			{
				GUI.DrawTexture(new Rect(7+i*70, 8, 66, 66), shockWaveTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
		}
		
		//--PowerState Indicators
		
		if(powers[powerIndex].Equals("Vortex"))
		{	
			if(powerState)
			{
				GUI.DrawTexture(
					new Rect
					(
						UnityEngine.Screen.width/2 - 26,
						UnityEngine.Screen.height/2 - 32,
						55,
						50
					)
					,vortexTexture, ScaleMode.StretchToFill, true, 10.0F
				);
			}
		}
		
		if(powers[powerIndex].Equals("Teleport"))
		{
			GUI.DrawTexture(new Rect(UnityEngine.Screen.width/2 - (charge*28)/100, UnityEngine.Screen.height/2-(30*charge)/100, (60*charge)/100, (60*charge)/100), teleportTexture, ScaleMode.StretchToFill, true, 10.0F);
		}
		
		if(powers[powerIndex].Equals("ShockWave"))
		{	
			if(powerState)
			{
				GUI.DrawTexture(
					new Rect
					(
						UnityEngine.Screen.width/2 - 28,
						UnityEngine.Screen.height/2 - 28,
						56,
						56
					)
					,shockWaveTexture, ScaleMode.StretchToFill, true, 10.0F);
			}
		}
	}
	
    void Update ()
    {
		powerSelectionState = false;
		
		//KEYBOARD INPUT
		
		if(Input.GetButtonDown("Q"))
		{
			powerIndex++;
			if(powerIndex == powers.Length)
			{
				powerIndex=0;
			}
		}
		
		if(Input.GetButtonDown("E"))
		{
			powerIndex--;
			if(powerIndex == -1)
			{
				powerIndex=powers.Length-1;
			}
		}
		
		//LEAP MOTION INPUT
		
		// Create Frame
        Frame frame = controller.Frame();
		
		if (!frame.Hands.Empty)
		{	
    		Hand hand = frame.Hands[0];
			Vector palmPos = hand.PalmPosition;
			
			//Handy Information...lol
			
			Vector normal = hand.PalmNormal;
			Vector direction = hand.Direction;
			
			float x_rot = Mathf.Rad2Deg * (-direction.Pitch);
            float y_rot = Mathf.Rad2Deg * (direction.Yaw);
            float z_rot = Mathf.Rad2Deg * (normal.Roll);

			transform.localEulerAngles = new Vector3(x_rot,y_rot,z_rot);
			
			float pos_x = palmPos.x/30.0f;
			float pos_y = (palmPos.y - 300)/30.0f;
			float pos_z = -((palmPos.z - 200)/20.0f) + 1.0f;
			
			transform.localPosition = new Vector3(pos_x,pos_y,pos_z);
			
			FingerList fingers = hand.Fingers;
			
			if(fingers.Empty)
			{
				//Grasp Powers
				if(powers[powerIndex].Equals("Vortex")||powers[powerIndex].Equals("Teleport")||powers[powerIndex].Equals("ShockWave"))
				{
					powerState = true;
				}
			}
			else
			{
				//Grasp Powers
				if(powers[powerIndex].Equals("Vortex")||powers[powerIndex].Equals("Teleport")||powers[powerIndex].Equals("ShockWave"))
				{
					powerState = false;
				}
			}
			
			//Both
			if(Input.GetButton("Jump"))
			{
				powerIndex = fingers.Count;
				powerSelectionState = true;
				(transform.parent.GetComponent("SmoothLookAt") as MonoBehaviour).enabled = false;
			}
			else
			{
				powerSelectionState = false;
				(transform.parent.GetComponent("SmoothLookAt") as MonoBehaviour).enabled = true;
			}
		}
			
		//Power Activation
		
		if(!powerSelectionState)
		{
			if(powers[powerIndex].Equals("Vortex"))
			{
				vortex();
			}

			if(powers[powerIndex].Equals("Teleport"))
			{
				teleport();
			}

			if(powers[powerIndex].Equals("ShockWave"))
			{
				shockWave();
			}
		}
	}
}