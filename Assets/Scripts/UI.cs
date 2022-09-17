using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestcoreText;
    [SerializeField]
    private Text _scoreTextonPannel;
    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private GameObject _pausePannel;
    private int _score;
    private int _bestscore;
    private int _health;
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("Score")|| !PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.SetInt("Health", 3);

        }else
        {
            _bestscore = PlayerPrefs.GetInt("Score");
            _health = PlayerPrefs.GetInt("Health");
        }

        _healthText.text = _health.ToString();
        _pausePannel.SetActive(false);

    }
    private void Update()
    {
        
        _bestcoreText.text = _bestscore.ToString();
        _health = PlayerPrefs.GetInt("Health");
        _healthText.text = _health.ToString();
        _scoreText.text = _score.ToString();
        _scoreTextonPannel.text = _scoreText.text;
    }
    private void OnEnable()
    {
        Coin._cionAdd += AddScore;
        Ball._openMenu += Pause;
    }
    private void OnDisable()
    {
        Coin._cionAdd -= AddScore;
        Ball._openMenu -= Pause;
    }
    public void AddScore()
    {
         _score+=Coin._value;
    }
    public void PlayGame()
    {
        _pausePannel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ExitGAme() 
    {
        Application.Quit();
    }
    public void Pause()
    {
        Debug.Log(_health);

        if (_health <= 1)
        {
            if (_score >= _bestscore)
            {              
                _bestscore = _score;
                
                 PlayerPrefs.SetInt("Score", _score);
            }
            _pausePannel.SetActive(true);
            Time.timeScale = 0f; 
            
        }
 
             
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        _pausePannel.SetActive(false);
        Time.timeScale = 1f;
    }
}
