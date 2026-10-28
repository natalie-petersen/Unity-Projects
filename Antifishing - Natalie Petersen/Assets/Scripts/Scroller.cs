using UnityEngine;
using UnityEngine.Rendering;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;
    [SerializeField]
    float winTime = 20f;
    [SerializeField]
    Root root;
    [SerializeField]
    FishLevel fish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (root.isPaused) { return; }
        Vector3 pos = this.transform.position;
        if(pos.x <= -28)
        {
            if (!fish.leavingInProgress) //only call once
            {
                fish.LeaveScreen();
            }
            return;
        }
        else
        {
            pos.x -= speed;
            this.transform.position = pos;
        }
    }

    
}
