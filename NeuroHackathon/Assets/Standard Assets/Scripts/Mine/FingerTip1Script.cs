using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using Leap;

public class FingerTip1Script : MonoBehaviour {
	
    Controller controller;
	
	/*
	public Transform finger_0;
	public Transform finger_1;
	public Transform finger_2;
	public Transform finger_3;
	*/
	
    void Start ()
    {
        controller = new Controller();
		//controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
		
		//Start Ship
		//rigidbody.velocity = new Vector3(0,0,10);
    }

    void Update ()
    {
		
		// Create Frame
        Frame frame = controller.Frame();
		
		if (!frame.Hands.Empty)
		{
    		Hand hand = frame.Hands[0];
			
			FingerList fingers = hand.Fingers;
			
			if (!fingers.Empty)
			{
				Finger tarFig = fingers[0];
				
    			foreach (Finger finger in fingers)
    			{
					if(finger.TipPosition.x > tarFig.TipPosition.x)
					{
						tarFig = finger;
					}
				}
				
    			float pos_x = tarFig.TipPosition.x/50.0f;
				float pos_y = (tarFig.TipPosition.y - 250)/50.0f;
				float pos_z = -(tarFig.TipPosition.z/50.0f);
				
				Vector3 tarPos = new Vector3(pos_x,pos_y,pos_z);

				
				if(Vector3.Distance(transform.position, tarPos)>.05)
				{
					rigidbody.velocity = (tarPos - transform.position).normalized * 2;
				}
				else
				{
					rigidbody.velocity = Vector3.zero;
				}
			}
		}
	}
}