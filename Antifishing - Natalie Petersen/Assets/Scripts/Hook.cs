using UnityEditor;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField]
    float speed = 0.05f;
    [SerializeField]
    float leftAndRightEdge = 1.5f;
    [SerializeField]
    float chanceChangeDirection = 0.0025f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.y += speed;
        if (pos.y < -leftAndRightEdge)
        {
            pos.y = -leftAndRightEdge;
        }
        else if (pos.y > leftAndRightEdge)
        {
            pos.y = leftAndRightEdge;
        }
        this.transform.position = pos;
        if (Random.value < chanceChangeDirection || Mathf.Abs(pos.y) == leftAndRightEdge)
        {
            speed *= -1; //change direction
        }
    }

}
