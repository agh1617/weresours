using UnityEngine;
using System.Collections.Generic;

public enum GameState { started, paused, stopped }
public enum GameType { singleplayer, multiplayer }

public static class GameManager
{
    static GameState gameState = GameState.stopped;
    static GameType gameType = GameType.singleplayer;

    public static GameState GetGameState()
    {
        return gameState;
    }

    public static void SetGameState(GameState gameState)
    {
        GameManager.gameState = gameState;
    }

    public static GameType GetGameType()
    {
        return gameType;
    }

    public static void SetGameType(GameType gameType)
    {
        GameManager.gameType = gameType;
    }
}
