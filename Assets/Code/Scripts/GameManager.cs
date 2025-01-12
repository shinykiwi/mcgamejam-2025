using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private FadingTextBox strikeText;
    [SerializeField] private RectTransform scoreRect;
    [HideInInspector] public int strike = 0;
    [HideInInspector]public float score = 0;

    public AudioSource audioSource;
    
    
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
        scoreRect.DOPunchAnchorPos(Vector2.down * 4, 0.8f, 5);
        if (!audioSource.isPlaying)
        {
           audioSource.Play(); 
        }
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
