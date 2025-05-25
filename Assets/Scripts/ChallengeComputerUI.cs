using UnityEngine;
using TMPro;

public class ChallengeComputerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentAlgarism;
    [SerializeField] private TMP_Text _currentMorse;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] GameObject _challengeCompletePanel;
    [SerializeField] GameObject _challengeFailedPanel;

    public void UpdateAlgarism(string algarism)
    {
        if (_currentAlgarism != null)
        {
            _currentAlgarism.text = algarism;
        }
    }

    public void UpdateProgressText(string progress)
    { 
        _progressText.text = progress;
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

    public void ToggleCompleteNotice(bool flag)
    {
        _challengeCompletePanel.SetActive(flag);
    }

    public void ToggleFailedNotice(bool flag)
    {
        _challengeFailedPanel.SetActive(flag);
    }
}
