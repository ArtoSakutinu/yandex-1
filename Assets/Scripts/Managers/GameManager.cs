using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Sprite[] _characters;
    public List<CharacterItem> _characterData = new List<CharacterItem>();

    public Sprite[] _backgrounds;
    public List<BackgroundItem> _backgroundItems = new List<BackgroundItem>();

    public Image _backgroundImage;

    public PlayerDataSo _playerData;

    [Header("COST SETTINGS")]
    public int _money_per_click = 1;
    public int _money_per_sec = 1;

    public int _bg_cost = 10;
    public int _character_cost = 50;

    public int _per_click_cost = 25;
    public int _passive_cost = 25;

    [Header("COST MULTIPLIERS SETTINGS")]
    public float _perClick_cost_multiplier = 1.5f;
    public float _perClick_income_multiplier = 1.25f;

    public float _passive_cost_multiplier = 1.5f;
    public float _passive_income_multiplier = 1.25f;

    public float _bg_cost_multiplier = 1.5f;
    public float _character_cost_multiplier = 1.7f;

    [Header("Script references")]
    public CharacterManager _characterManager;
    public LevelManager _levelManager;
    public UIManager _uiManager;
    public PassiveIncome _passiveIncome;
    public SaveService _saveService;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (YandexGame.SDKEnabled)
        {
            _saveService.LoadPlayerProgress();
        }

        Init();
    }

    private void Init()
    {
        _characterManager?.LoadCharacter();
        _levelManager?.LoadPlayerData();
        _uiManager?.UpdateUI();
        _passiveIncome?.StartIncome();

        InitBackgrounds();
        InitCharacters();
    }

    private void OnEnable() => YandexGame.GetDataEvent += _saveService.LoadPlayerProgress;
    private void OnDisable() => YandexGame.GetDataEvent -= _saveService.LoadPlayerProgress;

    private void InitBackgrounds()
    {
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            BackgroundItem _item = new BackgroundItem(i, _backgrounds[i]);

            _backgroundItems.Add(_item);
        }

        _backgroundItems[0]._equiped = true;
        _backgroundItems[0]._unlocked = true;
    }

    private void InitCharacters()
    {
        for (int i = 0; i < _characters.Length; i++)
        {
            CharacterItem _character = new CharacterItem(i, _characters[i]);

            _characterData.Add(_character);
        }

        _characterData[0]._unlocked = true;
        _characterData[0]._equiped = true;

        _characterManager.SetCharacter(0);
    }

    public void ResetProgress()
    {
        _playerData.PlayerExp = 0;
        _playerData.MoneyCount = 0;

        _playerData._characters = "";
        _playerData._saved_bgs = "";

        _playerData._money_per_sec = 1;
        _playerData._money_per_click = 1;
        _money_per_sec = _playerData._money_per_sec;
        _money_per_click = _playerData._money_per_click;

        _playerData._passive_cost = 75;
        _playerData._per_click_cost = 50;
        _playerData._character_cost = 75;
        _playerData._bg_cost = 50;

        _passive_cost = _playerData._passive_cost;
        _per_click_cost = _playerData._per_click_cost;
        _character_cost = _playerData._character_cost;
        _bg_cost = _playerData._bg_cost;

        _playerData._charEq = 0;
        _playerData._backgroundEq = 0;


        foreach (var _char in _characterData)
        {
            _char._unlocked = false;
        }

        foreach (var _bg in _backgroundItems)
        {
            _bg._unlocked = false;
        }

        _characterData[0]._unlocked = true;
        _backgroundItems[0]._unlocked = true;

        _uiManager.UpdateUI();
        _characterManager.SetCharacter(0);
        _characterManager.LoadCharacter();
        _backgroundImage.sprite = _backgroundItems.Find(x => x._id == 0)._texture;

        YandexGame.savesData = new SavesYG(); // заменяем на новый пустой объект
        YandexGame.SaveProgress(); // сохраняем чистый прогресс
        SceneManager.LoadScene(0);
    }

    public void CharacterToggle(int increment)
    {
        int _currentCharacterIndex = _characterData.FindIndex(x => x._equiped);

        foreach (var _char in _characterData)
        {
            _char._equiped = false;
        }

        List<CharacterItem> _unlockedCharacters = _characterData.FindAll(x => x._unlocked);

        int _newIndex = Mathf.Clamp(_currentCharacterIndex + increment, 0, _unlockedCharacters.Count - 1);

        var _foundedChar = _characterData.Find(x => x._texture == _unlockedCharacters[_newIndex]._texture);
        _foundedChar._equiped = true;

        _characterManager.LoadCharacter();
    }

    public void BackgroundToggle(int increment)
    {
        int _currentBgIndex = _backgroundItems.FindIndex(x => x._equiped);

        foreach (var _bg in _backgroundItems)
        {
            _bg._equiped = false;
        }

        List<BackgroundItem> _unlockedBgs = _backgroundItems.FindAll(x => x._unlocked);

        int _newIndex = Mathf.Clamp(_currentBgIndex + increment, 0, _unlockedBgs.Count - 1);

        var _foundedBg = _unlockedBgs[_newIndex];
        _foundedBg._equiped = true;

        _backgroundImage.sprite = _foundedBg._texture;
    }

    private void OnApplicationQuit()
    {
        _saveService.SavePlayerProgress(_playerData);
    }

    [Obsolete]
    private void Save()
    {
        _playerData._money_per_sec = _money_per_sec;
        _playerData._money_per_click = _money_per_click;
        _playerData._passive_cost = _passive_cost;
        _playerData._per_click_cost = _per_click_cost;

        _playerData._character_cost = _character_cost;
        _playerData._bg_cost = _bg_cost;

        string _saved_characters = "";
        string _saved_bgs = "";

        foreach (var _char in _characterData)
        {
            if (_char._unlocked)
            {
                _saved_characters += $"{_char._id};";
            }
        }

        _saved_characters.Remove(_saved_characters.Length - 1, 1);

        foreach (var _bg in _backgroundItems)
        {
            if (_bg._unlocked)
            {
                _saved_bgs += $"{_bg._id};";
            }
        }

        _saved_bgs.Remove(_saved_bgs.Length - 1, 1);

        _playerData._characters = _saved_characters;
        _playerData._saved_bgs = _saved_bgs;

        _playerData._charEq = _characterData.Find(x => x._equiped)._id;
        _playerData._backgroundEq = _backgroundItems.Find(x => x._equiped)._id;
    }
    
    public void LoadGame()
    {
        int _charEquiped = _playerData._charEq;
        int _bgEquiped = _playerData._backgroundEq;

        string[] _saved_characters = _playerData._characters.Split(';');
        string[] _saved_bgs = _playerData._saved_bgs.Split(';');

        for (int i = 0; i < _saved_characters.Length; i++)
        {
            if (int.TryParse(_saved_characters[i], out int _charId))
            {
                _characterData.Find(x => x._id == _charId)._unlocked = true;
            }
        }

        for (int i = 0; i < _saved_bgs.Length; i++)
        {
            if (int.TryParse(_saved_bgs[i], out int _bgId))
            {
                _backgroundItems.Find(x => x._id == _bgId)._unlocked = true;
            }
        }

        _characterData.Find(x => x._id == _charEquiped)._equiped = true;
        var _curBg = _backgroundItems.Find(x => x._id == _bgEquiped);
        _curBg._equiped = true;

        _characterManager.SetCharacter(_charEquiped);
        _backgroundImage.sprite = _curBg._texture;

        _bg_cost = _playerData._bg_cost;
        _character_cost = _playerData._character_cost;
        _passive_cost = _playerData._passive_cost;

        _per_click_cost = _playerData._per_click_cost;
        _money_per_click = _playerData._money_per_click;

        _passive_cost = _playerData._passive_cost;
        _money_per_sec = _playerData._money_per_sec;

        _uiManager.UpdateUI();
    }

    [Serializable]
    public class BackgroundItem
    {
        public int _id;
        public Sprite _texture;
        public bool _unlocked = false;
        public bool _equiped = false;

        public BackgroundItem(int id, Sprite texture)
        {
            _id = id;
            _texture = texture;
        }
    }

    [Serializable]
    public class CharacterItem
    {
        public int _id;
        public Sprite _texture;
        public bool _unlocked = false;
        public bool _equiped = false;

        public CharacterItem(int id, Sprite texture)
        {
            _id = id;
            _texture = texture;
        }
    }
}
