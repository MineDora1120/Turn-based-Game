using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SceneType
{
    None,
    Item,
    Skill
}

public class TurnSystemUI : MonoBehaviour
{
    [Header("텍스트들")]
    public TMP_Text[] firstTexts;
    public TMP_Text[] secondTexts;
    public TMP_Text noteText;

    [Header("문자들")]
    public List<string> homeStr;

    private TMP_Text[][] texts;

    [Header("인덱스 값")]
    public int yIndex; //1차원 인덱스
    public int xIndex; //2차원 인덱스

    [Header("UI 컴포넌트")]
    public GameUI gameUI;
    public Slider monsterHpBar;
    public TMP_Text monsterHpText;

    private bool isNoteEnable = false;
    private SceneType selectType;

    public void Init()
    {
        texts = new TMP_Text[2][];

        texts[0] = firstTexts;
        texts[1] = secondTexts;

        yIndex = 0;
        xIndex = 0;

        isNoteEnable = false;
        selectType = SceneType.None;

        gameObject.SetActive(false);
    }

    public void Main()
    {
        if (isNoteEnable) return;

        selectType = SceneType.None;
        isNoteEnable = false;

        SetTexts(homeStr);
    }

    public void Next()
    {
        if (isNoteEnable) return;

        if(selectType == SceneType.Skill)
        {
            SkillData skill = null;

            foreach (var item in gameUI.player.skills)
            {
                if(item.skillName == texts[yIndex][xIndex].text)
                {
                    skill = item;
                    break;
                }
            }

            if (skill == null) return;
            SetTexts(false);

            //스킬 Mp가 플레이어 MP보다 클 때!
            if (skill.useMp > gameUI.player.stat.mp)
            {
                StartCoroutine(NoMPCoroutine(skill));
                return;
            }

            gameUI.player.stat.mp -= skill.useMp;
            gameUI.player.defaultStat.mp -= skill.useMp;

            //AttackValue가 0 초과라면, Attack스킬
            if (skill.stat.attackValue > 0)
            {

                StartCoroutine(SkillAttackCoroutine(skill));
                return;
            }
            //1이상이 아니다
            StartCoroutine(SkillStatCoroutine(skill));
            return;
        }

        switch (texts[yIndex][xIndex].text)
        {
            case "공격":
                {
                    gameUI.player.OnAttackMonster(GameManager.Instance.nowMonster);
                    SetTexts(false);

                    StartCoroutine(AttackCoroutine());
                }
                break;
            case "도망":
                {
                    StartCoroutine(RunCoroutine());
                    break;
                }
            case "스킬":
                {
                    selectType = SceneType.Skill;

                    List<string> _str = new List<string>();

                    foreach (var item in gameUI.player.skills)
                    {
                        _str.Add(item.skillName);
                    }

                    SetTexts(_str);
                }
                break;

            default:
                break;
        }
    }

    public void SetTexts(bool allActive = true)
    {
        for (int i = 0; i < 4; i++)
        {
            texts[i / 2][i % 2].gameObject.SetActive(allActive);
        }
    }

    public void SetTexts(List<string> _list)
    {
        xIndex = 0;
        yIndex = 0;

        noteText.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            if(_list.Count <= i)
            {
                texts[i / 2][i % 2].gameObject.SetActive(false);
                continue;
            }

            texts[i / 2][i % 2].gameObject.SetActive(true);
        }

        for(int i = 0; i < _list.Count; i++)
        {
            texts[i/2][i % 2].text = _list[i];
        }

