using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishLevel : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    [SerializeField] float hp = 100;
    [SerializeField] float topAndBottomEdge = 5;
    [SerializeField] Root root;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float duration = 2f;
    float elapsed = 0f;
    [SerializeField] Transform endPoint;
    Transform startPoint;
    public bool leavingInProgress = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root.timeSinceLastMinigameStart = -Root.minigameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (root.isPaused) { GetComponent<Animator>().speed = 0; return; }
        else { GetComponent<Animator>().speed = 1f;  }
        if (leavingInProgress)
        {
            if (elapsed < duration)
            {
                //look at the end point
                transform.right = endPoint.position - transform.position;
                elapsed += Time.deltaTime;
                float pct = elapsed / duration;
                //find value for given percentage
                float curveVal = animCurve.Evaluate(pct);
                Debug.Log("MOVING " + curveVal);
                transform.position = Vector2.LerpUnclamped(startPoint.position, endPoint.position, curveVal);
            }
        }
        else
        {
            //move to mouse position (y axis only, fixed x)
            Vector3 mousePos2D = Input.mousePosition;
            //mousePos2D.y = -Camera.main.transform.position.y;
            //convert point from 2D to 3D space
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            //move x position of basket to x position of mouse
            Vector3 pos = this.transform.position;
            pos.y = mousePos3D.y;
            if (pos.y < -topAndBottomEdge)
            {
                pos.y = -topAndBottomEdge;
            }
            if (pos.y > topAndBottomEdge)
            {
                pos.y = topAndBottomEdge;
            }
            this.transform.position = pos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (root.isPaused) { return; }
        if((Time.time - root.timeSinceLastMinigameStart) <= Root.minigameDuration + 1f){ return; } //grace period
        GameObject collidedWith = collision.gameObject;
        Debug.Log("collided");
        if (collidedWith.CompareTag("Obstacle"))
        {
            Debug.Log("ouch");
        }

        if (collidedWith.CompareTag("Hook"))
        {
            Debug.Log("fish");
            SceneManager.LoadScene("fishingMinigame", LoadSceneMode.Additive);
            root.timeSinceLastMinigameStart = Time.time;
            root.isPaused = true;
            
        }
    }
    public void LeaveScreen()
    {
        if (root.isPaused || leavingInProgress) { return; }
        Debug.Log("swim away");
        elapsed = 0f;
        startPoint = this.transform;
        Vector3 pos = this.transform.position;
        pos.y += 3;
        pos.x += 14;
        endPoint.position = pos;
        leavingInProgress = true;
    }
}
