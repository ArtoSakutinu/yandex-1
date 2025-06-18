using System;
using UnityEngine;
using YG;

public class SaveService : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public int MoneyCount;
        public int PlayerExp;
        public string characters;
        public string saved_bgs;

        public int money_per_click;
        public int money_per_sec;

        public int passive_cost;
        public int per_click_cost;

        public int character_cost;
        public int bg_cost;

        public int charEq;
        public int backgroundEq;
    }

    public void ApplySaveData(PlayerDataSo playerData, SaveData save)
    {
        playerData.MoneyCount = save.MoneyCount;
        playerData.PlayerExp = save.PlayerExp;
        playerData._characters = save.characters;
        playerData._saved_bgs = save.saved_bgs;

        playerData._money_per_click = save.money_per_click;
        playerData._money_per_sec = save.money_per_sec;

        playerData._passive_cost = save.passive_cost;
        playerData._per_click_cost = save.per_click_cost;

        playerData._character_cost = save.character_cost;
        playerData._bg_cost = save.bg_cost;

        playerData._charEq = save.charEq;
        playerData._backgroundEq = save.backgroundEq;
    }

    public void SavePlayerProgress(PlayerDataSo playerData)
    {
        YandexGame.savesData.MoneyCount = playerData.MoneyCount;
        YandexGame.savesData.PlayerExp = playerData.PlayerExp;
        YandexGame.savesData.characters = playerData._characters;
        YandexGame.savesData.saved_bgs = playerData._saved_bgs;

        YandexGame.savesData.money_per_click = playerData._money_per_click;
        YandexGame.savesData.money_per_sec = playerData._money_per_sec;

        YandexGame.savesData.passive_cost = playerData._passive_cost;
        YandexGame.savesData.per_click_cost = playerData._per_click_cost;

        YandexGame.savesData.character_cost = playerData._character_cost;
        YandexGame.savesData.bg_cost = playerData._bg_cost;

        YandexGame.savesData.charEq = playerData._charEq;
        YandexGame.savesData.backgroundEq = playerData._backgroundEq;

        YandexGame.SaveProgress(); // Сохраняем
    }


    public void LoadPlayerProgress()
    {
        PlayerDataSo playerData = GameManager.Instance._playerData;

        playerData.MoneyCount = YandexGame.savesData.MoneyCount;
        playerData.PlayerExp = YandexGame.savesData.PlayerExp;
        playerData._characters = YandexGame.savesData.characters;
        playerData._saved_bgs = YandexGame.savesData.saved_bgs;

        playerData._money_per_click = YandexGame.savesData.money_per_click;
        playerData._money_per_sec = YandexGame.savesData.money_per_sec;

        playerData._passive_cost = YandexGame.savesData.passive_cost;
        playerData._per_click_cost = YandexGame.savesData.per_click_cost;

        playerData._character_cost = YandexGame.savesData.character_cost;
        playerData._bg_cost = YandexGame.savesData.bg_cost;

        playerData._charEq = YandexGame.savesData.charEq;
        playerData._backgroundEq = YandexGame.savesData.backgroundEq;

        GameManager.Instance.LoadGame();
    }
}
