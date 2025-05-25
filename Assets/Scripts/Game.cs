using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    [SerializeField] private int _baseDigits = 4;
    [SerializeField][Range(5, 12)] private float _baseAlgarismTime = 7.0f;

    [SerializeField] private int _maxDigits = 10;
    [SerializeField][Range(1, 4)] private float _minAlgarismTime = 3.0f;

    [SerializeField][Range(1, 4)] private float _timeStep = 0.15f;

    [SerializeField] private AudioSource _gameSfxPlayer;

    [SerializeField] private AudioClip _correctInputSfx;
    [SerializeField] private AudioClip _wrongInputSfx;

    private bool _isChallengeActive = false;
    private bool _everyOtherRound = false;
    private int _currentDigits = 4;
    private float _currentAlgarismTime = 7.0f;
    private float _currentTime;
    private float _timer;

    private Dictionary<char, string> _morseCodeDictionary = new Dictionary<char, string>()
    {
        {'0', "-----" },
        {'1', ".----"},
        {'2', "..---"},
        {'3', "...--"},
        {'4', "....-"},
        {'5', "....."},
        {'6', "-...."},
        {'7', "--..."},
        {'8', "---.."},
        {'9', "----."},
        {'A', ".-"},
        {'B', "-..."},
        {'C', "-.-."},
        {'D', "-.."},
        {'E', "."},
        {'F', "..-."},
        {'G', "--."},
        {'H', "...."},
        {'I', ".."},
        {'J', ".---"},
        {'K', "-.-"},
        {'L', ".-.."},
        {'M', "--"},
        {'N', "-."},
        {'O', "---"},
        {'P', ".--."},
        {'Q', "--.-"},
        {'R', ".-."},
        {'S', "..."},
        {'T', "-"},
        {'U', "..-"},
        {'V', "...-"},
        {'W', ".--"},
        {'X', "-..-"},
        {'Y', "-.--"},
        {'Z', "--.."},
    };

    private KeyValuePair<char, string>[] _morseCodeArray;

    private char[] _currentAlgarismCode;
    private char[] _currentMorseCode;
    private int _currentAlgarismIndex = 0;
    private int _currentMorseIndex = 0;

    private void Start()
    {
        _morseCodeArray = new KeyValuePair<char, string>[_morseCodeDictionary.Count];
        _morseCodeArray = _morseCodeDictionary.ToArray();


        ResetTimer();
        StartChallenge(_currentDigits);
        UIManager.Instance.ChallengeComputerUI.UpdateProgressText($"{_currentAlgarismIndex + 1}/{_currentDigits}");
        UIManager.Instance.ChallengeComputerUI.UpdateAlgarism(_currentAlgarismCode[_currentAlgarismIndex].ToString());
        UIManager.Instance.InformationComputerUI.SetSliderMaxValue(_currentTime);
    }

    private void Update()
    {
        if (_isChallengeActive)
        {
            _timer -= Time.deltaTime;
            UIManager.Instance.InformationComputerUI.SetSliderValue(_timer);
        }

        if (_timer <= 0)
        { 
            _isChallengeActive = false;
            StartCoroutine(ChallengeFailed());
        }
    }

    private char GetRandomAlgarism()
    {
        int randomIndex = Random.Range(0, _morseCodeArray.Length);
        return _morseCodeArray[randomIndex].Key;
    }

    private void StartChallenge(int algarismAmount)
    {
        _currentAlgarismCode = new char[algarismAmount];
        for (int i = 0; i < algarismAmount; i++)
        {
            _currentAlgarismCode[i] = GetRandomAlgarism();
        }
        UpdateMorseCode(0);
        _currentAlgarismIndex = 0;
        _currentMorseIndex = 0;
        _isChallengeActive = true;
    }

    private void UpdateMorseCode(int algarismIndex)
    {
        _currentMorseCode = new char[_morseCodeDictionary[_currentAlgarismCode[algarismIndex]].Length];
        for (int i = 0; i < _currentMorseCode.Length; i++)
        {
            _currentMorseCode[i] = _morseCodeDictionary[_currentAlgarismCode[algarismIndex]][i];
        }
    }

    public void CompareInputToCurrentMorse(char playerMorse)
    {
        if (_isChallengeActive == false) return;
        
        if (_currentMorseCode[_currentMorseIndex] == playerMorse)
        {
            Debug.Log($"Correct input: {playerMorse}");
            UIManager.Instance.ChallengeComputerUI.UpdatePlayerMorse(playerMorse.ToString());
            MoveToNextCodeStep();
        }
        else
        {
            Debug.Log($"Incorrect input: {playerMorse}");
            _gameSfxPlayer.PlayOneShot(_wrongInputSfx);
            _currentMorseIndex = 0;
            UIManager.Instance.ChallengeComputerUI.ClearMorse();
        }
    }

    private void MoveToNextCodeStep()
    {
        if (_currentMorseIndex + 1 < _currentMorseCode.Length)
        {
            _currentMorseIndex++;
        }
        else
        {
            if (_currentAlgarismIndex + 1 < _currentAlgarismCode.Length)
            {
                _currentAlgarismIndex++;
                UpdateMorseCode(_currentAlgarismIndex);
                UIManager.Instance.ChallengeComputerUI.UpdateProgressText($"{_currentAlgarismIndex + 1}/{_currentDigits}");
                UIManager.Instance.ChallengeComputerUI.UpdateAlgarism(_currentAlgarismCode[_currentAlgarismIndex].ToString());
                UIManager.Instance.ChallengeComputerUI.ClearMorse();
                _currentMorseIndex = 0;
            }
            else
            {
                Debug.Log("Challenge completed!");
                _isChallengeActive = false;
                StartCoroutine(ChallengeCompleted());
            }
            
        }
    }

    private IEnumerator ChallengeCompleted()
    {
        _gameSfxPlayer.PlayOneShot(_correctInputSfx);

        if (_currentAlgarismTime > _minAlgarismTime)
        {
            _currentAlgarismTime -= _timeStep;
        }
        ResetTimer();

        UIManager.Instance.ChallengeComputerUI.ClearMorse();
        UIManager.Instance.ChallengeComputerUI.ToggleCompleteNotice(true);
        UIManager.Instance.InformationComputerUI.SetSliderMaxValue(_currentTime);

        yield return new WaitForSeconds(5f);
        if (_currentDigits < _maxDigits)
        {
            if (_everyOtherRound)
            { 
                _currentDigits += 1;
            }
        }
        _everyOtherRound = !_everyOtherRound;
        StartChallenge(_currentDigits);
        UIManager.Instance.ChallengeComputerUI.ToggleCompleteNotice(false);
        UIManager.Instance.ChallengeComputerUI.UpdateAlgarism(_currentAlgarismCode[_currentAlgarismIndex].ToString());
        UIManager.Instance.ChallengeComputerUI.UpdateProgressText($"{_currentAlgarismIndex + 1}/{_currentDigits}");
    }

    private IEnumerator ChallengeFailed()
    {
        _gameSfxPlayer.PlayOneShot(_wrongInputSfx);

        _currentDigits = _baseDigits;
        _everyOtherRound = false;
        ResetTimer();

        UIManager.Instance.ChallengeComputerUI.ClearMorse();
        UIManager.Instance.ChallengeComputerUI.ToggleFailedNotice(true);
        UIManager.Instance.InformationComputerUI.SetSliderMaxValue(_currentTime);

        yield return new WaitForSeconds(5f);
        
        StartChallenge(_currentDigits);
        UIManager.Instance.ChallengeComputerUI.ToggleFailedNotice(false);
        UIManager.Instance.ChallengeComputerUI.UpdateAlgarism(_currentAlgarismCode[_currentAlgarismIndex].ToString());
        UIManager.Instance.ChallengeComputerUI.UpdateProgressText($"{_currentAlgarismIndex + 1}/{_currentDigits}");
    }

    private void ResetTimer()
    {
        _currentTime = _currentAlgarismTime * _currentDigits;
        _timer = _currentTime;
    }

}
