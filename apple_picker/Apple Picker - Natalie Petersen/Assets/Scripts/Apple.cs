using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;
    //audio stuff
    [SerializeField] private AudioClip missClip;
    [SerializeField] private AudioClip fallClip;
    public enum AppleTypes { APPLE, PERFECT, PITFALL }; //perfect apples are worth double, pitfalls end the round
    [SerializeField] public AppleTypes appleType = AppleTypes.APPLE;
    void Start()
    {
        SoundManager.instance.PlaySoundFXClip(fallClip, transform, 1f);
    }
    void Update()
    {
        if(transform.position.y < bottomY)
        { 
            //play miss sound effect
            SoundManager.instance.PlaySoundFXClip(missClip, transform, 1f);
            //getting the script for apple picker (it's on the camera)
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed(this);

            Destroy(this.gameObject);


        }
    }
}