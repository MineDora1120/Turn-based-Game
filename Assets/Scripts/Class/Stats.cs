using UnityEngine;

[System.Serializable]
public class Stats
{
    public int level;
    public float hp;
    public float maxHp;
    public float attackValue;
    public float defenseValue;
    public float turnSpeed;
}

[System.Serializable]
public class PlayerStat : Stats
{
    public float mp;
    public float maxMp;
    public float xp;
    public int criticalPercentage;
    public int missPercentage;

    public PlayerStat Clone()
    {
        return new PlayerStat
        {
            // Stats
            level = this.level,
            hp = this.hp,
            maxHp = this.maxHp,
            attackValue = this.attackValue,
            defenseValue = this.defenseValue,
            turnSpeed = this.turnSpeed,

            // PlayerStat
            mp = this.mp,
            maxMp = this.maxMp,
            xp = this.xp,
            criticalPercentage = this.criticalPercentage,
            missPercentage = this.missPercentage
        };
    }

    // + 연산자 오버로딩
    public static PlayerStat operator +(PlayerStat a, PlayerStat b)
    {
        PlayerStat result = a.Clone();

        // 부모 클래스 Stats
        result.level += b.level;
        result.hp += b.hp;
        result.maxHp += b.maxHp; // maxHp 누락 보완
        result.attackValue += b.attackValue;
        result.defenseValue += b.defenseValue;
        result.turnSpeed += b.turnSpeed;

        // PlayerStat 전용
        result.mp += b.mp;
        result.maxMp += b.maxMp; // maxMp 누락 보완
        result.xp += b.xp;
        result.criticalPercentage += b.criticalPercentage;
        result.missPercentage += b.missPercentage;

        return result;
    }

    // - 연산자 오버로딩
    public static PlayerStat operator -(PlayerStat a, PlayerStat b)
    {
        PlayerStat result = a.Clone();

        // 부모 클래스 Stats
        result.level -= b.level;
        result.hp -= b.hp;
        result.maxHp -= b.maxHp; // maxHp 누락 보완
        result.attackValue -= b.attackValue;
        result.defenseValue -= b.defenseValue;
        result.turnSpeed -= b.turnSpeed;

        // PlayerStat 전용
        result.mp -= b.mp;
        result.maxMp -= b.maxMp; // maxMp 누락 보완
        result.xp -= b.xp;
        result.criticalPercentage -= b.criticalPercentage;
        result.missPercentage -= b.missPercentage;

        return result;
    }

    // * (스칼라 곱) 연산자 오버로딩
    public static PlayerStat operator *(PlayerStat a, float value)
    {
        PlayerStat result = a.Clone();

        // 부모 클래스 Stats (int형은 반올림 처리)
        result.level = Mathf.RoundToInt(a.level * value);
        result.hp = Mathf.RoundToInt(a.hp * value);
        result.maxHp = Mathf.RoundToInt(a.maxHp * value); // maxHp 누락 보완
        result.attackValue *= value;
        result.defenseValue *= value;
        result.turnSpeed *= value;

        // PlayerStat 전용
        result.mp *= value;
        result.maxMp *= value; // maxMp 누락 보완
        result.xp *= value;
        result.criticalPercentage = Mathf.RoundToInt(a.criticalPercentage * value);
        result.missPercentage = Mathf.RoundToInt(a.missPercentage * value);

        return result;
    }

    // * (객체 간 곱) 연산자 오버로딩
    public static PlayerStat operator *(PlayerStat a, PlayerStat b)
    {
        PlayerStat result = a.Clone();

        // 부모 클래스 Stats
        result.level *= b.level;
        result.hp *= b.hp;
        result.maxHp *= b.maxHp;
        result.attackValue *= b.attackValue;
        result.defenseValue *= b.defenseValue;
        result.turnSpeed *= b.turnSpeed;

        // PlayerStat 전용
        result.mp *= b.mp;
        result.maxMp *= b.maxMp;
        result.xp *= b.xp;
        result.criticalPercentage *= b.criticalPercentage;
        result.missPercentage *= b.missPercentage;

        return result;
    }
}

[System.Serializable]
public class MonsterStat : Stats
{
    public float dropXp;
}

[System.Serializable]
public class Buff
{
    public float attackPercent;
    public int remainTurn;
}