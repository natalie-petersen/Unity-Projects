using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
