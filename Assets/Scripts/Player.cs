using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Game _game;

    private bool _playerButtonDown = false;
    private float _buttonDownTime = 0.0f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _telegraphButton;
    [SerializeField] private MeshRenderer _telegraphRenderer;

    private void Update()
    {
        if (_telegraphRenderer.isVisible)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _playerButtonDown = true;
                _audioSource.Play();
                _telegraphButton.localRotation = Quaternion.Euler(-67.9f, 0, 0); // Rotate telegraph to indicate button press
            }

            if (Input.GetButtonUp("Jump"))
            {
                _playerButtonDown = false;
                _audioSource.Stop();
                _telegraphButton.localRotation = Quaternion.Euler(-89.98f, 0, 0);

                if (_buttonDownTime <= 0.2f)
                {
                    _game.CompareInputToCurrentMorse('.');
                }
                else
                {
                    _game.CompareInputToCurrentMorse('-');
                }

                _buttonDownTime = 0.0f;
            }
        }
        else
        { 
            if (_playerButtonDown == true)
            { 
                _playerButtonDown = false; // Reset button state if telegraph is not visible
                _audioSource.Stop();
                _telegraphButton.localRotation = Quaternion.Euler(-89.98f, 0, 0);
                if (_buttonDownTime <= 0.2f)
                {
                    _game.CompareInputToCurrentMorse('.');
                }
                else
                {
                    _game.CompareInputToCurrentMorse('-');
                }

                _buttonDownTime = 0.0f;
            }
        }


        if (_playerButtonDown)
        {
            _buttonDownTime += Time.deltaTime;
        }

    }

}
