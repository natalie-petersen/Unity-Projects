using UnityEngine;
using UnityEngine.Rendering;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;
    [SerializeField]
    float winTime = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        if(Time.time >= winTime)
        {
            return;
        }
        else
        {
            pos.x -= speed;
            this.transform.position = pos;
        }
    }

    
}
