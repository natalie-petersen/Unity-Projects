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
    float winTime = 15f; //seconds until win
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //  clickAction.Enable();
    }

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        if (Time.time >= winTime)
        {
            //win
            GameObject HookRangeGO = GameObject.Find("HookRange");
            Destroy(HookRangeGO);
            //animation
            pos.x = 1;
            while(pos.x < 12 || Time.time >= 18)
            {
                pos.x *= 1.02f;
                this.transform.position = pos;
            }
            UnityEditor.EditorApplication.isPaused = true; //TODO: move back to original scene
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
            UnityEditor.EditorApplication.isPaused = true; //TODO: replace with an actual death screen
        }
    }
    public void click()
    {
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
