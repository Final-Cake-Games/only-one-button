using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] private ChallengeComputerUI _challengeComputerUI;

    public static UIManager Instance { get => _instance; }
    public ChallengeComputerUI ChallengeComputerUI { get => _challengeComputerUI; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
