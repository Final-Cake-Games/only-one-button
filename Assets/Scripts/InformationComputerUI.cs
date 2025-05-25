using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class InformationComputerUI : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private AudioSource _sosPlayer;

    public void SetSliderMaxValue(float value)
    { 
        _timerSlider.maxValue = value;
    }

    public void SetSliderValue(float value)
    {
        _timerSlider.value = value;
    }

    public IEnumerator ReceiveMessage(string message)
    {
        if (_sosPlayer != null)
        {
            _sosPlayer.Play();
        }
        string messageToLoad = message;
        _messageText.text = ""; // Clear previous message
        while (messageToLoad.Length > 0)
        { 
            _messageText.text += messageToLoad[0];
            messageToLoad = messageToLoad.Remove(0, 1);
            yield return new WaitForSeconds(0.05f);
        }
        if (_sosPlayer != null)
        {
            _sosPlayer.Stop();
        }
    }
}
