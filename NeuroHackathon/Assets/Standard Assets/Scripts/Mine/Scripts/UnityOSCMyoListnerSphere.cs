using UnityEngine;
using System.Collections;



public class UnityOSCMyoListnerSphere : MonoBehaviour {

	public Transform brush;
	
	public void OSCMessageReceived(OSC.NET.OSCMessage message){
		
		ArrayList args = message.Values;


		//Roll
		print ("Roll");
		print (args[0].ToString());
		//transform.RotateAround(Vector3.zero, transform.forward, int.Parse(args[0].ToString()));
		
		//Pitch
		print ("Pitch");
		print (args[1].ToString());
		//transform.RotateAround(transform.position, transform.right, int.Parse(args[1].ToString()));
		
		//Yaw
		print ("Yaw");
		print (args[2].ToString());
		//transform.RotateAround(transform.position, transform.up, int.Parse(args[2].ToString()));

		/*
		transform.eulerAngles = new Vector3(-int.Parse(args[1].ToString()),-int.Parse(args[2].ToString()),0);

		transform.position = Vector3.zero + transform.forward*10;

		if(int.Parse(args[3].ToString()) == 1)
		{
			Instantiate(brush, transform.position, transform.rotation);
		}
		*/
	}
}
