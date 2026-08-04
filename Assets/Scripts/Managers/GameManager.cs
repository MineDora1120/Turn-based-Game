using UnityEngine;

[System.Serializable]
public enum TurnAction
{
    MyTurn,
    MonsterTurn
}

public class GameManager : Singleton<GameManager>
{
    [Header("게임 상태 관리")]
    public bool isMoving = true;
    public TurnAction turnStatus;

    [Header("몬스터")]
    public Monster nowMonster;


    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        isMoving = true;
        turnStatus = TurnAction.MyTurn;
    }

    public void OnStartFight(Monster monster)
    {
        nowMonster = monster;
        isMoving = false;
    }

    public void OnGameOver()
    {
        isMoving = false;
    }
}
