using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelStartGame : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] StartGame startGame;

    public void OnStart()
    {
        Panel.SetActive(false);
        startGame.CreateGame();
    }
}
