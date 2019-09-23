using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreUpdateDelegate(int score);
    public static event ScoreUpdateDelegate OnScoreUpdateEvent;

    int score = 0;

    private void OnEnable()
    {
        Block.OnBlockRemoved += Block_OnBlockRemoved;
    }

    private void OnDisable()
    {
        Block.OnBlockRemoved -= Block_OnBlockRemoved;
    }

    internal void Init()
    {
        score = 0;
        OnScoreUpdateEvent?.Invoke(this.score);
    }

    private void Block_OnBlockRemoved(int blockScore)
    {
        AddScore(blockScore);
    }

    public void AddScore(int blockScore)
    {
        this.score += blockScore;
        OnScoreUpdateEvent?.Invoke(this.score);
    }
}
