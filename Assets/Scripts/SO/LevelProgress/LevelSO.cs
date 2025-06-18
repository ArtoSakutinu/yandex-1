using UnityEngine;

[CreateAssetMenu(menuName ="LevelExpNeed/NewLevel")]
public class LevelSO : ScriptableObject
{
    public int _index;

    [Range(0,1000000)]
    public int exp_for_new_level;

    [Range(0, 1000)]
    public int _points_per_click;

    public bool LevelProgressAchievied { get; set; }
}
