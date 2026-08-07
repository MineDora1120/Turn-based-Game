using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour, IDamaged
{
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("플레이어 설정")]
    public float speed;
    public float jumpForce;

    [Header("플레이어 스탯")]
    public PlayerStat stat;
    public PlayerStat defaultStat;
    public List<SkillData> skills;


    [Header("플레이어 아이템")]
    public EquipmentData sword;
    public EquipmentData armor;
    public List<PotionData> potionInven;
    public List<Buff> usePotion;

    float direction;
    private bool isGround = false;

    public int nowAttackDamage;

    public void OnDamage(int damage)
    {
        stat.hp -= damage;
        defaultStat.hp -= damage;
        
        if(stat.hp <= 0) //사망
        {
            anim.SetTrigger("Die");
            return;
        } else
        {
            anim.SetTrigger("Damaged");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        defaultStat = stat.Clone();
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

    public void OnFightStart()
    {
        stat = defaultStat.Clone();
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        if (!isGround) return;

        //Rigidbody2D
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);

        isGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        isGround = true;
    }

    public void OnAttackMonster(IDamaged monster)
    {
        //float attackValue = AttackValueLogic().attackValue; << 지역 변수(attackValue)에 값을 저장!
        // 지역변수는 무조건 Private로 저장이 된다. 또한 함수가 끝나면 사라짐!
        // 전역 변수로 선언하면 public으로 선언할 수 있음.
        // public으로 선언하면 다른 곳에서도 참조할 수 있음.
        // 맨 위 변수 선언하는 부분에 public int 형식의 변수를 제작,
        // 그 변수안에 AttackValueLogic().attackValue 값을 저장
        // TurnSystemUI에서 Player를 참조하여 값을 불러옴.

        nowAttackDamage = (int)AttackValueLogic().attackValue; //60

        monster.OnDamage(nowAttackDamage);
    }

    public int OnSkillAttack(SkillData skill, IDamaged monster)
    {
        int _skillDmg = (int)AttackValueLogic().attackValue;
        _skillDmg += (int)(_skillDmg * (skill.stat.attackValue * 0.01f));

        monster.OnDamage(_skillDmg);
        return _skillDmg;
    }

    public void OnSkillStats(SkillData skill)
    {
        stat += stat * (skill.stat * 0.01f);
    }

    public void OnNext(InputValue value)
    {
        if (!value.isPressed) return;

        GameManager.Instance.turnSys.Next();
    }

    public void OnBack(InputValue value)
    {
        if (!value.isPressed) return;

        GameManager.Instance.turnSys.Main();
    }

    public void OnArrowInput(InputValue value)
    {
        Vector2 _vec = value.Get<Vector2>();

        GameManager.Instance.turnSys.ChangeIndex((int)_vec.y, (int)_vec.x);
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
