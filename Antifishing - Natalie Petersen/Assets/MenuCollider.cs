using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Root root = GameObject.FindWithTag("Root").GetComponent<Root>();
        root.isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        //reload the scene
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("levelOne");
        SceneManager.LoadScene("AudioPlayer",LoadSceneMode.Additive);
        
    }
}
