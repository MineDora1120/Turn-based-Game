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
}

// 몬스터의 스탯을 담는 클래스
[System.Serializable]
public class MonsterStats : Stats
{
    public float dropXp;
}