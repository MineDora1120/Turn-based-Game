using UnityEngine;

public class PlayerCore : MonoBehaviour, IDamage
{
    public PlayerStats stats;

    public void OnDamage(float damage)
    {
    }

    bool Chance(float percent)
    {
        return UnityEngine.Random.value <= percent / 100f;
    }
}