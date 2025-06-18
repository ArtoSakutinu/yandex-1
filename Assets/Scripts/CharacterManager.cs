using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Image _characterHolder;

    public void LoadCharacter()
    {
        var _character = GameManager.Instance._characterData.Find(x => x._equiped);

        if (_character != null)
        {
            EquipCharacter(_character);
        }
    }

    public void SetCharacter(int id)
    {
        var _character = GameManager.Instance._characterData[id];

        if (_character != null)
        {
            EquipCharacter(_character);
        }
    }

    private void EquipCharacter(GameManager.CharacterItem _character)
    {
        _characterHolder.sprite = _character._texture;

        foreach (var item in GameManager.Instance._characterData)
        {
            item._equiped = false;
        }

        _character._equiped = true;
    }
}
