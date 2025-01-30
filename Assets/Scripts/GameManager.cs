using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string logs;
    private IAdapter adapter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Оставляем объект между сценами
        }
        else
        {
            Destroy(gameObject);
        }

        adapter = new MockAdapter(); // Используем mock-адаптер для локального тестирования
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        adapter.InitializeGame();
    }

    public void EndTurn()
    {
        adapter.ProcessTurn();
    }

    public void RestartGame()
    {
        adapter.RestartGame();
    }

    public void UpdateLogs(string line)
    {
        logs += '\n' + line;
    }
}
