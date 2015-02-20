using UnityEngine;
using System.Collections;

public class ModifyPlane : MonoBehaviour {

	private float w, h;			// Width and Height

	void Start(){
		w = 0;
		h = 0;
		
		// Get Height and Width
		h = 2 * Camera.main.orthographicSize;
		w = h * Camera.main.aspect;
		modifyPlane(gameObject,true);
	}

	// Modify plane's position, width, and height to Aspect Ratio 3:2 or 2:3
	// Requirements:
	// 	1.) Camera must be orthographic
	// 	2.) Camera must have an orthographic size of 10
	// 	3.) Plane modified must be created on an aspect ratio of 3:2 or 2:3
	public void modifyPlane (GameObject obj, bool scale)
	{
		h = 2 * Camera.main.orthographicSize;
		w = h * Camera.main.aspect;
		Vector3 center = obj.transform.position;
		Vector3 centerP = new Vector3();
		Vector3 centerN = new Vector3();

		if (Screen.orientation == ScreenOrientation.Landscape || 
		    Screen.orientation == ScreenOrientation.LandscapeLeft || 
		    Screen.orientation == ScreenOrientation.LandscapeRight ){
			centerP.x = (center.x + 30.0f*0.5f)/30.0f;
			centerP.y = (center.y + 20.0f*0.5f)/20.0f;
			
			centerN.x = w * centerP.x - w/2.0f;
			centerN.y = h * centerP.y - h/2.0f;
			centerN.z = center.z;
		} else if (Screen.orientation == ScreenOrientation.Portrait ||
		           Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			centerP.x = (center.x + 13.33f*0.5f)/13.33f;
			centerP.y = (center.y + 20.0f*0.5f)/20.0f;
			
			centerN.x = w * centerP.x - w/2.0f;
			centerN.y = h * centerP.y - h/2.0f;
			centerN.z = center.z;
		}
		
		Bounds b = renderer.bounds;
		float width = b.max[0] - b.min[0];
		float height = b.max[1] - b.min[1];
		
		float pWidth = 0.0f;
		float pHeight = 0.0f;

		if (Screen.orientation == ScreenOrientation.Landscape || 
		    Screen.orientation == ScreenOrientation.LandscapeLeft || 
		    Screen.orientation == ScreenOrientation.LandscapeRight ){
			pWidth = width/30.0f;
			pHeight = height/20.0f;
		} else if (Screen.orientation == ScreenOrientation.Portrait ||
		           Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			pWidth = width/13.33f;
			pHeight = height/20.0f;
		}
		
		float newWidth = pWidth*w;
		float newHeight = pHeight*h;
		
		float scaleFactorX = newWidth / width;
		float scaleFactorY = newHeight / height;
		
		//Vector3 v = new Vector3( centerN.x,centerN.y,centerN.z);
		//obj.transform.position = v;
		if(scale) {
			obj.transform.localScale = new Vector3(obj.transform.localScale.x * (scaleFactorX),  obj.transform.localScale.y * (scaleFactorY),1);
		}
	}
}

