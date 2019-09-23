using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void StateChangedDelegate(State newState);
    public static event StateChangedDelegate OnStateChanged;
    public BlockManager blockManager;
    public InputManager inputManager;
    public ScoreManager scoreManager;
    public UIManager uiManager;

    public enum State
    {
        Menu,
        Play,
        GameOver
    }

    public State state = State.Menu;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Init()
    {
        blockManager.gameObject.SetActive(true);
        blockManager.Init();
        scoreManager.Init();
        state = State.Play;
        OnStateChanged?.Invoke(state);
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void Restart()
    {
        Init();
    }

    private void OnEnable()
    {
        BlockManager.OnBlockOutOfBounds += BlockManager_OnBlockOutOfBounds;
    }

    private void OnDisable()
    {
        BlockManager.OnBlockOutOfBounds -= BlockManager_OnBlockOutOfBounds;

    }


    private void BlockManager_OnBlockOutOfBounds()
    {
        state = State.GameOver;
        OnStateChanged?.Invoke(state);
        blockManager.gameObject.SetActive(false);
    }

}
