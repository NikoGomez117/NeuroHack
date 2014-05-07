using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using Leap;

public class LeapShip : MonoBehaviour {
    Controller controller;
	
    void Start ()
    {
        controller = new Controller();
		//controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
		
		//Start Ship
		rigidbody.velocity = new Vector3(0,0,10);
    }

    void Update ()
    {
		
		// Create Frame
        Frame frame = controller.Frame();
		
		if (!frame.Hands.Empty)
		{
    		// Get the first hand
    		Hand hand = frame.Hands[0];
			
			
			// Check if the hand has any fingers
			FingerList fingers = hand.Fingers;
			if (!fingers.Empty)
			{
    			// Calculate the hand's average finger tip position
    			Vector preFig = fingers[0].TipPosition;
				float totalDis = 0.0f;
				
    			foreach (Finger finger in fingers)
    			{
        			float Distance = preFig.DistanceTo(finger.TipPosition);
					//if(Distance == 0)
					//{
					//	break;
					//}
					totalDis+=Distance;
					preFig = finger.TipPosition;
    			}
				
				//Move Ship
				rigidbody.velocity = transform.forward * 0.005f * (totalDis/fingers.Count)*(totalDis/fingers.Count);
			}
			
			//Check Gestures
			var gestures = frame.Gestures();
			
			var enumGestures = gestures.GetEnumerator();
			
			// Get the hand's normal vector and direction
			
			Vector normal = hand.PalmNormal;
			Vector direction = hand.Direction;
			
			float x = -direction.Pitch * 0.10f / (float)Math.PI;
            float z = normal.Roll * 0.10f / (float)Math.PI;
            float y = direction.Yaw * 0.10f / (float)Math.PI;
			
			transform.RotateAround(transform.right, x);
			transform.RotateAround(transform.forward, z);
			transform.RotateAround(transform.up, y);
			
			//transform.eulerAngles += new Vector3(x, y, z);
		}
	}
}