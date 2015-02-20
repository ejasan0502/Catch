using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {
    public GameObject background;
    public float speed = 1f;

    public Material[] mats;
    private Dictionary<string,Material> materials = new Dictionary<string,Material>();

    void Start(){
        if ( mats.Length > 0 ){
            foreach (Material m in mats){
               materials.Add(m.name,m);
            }
        }
    }

    void Update(){
        if ( Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ){
			float h = Input.acceleration.x;
			transform.position += new Vector3(h,0,0);
		} else {
            float h = Input.GetAxis("Horizontal");
            transform.position += new Vector3(h,0,0);
		}

		if ( transform.position.x - renderer.bounds.size.x/2.0f < background.renderer.bounds.min.x ){
			transform.position = new Vector3(background.renderer.bounds.min.x+renderer.bounds.size.x/2.0f,
				                                transform.position.y,
				                                transform.position.z);
		} else if ( transform.position.x + renderer.bounds.size.x/2.0f > background.renderer.bounds.max.x ){
			transform.position = new Vector3(background.renderer.bounds.max.x-renderer.bounds.size.x/2.0f,
				                                transform.position.y,
				                                transform.position.z);
		}
    }

    public void SetColor(string s){
        renderer.sharedMaterial = materials[s];
    }
}
