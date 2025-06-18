using TMPro;
using UnityEngine;
using YG;

public class RewardAdsManager : MonoBehaviour
{
    public YandexGame sdk;
    [SerializeField] private GameObject _adPanel;
    [SerializeField] private TMP_Text _adText;

    [SerializeField] private float _delay = 60;
    [SerializeField] private float _maxDelay = 60;

    public void AdButton()
    {
        sdk._RewardedShow(1);
    }

    public void AdButtonCul()
    {
        GameManager.Instance._playerData.MoneyCount *= 2;

        if (GameManager.Instance._playerData.MoneyCount > int.MaxValue)
        {
            GameManager.Instance._playerData.MoneyCount = int.MaxValue;
        }

        if (GameManager.Instance._playerData.MoneyCount < 0)
        {
            GameManager.Instance._playerData.MoneyCount = int.MaxValue;
        }

        GameManager.Instance._uiManager.UpdateUI();
    }

    private void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay <= 3 && _delay > 0)
        {
            if (!_adPanel.activeSelf)
            {
                _adPanel.SetActive(true);
            }

            _adText.text = $"Реклама через: {Mathf.CeilToInt(_delay)}";
        }
        else if (_delay <= 0)
        {
            if (_adPanel.activeSelf)
            {
                _adPanel.SetActive(false);
            }

            YandexGame.FullscreenShow();
            _delay = Random.Range(90, _maxDelay);
        }
    }
}
