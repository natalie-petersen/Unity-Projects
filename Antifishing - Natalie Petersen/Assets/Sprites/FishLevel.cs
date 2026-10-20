using UnityEngine;

public class FishLevel : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;
    [SerializeField]
    float hp = 100;
    [SerializeField]
    float topAndBottomEdge = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move to mouse position (y axis only, fixed x)
        Vector3 mousePos2D = Input.mousePosition;
        //mousePos2D.y = -Camera.main.transform.position.y;
        //convert point from 2D to 3D space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //move x position of basket to x position of mouse
        Vector3 pos = this.transform.position;
        pos.y = mousePos3D.y;
        if(pos.y < -topAndBottomEdge)
        {
            pos.y = -topAndBottomEdge;
        }
        if(pos.y > topAndBottomEdge)
        {
            pos.y = topAndBottomEdge;
        }
        this.transform.position = pos;
    }
}
