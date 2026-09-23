using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    [SerializeField] private AudioClip landClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //looks through scene hierarchy
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        //convert point from 2D to 3D space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //move x position of basket to x position of mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.CompareTag("Apple"))
        {
            var temp = collidedWith.GetComponent<Apple>().appleType;
            if (temp == Apple.AppleTypes.PITFALL)
            {
                //immediate loss, reload
                SceneManager.LoadScene("_Scene_0");
            }
            else
            {
                SoundManager.instance.PlaySoundFXClip(landClip, transform, 1f);
                Destroy(collidedWith);
                if(temp == Apple.AppleTypes.PERFECT)
                {
                    scoreCounter.score += 200;
                }
                else
                {
                    scoreCounter.score += 100;
                }
                HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
            }
            
        }
    }
}
