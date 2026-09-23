using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList; //keeping track of the baskets the player has available
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AppleMissed(Apple a)
    {
        if (a.appleType != Apple.AppleTypes.PITFALL)
        {
            //destroy all apples on screen
            GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
            foreach (GameObject tempGO in appleArray)
            {
                Destroy(tempGO);
            }
            //destroy top-most basket
            int basketIdx = basketList.Count - 1;
            GameObject basketGO = basketList[basketIdx];
            basketList.RemoveAt(basketIdx);
            Destroy(basketGO);
            if (basketList.Count == 0)
            {
                SceneManager.LoadScene("_Scene_0");

            }
        }
    }
}
