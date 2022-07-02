using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // handle to the text 
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _listOfLiveSprtes;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _gameRestartText;

    //handle to game manager

    private GameManager _gameManager;


    
    
    // Start is called before the first frame update
    void Start()
    {
        // assign text component to the handle 
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_manager").GetComponent<GameManager>();

        if (_gameManager == null) 
        {
            Debug.Log("Game manaager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //_scoreText.text = "Score: " + Player.
        
    }

    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        // display lives
        // give it a new one basied on number of lives. switch statment 
        _LivesImage.sprite = _listOfLiveSprtes[currentLives];
        if (currentLives == 0)
        {
            gameOverSequence();
        }

    }

    void gameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _gameRestartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
           yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
           yield return new WaitForSeconds(0.5f);

        }
    }

    
}
