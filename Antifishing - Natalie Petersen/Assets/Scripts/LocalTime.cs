using UnityEngine;

public static class LocalTime
{
    public static float localTimeScale = 1f;

    public static float deltaTime => Time.deltaTime * localTimeScale;
    public static float fixedDeltaTime => Time.fixedDeltaTime * localTimeScale;
}
