using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using Leap;

public class FingerTip3Script : MonoBehaviour {
	
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
	
	Vector3 RotateAroundPoint ( Vector3 point ,   Vector3 pivot ,   Quaternion angle  )
	{
    	return angle * ( point - pivot) + pivot;
	}
	
    void Update ()
    {
		
		// Create Frame
        Frame frame = controller.Frame();
		
		if (!frame.Hands.Empty)
		{
    		Hand hand = frame.Hands[0];
			
			//Handy Information ++++++++++++++++++++
			
			Vector palmPos = hand.PalmPosition;
			
			float palm_x = palmPos.x/50.0f;
			float palm_y = (palmPos.y - 250)/50.0f;
			float palm_z = -(palmPos.z/50.0f);
			
			Vector3 newPalmPos = new Vector3(palm_x,palm_y,palm_z);
			
			Vector normal = hand.PalmNormal;
			Vector direction = hand.Direction;
			
			float x = Mathf.Rad2Deg * (-direction.Pitch);
            float y = Mathf.Rad2Deg * (direction.Yaw);
            float z = Mathf.Rad2Deg * (normal.Roll);

			transform.localEulerAngles = new Vector3(x,y,z);
			
			//++++++++++++++++++++++++++++++++++++++
			
			FingerList fingers = hand.Fingers;
			
			if (!fingers.Empty)
			{
				Finger tarFig = fingers[0];
				
    			foreach (Finger finger in fingers)
    			{
					if(finger.TipPosition.x < tarFig.TipPosition.x)
					{
						tarFig = finger;
					}
				}
				
    			// Calculate the hand's average finger tip position
				
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