//모든 캐릭터의 스탯을 담는 클래스
[System.Serializable]
public class Stats
{
    public int hp;
    public float attackValue;
    public float defenseValue;
    public float turnSpeed;
}

// 플레이어의 스탯을 담는 클래스
[System.Serializable]
public class PlayerStats : Stats
{
    public int mp;
    public int level;
    public float xp;
    public float criticalAttackPercentage;
    public float missPercentage;

    public static PlayerStats operator +(PlayerStats stats, PlayerStats other)
    {
        PlayerStats result = new PlayerStats();
        result.hp = stats.hp + other.hp;
        result.mp = stats.mp + other.mp;
        result.attackValue = stats.attackValue + other.attackValue;
        result.defenseValue = stats.defenseValue + other.defenseValue;
        result.turnSpeed = stats.turnSpeed + other.turnSpeed;
        result.level = stats.level + other.level;
        result.xp = stats.xp + other.xp;
        result.criticalAttackPercentage = stats.criticalAttackPercentage + other.criticalAttackPercentage;
        result.missPercentage = stats.missPercentage + other.missPercentage;
        return result;
    }

    public static PlayerStats operator -(PlayerStats stats, PlayerStats other)
    {
        PlayerStats result = new PlayerStats();
        result.hp = stats.hp - other.hp;
        result.mp = stats.mp - other.mp;
        result.attackValue = stats.attackValue - other.attackValue;
        result.defenseValue = stats.defenseValue - other.defenseValue;
        result.turnSpeed = stats.turnSpeed - other.turnSpeed;
        result.level = stats.level - other.level;
        result.xp = stats.xp - other.xp;
        result.criticalAttackPercentage = stats.criticalAttackPercentage - other.criticalAttackPercentage;
        result.missPercentage = stats.missPercentage - other.missPercentage;
        return result;
    }
}

// 몬스터의 스탯을 담는 클래스
[System.Serializable]
public class MonsterStats : Stats
{
    public float dropXp;
}