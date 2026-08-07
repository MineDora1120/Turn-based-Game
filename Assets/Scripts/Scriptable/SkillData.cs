using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Games/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("정보")]
    public string skillName;

    [Header("소모량")]
    public int useMp;

    [Header("능력")]
    public PlayerStat stat;
    public string upgradeStatDesc;
}