        UpdateUI();
    }

    public void ChangeIndex(int y, int x)
    {
        yIndex -= y;
        xIndex += x;

        //yIndex가 0 아래가 되면
        // O /
        // /  /

        // /  /
        // 0  /
        if(yIndex < 0)
        {
            yIndex = 1;
        }

        //XIndex가 0 아래가 되면
        // 0 /
        // /  /

        // /  /
        // /  0

        // /  /
        // 0 /

        // / 0
        // / /

        if(xIndex < 0)
        {
            xIndex = 1;
            ChangeIndex(1 , 0);

            return;
        }

        // /  /
        // 0 0
        //아래방향키를

        // 0  0
        // /   /

        if(yIndex >= 2)
        {
            yIndex = 0;
        }

        // /  0
        // /  0

        if(xIndex >= 2)
        {
            xIndex = 0;
            ChangeIndex(1, 0);
        }

        UpdateUI();
    }

    private void Update()
    {
        if (GameManager.Instance.nowMonster == null) return;

        //슬라이더 크가 = 몬스터 최대 HP
        monsterHpBar.maxValue = GameManager.Instance.nowMonster.stat.maxHp;
        //슬라이더 값 = 몬스터 HP
        monsterHpBar.value = GameManager.Instance.nowMonster.stat.hp;

        //Text
        monsterHpText.text = $"HP : {monsterHpBar.value} / {monsterHpBar.maxValue}";
    }

    //키가 눌렸거나, UI 업데이트 요청이 들어왔을때
    public void UpdateUI()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                //모든 행, 열의 텍스트 컬러 초기화
                texts[i][j].color = Color.white;
            }
        }

        //선택된 컬러 변경
        texts[yIndex][xIndex].color = Color.yellow;
    }

    public void Open()
    {
        gameObject.SetActive(true);

        yIndex = 0;
        xIndex = 0;

        SetTexts(homeStr);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    #region # Coroutine
    IEnumerator AttackCoroutine()
    {
        isNoteEnable = true;

        noteText.gameObject.SetActive(true);
        //30
        noteText.text = $"몬스터에게 {gameUI.player.nowAttackDamage}의 데미지가 들어갔다!";

        yield return new WaitForSeconds(1f);

        StartCoroutine(MonsterAttackCoroutine());
    }

    IEnumerator NoMPCoroutine(SkillData skill)
    {
        isNoteEnable = true;

        noteText.gameObject.SetActive(true);
        //30
        noteText.text = $"MP가 부족합니다.. 필요 MP: {skill.useMp}";

        yield return new WaitForSeconds(1f);

        isNoteEnable = false;

        List<string> _str = new List<string>();

        foreach (var item in gameUI.player.skills)
        {
            _str.Add(item.skillName);
        }

        SetTexts(_str);
    }

    IEnumerator SkillAttackCoroutine(SkillData skill)
    {
        isNoteEnable = true;
        selectType = SceneType.None;
        noteText.gameObject.SetActive(true);

        int dmg = gameUI.player.OnSkillAttack(skill, GameManager.Instance.nowMonster);

        noteText.text = $"{skill.skillName}을 사용하여 몬스터에게 {dmg}만큼 피해를 주었다!";

        yield return new WaitForSeconds(1);

        StartCoroutine(MonsterAttackCoroutine());
    }

    IEnumerator SkillStatCoroutine(SkillData skill)
    {
        isNoteEnable = true;
        selectType = SceneType.None;
        noteText.gameObject.SetActive(true);

        gameUI.player.OnSkillStats(skill);

        noteText.text = $"{skill.skillName}을 사용하여 {skill.upgradeStatDesc}를 증가시켰다!";
        yield return new WaitForSeconds(1);

        StartCoroutine(MonsterAttackCoroutine());
    }

    IEnumerator MonsterAttackCoroutine()
    {
        //Monster HP가 0아래면
        if (GameManager.Instance.nowMonster.stat.hp <= 0)
        {
            noteText.text = $"몬스터가 사망했다! 총 {GameManager.Instance.nowMonster.stat.dropXp}만큼의 경험치를 얻었다!";
            yield return new WaitForSeconds(1f);

            isNoteEnable = false;
            GameManager.Instance.OnGameOver(true);
            Close();
        }
        else //아니면
        {
            GameManager.Instance.turnStatus = TurnAction.MonsterTurn;

            GameManager.Instance.nowMonster.OnAttackPlayer(gameUI.player);
            noteText.text = $"{GameManager.Instance.nowMonster.stat.attackValue}의 데미지를 받았다!";

            yield return new WaitForSeconds(1f);

            GameManager.Instance.turnStatus = TurnAction.MyTurn;

            //플레이어 hp가 0이면
            if (gameUI.player.stat.hp <= 0)
            {
                noteText.text = $"플레이어 사망!";
                yield return new WaitForSeconds(1f);

                isNoteEnable = false;
                GameManager.Instance.OnGameOver(false);
                Close();
            }
            else //hp가 0이 아니면
            {
                isNoteEnable = false;
                SetTexts(homeStr);
            }
        }
    }

    IEnumerator RunCoroutine()
    {
        isNoteEnable = true;
        noteText.gameObject.SetActive(true);

        SetTexts(false);
        noteText.text = "도망쳤다...";

        yield return new WaitForSeconds(1);

        isNoteEnable = false;
        Close();
        GameManager.Instance.OnRunPlayer();
    }
    #endregion # Coroutine
}
