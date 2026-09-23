using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    //used to show the code won't change during runtime (versus Dynamic)
    [Header("Inscribed")]
    //prefab to instantiate apples and/or pitfalls
    public GameObject applePrefab;
    public GameObject perfectApplePrefab;
    public GameObject pitfallPrefab;
    //speed the apple falls (multiplied with gravity)
    public float speed = 1f;
    //distance when the apple turns around
    public float leftAndRightEdge = 10f;
    //chance the apple will change directions
    public float changeDirChance = 0.1f;
    //chance the apple will be perfect (worth double)
    public float perfectAppleChance = 0.2f;
    //chance the apple will be a pitfall (deletes all baskets)
    public float pitfallChance = 0.1f;
    //seconds between apple spawns
    public float appleDropDelay = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //start the game centered
        Vector3 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
        //start dropping apples
        Invoke("DropApple", 2f);

    }

    // Update is called once per frame
    void Update()
    {
        //movement and direction changes
        Vector3 pos = transform.position;
        //get new pos
        pos.x += speed * Time.deltaTime;
        //apply new pos
        transform.position = pos;

        //changing direction
        if(pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); //move right
        }
        else if(pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); //move left
        }
    }
    private void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1; //randomly change direction (random.value will calc a # between 0 and 1 inclusive)
        }
    }

    void DropApple()
    {
        GameObject apple;
        float rand = Random.value;
        float willSpawn = Random.value;
        //settings to check for apple type and spawning the correct version
        if (willSpawn <= 2.5*appleDropDelay)
        {
            if (rand < pitfallChance)
            {
                apple = Instantiate<GameObject>(pitfallPrefab);
            }
            else if (rand < pitfallChance + perfectAppleChance)
            {
                apple = Instantiate<GameObject>(perfectApplePrefab);
            }
            else { apple = Instantiate<GameObject>(applePrefab); }

            apple.transform.position = transform.position;
        }
        //modify speed to make the game more challenging
        if (speed > 0 && Mathf.Abs(speed) < 15)
        {
            speed += 0.1f;
        }
        else if (Mathf.Abs(speed) < 15)
        {
            speed -= 0.1f;
        }
        appleDropDelay = 5 / Mathf.Abs(speed); //starts at 1, gets shorter as speed increases (down to 1/3 sec) 
        //this will call itself after a delay, making it run continuously
        Invoke("DropApple", appleDropDelay);
    }
}
