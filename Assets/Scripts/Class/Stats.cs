using UnityEngine;

[System.Serializable]
//C# 클래스를 직렬화(Serialization) 가능하게 만드는 속성(Attribute)
//클래스의 데이터를 Inspector 창에 표시하거나 저장할 수 있게 해주는 기능

public class Stats
{
    public int level;
    public int hp;
    public float attackValue;
    public float defenseValue;
    public float turnSpeed;
}

[System.Serializable]
public class PlayerStat : Stats
{
    public float mp;
    public float xp;
    public int criticalPercentage;
    public int missPercentage;

    public static PlayerStat operator +(PlayerStat a, PlayerStat b)
    {
        PlayerStat result = new PlayerStat();

        // 부모 클래스 Stats
        result.hp = a.hp + b.hp;
        result.attackValue = a.attackValue + b.attackValue;
        result.defenseValue = a.defenseValue + b.defenseValue;
        result.turnSpeed = a.turnSpeed + b.turnSpeed;

        // PlayerStat 전용
        result.mp = a.mp + b.mp;
        result.level = a.level + b.level;
        result.xp = a.xp + b.xp;
        result.criticalPercentage = a.criticalPercentage + b.criticalPercentage;
        result.missPercentage = a.missPercentage + b.missPercentage;

        return result;
    }


    // - 연산자 오버로딩
    public static PlayerStat operator -(PlayerStat a, PlayerStat b)
    {
        PlayerStat result = new PlayerStat();

        // 부모 클래스 Stats
        result.hp = a.hp - b.hp;
        result.attackValue = a.attackValue - b.attackValue;
        result.defenseValue = a.defenseValue - b.defenseValue;
        result.turnSpeed = a.turnSpeed - b.turnSpeed;

        // PlayerStat 전용
        result.mp = a.mp - b.mp;
        result.level = a.level - b.level;
        result.xp = a.xp - b.xp;
        result.criticalPercentage = a.criticalPercentage - b.criticalPercentage;
        result.missPercentage = a.missPercentage - b.missPercentage;

        return result;
    }

    // * 연산자 오버로딩
    public static PlayerStat operator *(PlayerStat a, float value)
    {
        PlayerStat result = new PlayerStat();

        // 부모 클래스 Stats
        result.hp = (int)(a.hp * value);
        result.attackValue = a.attackValue * value;
        result.defenseValue = a.defenseValue * value;
        result.turnSpeed = a.turnSpeed * value;

        // PlayerStat 전용
        result.mp = a.mp * value;
        result.level = (int)(a.level * value);
        result.xp = a.xp * value;
        result.criticalPercentage = (int)(a.criticalPercentage * value);
        result.missPercentage = (int)(a.missPercentage * value);

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
    //몇턴 남았나?
    public int remainTurn;
}