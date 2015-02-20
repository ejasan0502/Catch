using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioClip[] soundClips;

    private AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string n){
        AudioClip clip = GetClip(n);
        if ( clip != null ){
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private AudioClip GetClip(string n){
        for (int i = 0; i < soundClips.Length; i++){
            if ( soundClips[i].name == n ) return soundClips[i];
        }
        return null;
    }
}
