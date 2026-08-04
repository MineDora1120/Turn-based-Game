using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image swordImg;
    public Image armorImg;

    private Player player;

    private void Start()
    {
        //Getcomponent와 다른점은
        //Getcomponent -> 현재 이 스크립트가 있는 오브젝트에서 컴포넌트를 가져옴;
        //FindAnyObjectByType -> 씬 내 Player 컴포넌트가 존재만 한다면 불러올 수 있습닌다.
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        swordImg.sprite = player.sword.itemSpr;
        armorImg.sprite = player.armor.itemSpr;
    }
}
