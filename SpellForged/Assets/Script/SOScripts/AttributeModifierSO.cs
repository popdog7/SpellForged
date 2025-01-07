using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeModifier", menuName = "ScriptableObjects/AttributeModifier")]

public class AttributeModifierSO : ScriptableObject
{
    public float damage;
    public float spread;
    public float cooldown;
    public float crit_chance;
}
