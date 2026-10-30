using UnityEngine;
using UnityEngine.SceneManagement;

public class LossCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        //reload the scene
        SceneManager.UnloadSceneAsync("Loss");
        SceneManager.LoadScene("levelOne");
        SceneManager.LoadScene("AudioPlayer", LoadSceneMode.Additive);
    }
}
