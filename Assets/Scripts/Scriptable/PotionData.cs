using UnityEngine;

[System.Serializable]
public enum PotionType
{
    Heal,
    Strong,
    Miss
}

[CreateAssetMenu(fileName = "New Potion", menuName = "Games/Potion")]
public class PotionData : ItemData
{
    [Header("포션 기능")]
    public PotionType potionType;

    [Header("턴 옵션")]
    public int maxTurn;
}
