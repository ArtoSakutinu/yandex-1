using UnityEngine;

[CreateAssetMenu(menuName ="Item/New item")]
public class ItemSO : ScriptableObject
{
    public int _index;

    public Sprite _graphics;

    public string _name;

    public int _cost;
    public int _money_per_click;
    public int _exp_per_click;

    public bool IsBought { get; set; }
    public bool IsEquiped { get; set; }
}
