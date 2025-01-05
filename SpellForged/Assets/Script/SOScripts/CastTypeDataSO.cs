using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CastTypeData", menuName = "ScriptableObjects/CastTypeData")]
public class CastTypeDataSO : ScriptableObject
{
    public Transform spell_prefab;
    public float damage;
    public float range;
    public float cooldown;
    public float crit_chance;
}
