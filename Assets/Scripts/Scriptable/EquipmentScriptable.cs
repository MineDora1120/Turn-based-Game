using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Scriptable/Equipment")]
public class EquipmentScriptable : ItemScriptable
{
    [Header("정보 설정")]
    public EquipmentType equipType;
}