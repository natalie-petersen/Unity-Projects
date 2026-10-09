using UnityEngine;

public class HookRange : MonoBehaviour
{
    [Header("Inscribed")]
    float speed = 0.075f;
    float leftAndRightEdge = 8f;
    float chanceChangeDirection = 0.15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x += speed;
        if (pos.x < -leftAndRightEdge)
        {
            pos.x = -leftAndRightEdge;
        }
        else if(pos.x > leftAndRightEdge)
        {
            pos.x = leftAndRightEdge;
        }
        this.transform.position = pos;
        if(Random.value < chanceChangeDirection)
        {
            speed *= -1; //change direction
        }

    }
}
