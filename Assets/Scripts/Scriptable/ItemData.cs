using UnityEngine;

public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    public string itemName;
    public Sprite itemSpr;

    [Header("스탯")]
    public PlayerStat addStats;
}
