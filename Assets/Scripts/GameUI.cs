using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("인벤토리 장비")]
    public Image swordImg;
    public Image armorImg;

    [Header("바")]
    public Slider hpBar;
    public Slider mpBar;
    public TMP_Text hpText;
    public TMP_Text mpText;

    [Header("컴포넌트")]
    public TurnSystemUI turnSys;

    public Player player;

    private void Start()
    {
        //Getcomponent와 다른점은
        //Getcomponent -> 현재 이 스크립트가 있는 오브젝트에서 컴포넌트를 가져옴;
        //FindAnyObjectByType -> 씬 내 Player 컴포넌트가 존재만 한다면 불러올 수 있습닌다.
        player = FindAnyObjectByType<Player>();

        //게임 메니저에 턴 시스템 UI 등록
        GameManager.Instance.turnSys = turnSys;
        turnSys.Init();
    }

    // Update is called once per frame
    void Update()
    {
        swordImg.sprite = player.sword.itemSpr;
        armorImg.sprite = player.armor.itemSpr;

        //HpBar
        hpBar.maxValue = player.stat.maxHp;
        hpBar.value = player.stat.hp;
        hpText.text = $"HP: {player.stat.hp}/{player.stat.maxHp}";

        //MpBar
        mpBar.maxValue = player.stat.maxMp;
        mpBar.value = player.stat.mp;
        mpText.text = $"MP: {player.stat.mp}/{player.stat.maxMp}";
    }
}
