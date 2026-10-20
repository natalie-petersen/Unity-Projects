using UnityEngine;
using UnityEngine.UIElements;

public class Collider : MonoBehaviour
{
    Fish fishGO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishGO = GameObject.Find("Fish").GetComponent<Fish>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        fishGO.click();
    }
}
