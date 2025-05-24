using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Game _game;

    private bool _playerButtonDown = false;
    private float _buttonDownTime = 0.0f;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerButtonDown = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _playerButtonDown = false;

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
