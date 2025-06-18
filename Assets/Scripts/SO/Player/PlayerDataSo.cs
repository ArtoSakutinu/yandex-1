using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData/New Data")]
public class PlayerDataSo : ScriptableObject
{
    public int MoneyCount {get; set;}
        
    public float MusicVolume { get; set;}
    public int PlayerExp { get; set;}

    public string _characters { get; set; }
    public string _saved_bgs { get; set; }

    public int _money_per_click;
    public int _money_per_sec;

    public int _passive_cost;
    public int _per_click_cost;

    public int _character_cost;
    public int _bg_cost;

    public int _charEq;
    public int _backgroundEq;
}
