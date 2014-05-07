
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityOSCEmotivListener : MonoBehaviour  {

	public float frustration;

	public void OSCMessageReceived(OSC.NET.OSCMessage message){

		ArrayList args = message.Values;
		if(!(float.Parse(args[0].ToString())==0||float.Parse(args[0].ToString())>1))
		{
			frustration = float.Parse(args[0].ToString())*float.Parse(args[0].ToString());//*float.Parse(args[0].ToString())*float.Parse(args[0].ToString())*float.Parse(args[0].ToString());
		}
	}
}
