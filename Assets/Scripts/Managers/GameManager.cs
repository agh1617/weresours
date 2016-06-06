using UnityEngine;
using System.Collections.Generic;

public enum GameState { started, paused, stopped }
public enum GameType { singleplayer, multiplayer }

public static class GameManager
{
    static GameState gameState = GameState.stopped;
    static GameType gameType = GameType.singleplayer;
    static int playersCount = 1;

    public static GameState GetGameState()
    {
        return gameState;
    }

    public static void SetGameState(GameState gameState)
    {
        GameManager.gameState = gameState;
        if (gameState == GameState.started) Time.timeScale = 1;
    }

    public static GameType GetGameType()
    {
        return gameType;
    }

    public static void SetGameType(GameType gameType)
    {
        GameManager.gameType = gameType;

        if (gameType == GameType.singleplayer) playersCount = 1;
        else playersCount = 2;
    }

    public static void PlayerDied()
    {
        playersCount--;

        if (playersCount == 0) EndGame();
    }

    static void EndGame()
    {
        gameState = GameState.stopped;
        Time.timeScale = 0;
        Canvas gameOverCanvas = GameObject.Find("GameOverScreen").GetComponent<Canvas>();
        gameOverCanvas.enabled = true;
    }
}
