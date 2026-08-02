using UnityEngine;

public class ItemScriptable : ScriptableObject
{
    [Header("아이템 표시 설정")]
    public Sprite itemSpr;

    public string itemName;

    [Header("추가 능력")]
    public PlayerStats addStats;
}