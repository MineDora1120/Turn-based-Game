using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("컴포넌트")]
    public TurnSystemUI turnSys;

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

        turnSys.Open();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        player.OnFightStart();
    }

    public void OnGameOver(bool playerWin = false)
    {
        nowMonster = null;
        isMoving = playerWin;

        if(!playerWin)
        {
            isMoving = true;
            SceneManager.LoadScene("Main");
        }
    }

    public void OnRunPlayer()
    {
        nowMonster = null;
        isMoving = true;
    }
}
