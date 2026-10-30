using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;
    [SerializeField]
    float leftAndRightEdge = 10f;
    [SerializeField]
    float push = 2f;
    float lastClick = 0f; //used to calculate delta time
    [SerializeField]
    float hp = 100;
    [SerializeField]
    float dmg = 10f;
    [SerializeField]
    private float timeSinceSceneLoad;
    [SerializeField]
    private AudioSubclipPlayer soundPlayer;
    [SerializeField]
    AudioSource audioSource;
    AudioClip[] swimClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeSinceSceneLoad = Time.time;
        swimClips = Resources.LoadAll<AudioClip>("fish_swim"); //load all sounds from fish_swim folder
    }

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        Root root = GameObject.FindWithTag("Root").GetComponent<Root>();
        if ((Time.time-timeSinceSceneLoad) >= Root.minigameDuration)
        {
            Debug.Log("unloading, time is " + Time.timeSinceLevelLoadAsDouble);
            //animation
            pos.x = 1;
            while(pos.x < 12)
            {
                pos.x *= 1.02f;
                this.transform.position = pos;
            }
            SceneManager.UnloadSceneAsync("fishingMinigame");
            root.isPaused = false;
            
        }
        //move left
        
        pos.x -= speed;
        if(pos.x < -leftAndRightEdge)
        {
            pos.x = -leftAndRightEdge;
        }
        this.transform.position = pos;
        doFlip();
        if(hp <= 0)
        {
            //die
            Quaternion r = this.transform.localRotation;
            r.x = 180;
            this.transform.localRotation = r;
            SceneManager.LoadScene("Loss",LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("AudioPlayer");
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject obj in objs)
            {
                obj.SetActive(false);
            }
            root.isPaused = true;
            SceneManager.UnloadSceneAsync("fishingMinigame");
        }
    }
    public void click()
    {
        AudioClip randSfx = swimClips[UnityEngine.Random.Range(0, swimClips.Length)];
        audioSource.PlayOneShot(randSfx);
        lastClick = Time.time;
        Vector3 pos = this.transform.position;
        pos.x += push;
        if (pos.x > leftAndRightEdge)
        {
            pos.x = leftAndRightEdge;
        }
        this.transform.position = pos;
        doFlip();
    }
    public void doFlip()
    {
        Vector3 scale = this.transform.localScale;
        float clickDelta = Time.time - lastClick;
        scale.x = Math.Abs(scale.x) * ((clickDelta < 0.33) ? 1 : -1); //flip the fish if clicks stall
        this.transform.localScale = scale;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject collidedObj = other.gameObject;
        if (collidedObj.CompareTag("HealthDraining"))
        {
            //take damage (keep in mind this is happening on loop, so not too much at a time)
            hp -= dmg;
        }
    }
}
