using UnityEngine;
using System.Collections;

public class PlayersManager : MonoBehaviour {

    public GameObject playerOne;
    public Transform firstSpawnPoint;
    public GameObject playerTwo;
    public Transform secondSpawnPoint;

    GameType gameType;

	void Start()
    {
        gameType = GameManager.GetGameType();
        
        spawnPlayers();
        GameManager.SetGameState(GameState.started);
	}


    void spawnPlayers()
    {
        playerOne = (GameObject) Instantiate(playerOne, firstSpawnPoint.position, firstSpawnPoint.rotation);

        switch (gameType)
        {
            case GameType.multiplayer:
                playerTwo = (GameObject)Instantiate(playerTwo, secondSpawnPoint.position, secondSpawnPoint.rotation);
                break;
            case GameType.singleplayer:
                Camera cameraOne = playerOne.GetComponentInChildren<Camera>();
                cameraOne.rect = new Rect(0, 0, 1, 1);
                break;
        }
    }
}
