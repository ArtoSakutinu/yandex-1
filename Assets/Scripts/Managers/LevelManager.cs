using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int _currentLevel;
    [HideInInspector] public int _currentExp;

    //public void LevelUp()
    //{
    //    for (int i = 0; i < GameManager.Instance._levels.Count; i++)
    //    {
    //        var _level = GameManager.Instance._levels[i];

    //        if (_level.LevelProgressAchievied.Equals(false))
    //        {
    //            if (_currentExp >= _level.exp_for_new_level)
    //            {
    //                _currentLevel += 1;
    //                _level.LevelProgressAchievied.Equals(true);
    //            }
    //        }
    //    }
    //}

    private void OnDestroy()
    {
        SavePlayerData();
    }

    public void LoadPlayerData()
    {
        _currentExp = GameManager.Instance._playerData.PlayerExp;
    }

    public void SavePlayerData()
    {
        GameManager.Instance._playerData.PlayerExp = _currentLevel;
    }
}
