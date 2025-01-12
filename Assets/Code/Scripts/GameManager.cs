using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private FadingTextBox strikeText;
    [HideInInspector] public int strike = 0;
    [HideInInspector]public float score = 0;
    
    
    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score;
    }

    public void UpdateStrikesLeft()
    {
        strike++;
        strikeText.flash(strike);

        if (strike == 3)
        {
            SceneManager.LoadScene(0);
        }
    }
    
}
