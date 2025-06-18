using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [Header("UI")]
    //[SerializeField] private Slider _levelUIProgress;
    [SerializeField] private RectTransform _pointsUI;
    [SerializeField] private RectTransform _characterUI;

    public void OnClickAddPoints(CharacterManager clickObject)
    {
        if (clickObject == null)
        {
            return;
        }
        else
        {
            //foreach (ItemSO item in GameManager.Instance._characterData)
            //{
            //    if (item._index == clickObject._character._index)
            //    {
            //        //_levelUIProgress.value += clickObject._character._exp_per_click;
            //        //GameManager.Instance._levelManager.LevelUp();
            //        //GameManager.Instance._playerData.PlayerExp = GameManager.Instance._levelManager._currentExp;
            //    }
            //}

            int _per_click = GameManager.Instance._money_per_click;

            _characterUI.DOShakeRotation(0.1f, 10, 3, 10, true).OnComplete(() => _characterUI.rotation = new Quaternion(0, 0, 0, 0));

            _pointsUI.GetComponentInChildren<TextMeshProUGUI>().text = $"{_per_click}";

            GameManager.Instance._playerData.MoneyCount += _per_click;
            GameManager.Instance._uiManager.UpdateUI();
        }
    }
}
