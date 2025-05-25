using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] private ChallengeComputerUI _challengeComputerUI;
    [SerializeField] private InformationComputerUI _informationComputerUI;

    public static UIManager Instance { get => _instance; }
    public ChallengeComputerUI ChallengeComputerUI { get => _challengeComputerUI; }
    public InformationComputerUI InformationComputerUI { get => _informationComputerUI; }

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
