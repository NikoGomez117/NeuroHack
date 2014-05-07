

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyUnityOSCListener : MonoBehaviour  {
	public void OSCMessageReceived(OSC.NET.OSCMessage message){	
		//string address = message.Address;

		//Debug.Log(address);
		//foreach( var item in args){
		//	Debug.Log(item);
		//}	
			
		//Handy Information...lol
		ArrayList args = message.Values;
		
		print(args[0]);
		
		/*
		float x_rot = Mathf.Rad2Deg * (-direction.Pitch);
        float y_rot = Mathf.Rad2Deg * (direction.Yaw);
        float z_rot = Mathf.Rad2Deg * (normal.Roll);

		transform.localEulerAngles = new Vector3(x_rot,y_rot,z_rot);
		
		float pos_x = palmPos.x/30.0f;
		float pos_y = (palmPos.y - 300)/30.0f;
		float pos_z = -((palmPos.z - 200)/20.0f) + 1.0f;
		
		transform.localPosition = new Vector3(pos_x,pos_y,pos_z);
		
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
			(transform.parent.GetComponent("SmoothLookAt") as MonoBehaviour).enabled = false;
		}
		else
		{
			powerSelectionState = false;
			(transform.parent.GetComponent("SmoothLookAt") as MonoBehaviour).enabled = true;
		}
		*/
	}
}
