using System.Collections;
using UnityEngine;

public class PassiveIncome : MonoBehaviour
{
    public void StartIncome()
    {
        StartCoroutine(PassiveIncome_Co());
    }

    private IEnumerator PassiveIncome_Co()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameManager.Instance._playerData.MoneyCount += GameManager.Instance._money_per_sec;

        if (GameManager.Instance._playerData.MoneyCount < 0)
        {
            GameManager.Instance._playerData.MoneyCount = int.MaxValue;
        }

        GameManager.Instance._uiManager.UpdateUI();

        StartCoroutine(PassiveIncome_Co());
    }
}
