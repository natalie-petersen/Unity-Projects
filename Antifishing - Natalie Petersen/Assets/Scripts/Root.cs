using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    public GameObject[] levels;
    public bool isPaused = false;
    public static float minigameDuration = 10f;
    public float timeSinceLastMinigameStart = -minigameDuration;
    public int currentLevel;
    private void Awake()
    {
        currentLevel = 0;
    }
    public void nextLevel()
    {
        Debug.Log("next stage");
        Debug.Log(currentLevel);
        setLevel(currentLevel, currentLevel + 1); 
    }
    public void setLevel(int thisLevel, int targetLevel)
    {
        levels[thisLevel].SetActive(false); //turn off this level
        levels[targetLevel].SetActive(true); //start next level
        currentLevel = targetLevel;
    }
}
