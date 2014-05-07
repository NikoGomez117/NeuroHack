

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityOSCMyoListener : MonoBehaviour  {
	
	public void OSCMessageReceived(OSC.NET.OSCMessage message){

		ArrayList args = message.Values;

		//Roll
		print ("Roll");
		print (args[0].ToString());

		//Pitch
		print ("Pitch");
		print (args[1].ToString());
		
		//Yaw
		print ("Yaw");
		print (args[2].ToString());
	}
}
