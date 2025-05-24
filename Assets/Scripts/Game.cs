using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    [SerializeField][Range(1,8)] private int _baseDigits = 4;
    [SerializeField][Range(2, 5)] private float _baseDigitTime = 5.0f;

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

        StartChallenge(_baseDigits);
        Debug.Log(_currentAlgarismCode[0]);
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
    }

    private void UpdateMorseCode(int algarismIndex)
    {
        Debug.Log(_currentAlgarismCode[_currentAlgarismIndex]);
        _currentMorseCode = new char[_morseCodeDictionary[_currentAlgarismCode[algarismIndex]].Length];
        for (int i = 0; i < _currentMorseCode.Length; i++)
        {
            _currentMorseCode[i] = _morseCodeDictionary[_currentAlgarismCode[algarismIndex]][i];
        }
    }

    public void CompareInputToCurrentMorse(char playerMorse)
    {
        if (_currentMorseCode[_currentMorseIndex] == playerMorse)
        {
            Debug.Log($"Correct input: {playerMorse}");
            MoveToNextCodeStep();
        }
        else
        {
            Debug.Log($"Incorrect input: {playerMorse}");
            // Reset???
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
                _currentMorseIndex = 0;
            }
            else
            {
                Debug.Log("Challenge completed!");
            }
        }
    }


}
