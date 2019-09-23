using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image nextBlockImage;
    public Text scoreText;
    public Text gameStateText;
    public Text instructionsText;

    private void OnEnable()
    {
        BlockManager.OnNextBlockGenerated += BlockManager_OnNextBlockGenerated;
        ScoreManager.OnScoreUpdateEvent += ScoreManager_OnScoreUpdateEvent;
        GameManager.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDisable()
    {
        BlockManager.OnNextBlockGenerated -= BlockManager_OnNextBlockGenerated;
        ScoreManager.OnScoreUpdateEvent -= ScoreManager_OnScoreUpdateEvent;
        GameManager.OnStateChanged -= GameManager_OnStateChanged;

    }

    private void BlockManager_OnNextBlockGenerated(int blockIndex)
    {
        // update ui
        nextBlockImage.gameObject.SetActive(true);
        nextBlockImage.sprite = GameManager.instance.blockManager.blockPrefabs[blockIndex].GetComponent<SpriteRenderer>().sprite;
    }

    private void ScoreManager_OnScoreUpdateEvent(int score)
    {
        scoreText.text = score.ToString("D6");
    }

    private void GameManager_OnStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.GameOver)
        {
            instructionsText.gameObject.SetActive(true);
            instructionsText.text = @"GAME OVER
    'R' - Play / Restart";
        }
        else if (newState == GameManager.State.Menu)
        {
            instructionsText.gameObject.SetActive(true);
            instructionsText.text = @"Arrow Up - Rotate
    Arrow Left - Move Left
    Arrow Right - Move Right
    Arrow Down - Move Down

    'R' - Play / Restart";
        }
        else if (newState == GameManager.State.Play)
        {
            instructionsText.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameStateText.text = GameManager.instance.state.ToString();
    }
}
