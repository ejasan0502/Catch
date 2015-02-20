using UnityEngine;
using System.Collections;

public class GenerateObjects : MonoBehaviour {
    public float spawnRate = 0.5f;
    public GameObject[] objects;

    void Start(){
        //StartCoroutine("Generate");
    }

    public IEnumerator Generate(){
        while(true){
            yield return new WaitForSeconds(spawnRate);
        
            if ( objects.Length > 0 ){
                int index = Random.Range(0,objects.Length);
                float x = Random.Range(renderer.bounds.min.x+objects[index].renderer.bounds.size.x/2.0f,
                                       renderer.bounds.max.x-objects[index].renderer.bounds.size.x/2.0f);

                Instantiate(objects[index],new Vector3(x,transform.position.y,transform.position.z),Quaternion.identity);
            } else {
                Debug.Log("No game object references in objects array");
            }
        }
    }
}
