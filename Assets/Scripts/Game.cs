using Core.Common;
using Core.Services;
using UnityEngine;

public class Game : MonoBehaviour, IGame
{
    [SerializeField] private GameplayStartup _startup;
    
    public void StartGame()
    {
        _startup.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        
    }

    public void StopGame()
    {
        _startup.gameObject.SetActive(false);
    }
}