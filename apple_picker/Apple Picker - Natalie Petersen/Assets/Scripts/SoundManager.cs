using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //creating a singleton so this can be called from anywhere w/o instantiation
    public static SoundManager instance;
    [SerializeField] private AudioSource soundObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(this.gameObject); } //when the scene reloads it'll try to create another instance...don't use that one
        DontDestroyOnLoad(this);
    }
    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //make one-off sound object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //assign clip
        audioSource.clip = clip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //destroy clip when finished playing
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
