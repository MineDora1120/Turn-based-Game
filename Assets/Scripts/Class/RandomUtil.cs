using UnityEngine;

public static class RandomUtil
{
    public static bool Chance(float percentage)
    {
        return Random.Range(0f, 100f) < percentage;
    }
}
