using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Countdown, Wave, Rest }
    public GameState CurrentState {  get; private set; }

    public float countdownTime = 10f;
    public float restTime = 15f;

    public GameObject[] enemiesToSpawn;
    public Transform[] spawnPoints;

    public TMP_Text statusText;

    private int currentWave = 0;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        // Initial Countdown
        yield return StartCoroutine(CountdownPhase());

        while (true)
        {
            yield return StartCoroutine(WavePhase());
            yield return StartCoroutine(RestPhase());
        }
    }

    IEnumerator CountdownPhase()
    {
        CurrentState = GameState.Countdown;

        // Updating the game status text with countdown
        float timer = countdownTime;
        while (timer > 0)
        {
            UpdateStatusText($"Prepare for combat: {Mathf.CeilToInt(timer)}");
            timer -= Time.deltaTime;
            yield return null;
        }

        UpdateStatusText("Fight!");

    }

    IEnumerator WavePhase()
    {
        CurrentState = GameState.Wave;
        currentWave++;

        SpawnEnemies(currentWave);

        // Wait until all enemies are gone
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }
    }

    void SpawnEnemies(int wave)
    {
        // Increase difficulty each wave
        int enemyCount = wave + 2;
        for (int i = 0; i < enemyCount; i++)
        {
            var enemyPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator RestPhase()
    {
        CurrentState = GameState.Rest;

        // Updating the game status text with countdown
        float timer = restTime;
        while (timer > 0)
        {
            UpdateStatusText($"Prepare for combat: {Mathf.CeilToInt(timer)}");
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    void UpdateStatusText(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}
