using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Game _game;

    private bool _playerButtonDown = false;
    private float _buttonDownTime = 0.0f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _telegraphTransform;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerButtonDown = true;
            _audioSource.Play();
            _telegraphTransform.localRotation = Quaternion.Euler(-67.9f, 0, 0); // Rotate telegraph to indicate button press
        }

        if (Input.GetButtonUp("Jump"))
        {
            _playerButtonDown = false;
            _audioSource.Stop();
            _telegraphTransform.localRotation = Quaternion.Euler(-89.98f, 0, 0);

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

        if (_playerButtonDown)
        {
            _buttonDownTime += Time.deltaTime;
        }

    }

}
