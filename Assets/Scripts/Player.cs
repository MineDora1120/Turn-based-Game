using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamaged
{
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("플레이어 설정")]
    public float speed;


    [Header("플레이어 스탯")]
    public PlayerStat stat;


    [Header("플레이어 아이템")]
    public EquipmentData sword;
    public EquipmentData armor;
    public List<PotionData> potionInven;
    public List<Buff> usePotion;

    float direction;

    public void OnDamage(int damage)
    {
        stat.hp -= damage;
        
        if(stat.hp <= 0)
        {
            GameManager.Instance.OnGameOver();
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isMoving)
        {
            //힘 제거
            rb.linearVelocityX = 0;
            anim.SetFloat("Speed", 0);
            return;
        }

        //rigidbody에 힘 추가
        rb.linearVelocityX = speed * Time.deltaTime * direction;

        //애니메이션 컴포넌트에 rigidbody 현재 속도지정
        anim.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    //Player Input
    public void OnMove(InputValue values)
    {
        //A = -1, D = 1
        direction = values.Get<float>();

        if (direction < 0)
        {
            spr.flipX = true;
        } else
        {
            spr.flipX = false;
        }
    }

    #region # Value Logic

    public PlayerStat AttackValueLogic() //플레이어 턴
    {
        PlayerStat _stat = sword.addStats + stat;

        foreach (var item in usePotion)
        {
            _stat.attackValue += _stat.attackValue * (item.attackPercent * 0.01f);
            item.remainTurn--;

            if(item.remainTurn <= 0)
            {
                usePotion.Remove(item);
            }
        }

        //크리티컬 퍼센트 계산
        if(RandomUtil.Chance(_stat.criticalPercentage))
        {
            _stat.attackValue *= 2;
        }

        return _stat;
    }

    public PlayerStat DefenseValueLogic()
    {
        return armor.addStats + stat;
    }

    #endregion # Value Logic

    #region # Potion

    public void OnUsePotion(int index)
    {
        if (potionInven.Count <= index) return;

        PotionData potion = potionInven[index];

        switch(potion.potionType)
        {
            case PotionType.Heal:
                {
                    stat.hp += (int)(stat.hp * (potion.addStats.hp * 0.01f));
                    stat.mp += stat.mp * (potion.addStats.mp * 0.01f);
                } 
                break;
            case PotionType.Strong:
                {
                    Buff buff = new Buff();

                    buff.attackPercent = potion.addStats.attackValue;
                    buff.remainTurn = potion.maxTurn;

                    usePotion.Add(buff);
                }
                break;
            case PotionType.Miss:
                {
                    stat.missPercentage *= 2;
                }
                break;
        }

        potionInven.RemoveAt(index);
    }

    #endregion # Potion
}
