using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    [SerializeField] private TMP_Text _moneyPerClickInfo;
    [SerializeField] private TMP_Text _moneyPerSecInfo;

    [SerializeField] private TMP_Text _moneyPerClickButton;
    [SerializeField] private TMP_Text _moneyPerSecButton;

    [SerializeField] private TMP_Text _bgButton;
    [SerializeField] private TMP_Text _charButton;

    public void UpdateUI()
    {
        _moneyText.text = $"демэцх: {GameManager.Instance._playerData.MoneyCount}";

        int click_new_cost = GameManager.Instance._per_click_cost;
        int click_new_income = Mathf.CeilToInt(GameManager.Instance._money_per_click * GameManager.Instance._perClick_income_multiplier);

        _moneyPerClickButton.text = $"сксвьхрэ йкхй дн +{click_new_income} гю {click_new_cost}";

        int passive_new_cost = GameManager.Instance._passive_cost;
        int passive_new_income = Mathf.CeilToInt(GameManager.Instance._money_per_sec * GameManager.Instance._passive_income_multiplier);

        _moneyPerSecButton.text = $"сксвьхрэ днунд дн +{passive_new_income}/яей гю {passive_new_cost}";

        _moneyPerClickInfo.text = $"+{GameManager.Instance._money_per_click} гю йкхй";
        _moneyPerSecInfo.text = $"+{GameManager.Instance._money_per_sec} гю яейсмдс";

        int bg_new_cost = GameManager.Instance._bg_cost;

        _bgButton.text = $"мнбши тнм гю {bg_new_cost}";

        int char_new_cost = GameManager.Instance._character_cost;

        if (char_new_cost < 0)
        {
            char_new_cost = int.MaxValue;
        }

        _charButton.text = $"мнбши оепянмюф гю {char_new_cost}";
    }
}
