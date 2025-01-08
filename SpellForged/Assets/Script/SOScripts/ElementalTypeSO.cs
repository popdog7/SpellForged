using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalTypeData", menuName = "ScriptableObjects/ElementalTypeData")]
public class ElementalTypeSO : ScriptableObject
{
    public string element_name;
    public Color first_color;
    public Color second_color;
    [Range(0, 4)] public int element_id;
}
