using UnityEngine;

[System.Serializable]
public enum EquipmentType
{
    Sword,
    Armor
}


[CreateAssetMenu(fileName = "New Equipment", menuName = "Games/Equipment")]
public class EquipmentData : ItemData
{
    [Header("장비 옵션")]
    public EquipmentType equipmentType;
}
