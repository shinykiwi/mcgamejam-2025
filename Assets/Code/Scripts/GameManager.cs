using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    
    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        currentScore += 0.1f;
        scoreText.text = ((int)currentScore).ToString();
    }
    
    private int strikesLeft = 3;
    private float currentScore = 0;
    [SerializeField] private GameObject currentPerson;
}
