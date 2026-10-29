using System;
using System.Collections;
using UnityEngine;

//I have a couple files that are long clips of evenly spaced soundbytes and want to play a random soundbyte at a given point in time. This is a utility class to help me do that :)
[RequireComponent(typeof(AudioSource))]
public class AudioSubclipPlayer : MonoBehaviour
{
    [SerializeField]
    private float numberClips;
    [SerializeField]
    private float clipTime;
    private AudioSource audioSource;
    private Coroutine playCoroutine;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private IEnumerator playClip(float start)
    {
        if (!audioSource) { yield break; }

        audioSource.Play(); //will default to 0 on play, so start playing the clip BEFORE seeking!!
        audioSource.time = start;
        Debug.Log("running at " + audioSource.time);
        yield return new WaitForSeconds(clipTime);
        audioSource.Stop();
    }
    public void playRandomClip()
    {
        float clipStart = clipTime * UnityEngine.Random.Range(0,numberClips-1);
        Debug.Log(clipStart);
        if(playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine(playClip(clipStart));
    }
}
