using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    // References to AudioClips for various actions
    public AudioClip shootClip;
    public AudioClip sheepHitClip; 
    public AudioClip sheepDroppedClip; 
    public AudioClip reloadClip; 

    private Vector3 cameraPosition; 


    // Awakes are called before Start, this is good when we want to set references.
    // Otherwise we may face problems when referencing the Singleton from other scripts in the Awake.
    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
    }

    private void PlaySound(AudioClip clip)
    {
        // Creates a temporary AudioSource that plays the clip passed as parameters at the location of the cam
        AudioSource.PlayClipAtPoint(clip, cameraPosition); 
    }

    // Methods to actually play the sound effect
    public void PlayShootClip()
    {
        PlaySound(shootClip);
    }

    public void PlaySheepHitClip()
    {
        PlaySound(sheepHitClip);
    }

    public void PlaySheepDroppedClip()
    {
        PlaySound(sheepDroppedClip);
    }

    public void PlayRealoadClip()
    {
        PlaySound(reloadClip);
    }

}
