using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private enum UpgradeType
    {
        PER_CLICK = 0,
        PER_SEC = 1,
        NEW_CHARACTER = 2,
        NEW_BG = 3
    }

    public void OnClick_Upgrade(int index)
    {
        UpgradeType _type = (UpgradeType)index;

        Upgrade(_type);
    }

    private void Upgrade(UpgradeType _type)
    {
        if (_type == UpgradeType.PER_CLICK)
        {
            int click_new_cost = GameManager.Instance._per_click_cost;

            if (GameManager.Instance._playerData.MoneyCount >= click_new_cost)
            {
                int click_new_income = Mathf.CeilToInt(GameManager.Instance._money_per_click * GameManager.Instance._perClick_income_multiplier);

                GameManager.Instance._money_per_click = click_new_income;
                GameManager.Instance._playerData.MoneyCount -= click_new_cost;

                click_new_cost = Mathf.CeilToInt(GameManager.Instance._per_click_cost * GameManager.Instance._perClick_cost_multiplier);
                GameManager.Instance._per_click_cost = click_new_cost;
            }
        }
        else if (_type == UpgradeType.PER_SEC)
        {
            int passive_new_cost = GameManager.Instance._passive_cost;

            if (GameManager.Instance._playerData.MoneyCount >= GameManager.Instance._passive_cost)
            {
                int passive_new_income = Mathf.CeilToInt(GameManager.Instance._money_per_sec * GameManager.Instance._passive_income_multiplier);

                GameManager.Instance._money_per_sec = passive_new_income;
                GameManager.Instance._playerData.MoneyCount -= passive_new_cost;

                passive_new_cost = Mathf.CeilToInt(GameManager.Instance._passive_cost * GameManager.Instance._passive_cost_multiplier);
                GameManager.Instance._passive_cost = passive_new_cost;
            }
        }
        else if (_type == UpgradeType.NEW_CHARACTER)
        {
            bool _inStock = GameManager.Instance._characterData.Any(x => x._unlocked == false);

            if (_inStock)
            {
                int char_new_cost = GameManager.Instance._character_cost;

                if (GameManager.Instance._playerData.MoneyCount >= GameManager.Instance._character_cost)
                {
                    GameManager.Instance._playerData.MoneyCount -= char_new_cost;
                    GameManager.Instance._character_cost = char_new_cost;

                    UnlockItem(_type);

                    char_new_cost = Mathf.CeilToInt(GameManager.Instance._character_cost * GameManager.Instance._character_cost_multiplier);
                    GameManager.Instance._character_cost = char_new_cost;
                }
            }
        }
        else if (_type == UpgradeType.NEW_BG)
        {
            bool _inStock = GameManager.Instance._backgroundItems.Any(x => x._unlocked == false);

            if (_inStock)
            {
                int bg_new_cost = GameManager.Instance._bg_cost;

                if (GameManager.Instance._playerData.MoneyCount >= GameManager.Instance._bg_cost)
                {
                    GameManager.Instance._playerData.MoneyCount -= bg_new_cost;
                    GameManager.Instance._bg_cost = bg_new_cost;

                    UnlockItem(_type);

                    bg_new_cost = Mathf.CeilToInt(GameManager.Instance._bg_cost * GameManager.Instance._bg_cost_multiplier);
                    GameManager.Instance._bg_cost = bg_new_cost;
                }
            }
        }

        GameManager.Instance._uiManager.UpdateUI();
    }

    private void UnlockItem(UpgradeType _type)
    {
        switch (_type)
        {
            case UpgradeType.NEW_BG:
                {
                    foreach (var item in GameManager.Instance._backgroundItems)
                    {
                        item._equiped = false;
                    }

                    foreach (var item in GameManager.Instance._backgroundItems)
                    {
                        if (!item._unlocked)
                        {
                            item._unlocked = true;
                            item._equiped = true;

                            GameManager.Instance._backgroundImage.sprite = item._texture;

                            break;
                        }
                    }

                    break;
                }

            case UpgradeType.NEW_CHARACTER:
                {
                    foreach (var item in GameManager.Instance._characterData)
                    {
                        item._equiped = false;
                    }

                    foreach (var item in GameManager.Instance._characterData)
                    {
                        if (!item._unlocked)
                        {
                            item._unlocked = true;
                            item._equiped = true;

                            GameManager.Instance._characterManager.LoadCharacter();

                            break;
                        }
                    }

                    break;
                }
        }
    }
}
