using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FallingObject : MonoBehaviour {
    public int points;
    public GameObject particles;

    void OnCollisionEnter(Collision other){
        if ( other.gameObject.tag == "floor" ){
            Destroy(gameObject);
        } else if ( other.gameObject.tag == "Player" ){
            string n = renderer.material.name.Split(' ')[0];
            GameManager.instance.PlaySound(n);
            GameObject o = (GameObject) Instantiate(particles,transform.position,Quaternion.identity);
            o.GetComponent<ParticleSystem>().startColor = renderer.material.color;
            GameManager.instance.AddPoints(points,n);
            Destroy(gameObject,0.1f);
        }
    }
}
