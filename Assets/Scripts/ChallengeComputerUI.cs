using UnityEngine;
using TMPro;

public class ChallengeComputerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentAlgarism;
    [SerializeField] private TMP_Text _currentMorse;

    public void UpdateAlgarism(string algarism)
    {
        if (_currentAlgarism != null)
        {
            _currentAlgarism.text = algarism;
        }
    }

    public void UpdatePlayerMorse(string morse)
    {
        if (_currentMorse != null)
        {
            _currentMorse.text += morse;
        }
    }

    public void ClearMorse()
    {
        if (_currentMorse != null)
        {
            _currentMorse.text = "";
        }
    }
}
