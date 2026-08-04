using UnityEngine;

public class Monster : MonoBehaviour, IDamaged
{
    [Header("몬스터 설정")]
    public MonsterStat stat;

    [Header("몬스터 값")]
    //레벨
    public int minLevel = 0;
    public int maxLevel = 0;
    //체력
    public int minHp;
    //공격력
    public int minAttackValue;
    public int multipleAttackValue;

    private void Start()
    {
        stat.level = Random.Range(minLevel, maxLevel+1);

        stat.hp = minHp + stat.level * 10;
        stat.attackValue = minAttackValue + stat.level * multipleAttackValue;
    }

    public void OnDamage(int damage)
    {
        stat.hp -= damage;

        if(stat.hp <= 0)
        {
            //게임 오브젝트를 파괴한다.
            Destroy(this.gameObject);
        }
    }


    //유니티 이벤트 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        //플레이어 일 경우 로직
        GameManager.Instance.OnStartFight(this);
    }
}
