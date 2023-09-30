using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SteelLotus/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string dinosaurName;

    [SerializeField]
    private string dinosaurDescription;

    [SerializeField]
    private Sprite dinosourSprite;

    [SerializeField]
    private float hp;

    [SerializeField]
    private float baseAttack;

    [SerializeField, MinMaxSlider(0, 10)]
    private Vector2 randomElements;

    public string DinosaurName { get => dinosaurName; set => dinosaurName = value; }
    public string DinosaurDescription { get => dinosaurDescription; set => dinosaurDescription = value; }
    public Sprite DinosourSprite { get => dinosourSprite; set => dinosourSprite = value; }
    public float HP { get => hp; set => hp = value; }
    public float BaseAttack { get => baseAttack; set => baseAttack = value; }
    public Vector2 RandomElements { get => randomElements; set => randomElements = value; }


}
