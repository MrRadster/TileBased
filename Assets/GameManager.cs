using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timeLimit = 30f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;

    [Header("Respawn Settings")]
    public float tileSize = 1.1f;
    public int gridWidth = 8;
    public int gridHeight = 8;

    int score;
    float timer;
    public bool IsGameOver { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = timeLimit;
        IsGameOver = false;
        if (gameOverText) gameOverText.gameObject.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (IsGameOver) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            GameOver();
        }
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        score += amount;
        UpdateUI();
    }

    public void RespawnCollectible(Collectible collectible, float delay)
    {
        StartCoroutine(RespawnCoroutine(collectible, delay));
    }

    IEnumerator RespawnCoroutine(Collectible collectible, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (IsGameOver) yield break;

        // Pick a random tile position
        int x = Random.Range(0, gridWidth);
        int z = Random.Range(0, gridHeight);
        Vector3 pos = new Vector3(x * tileSize, 0.6f, z * tileSize);

        collectible.RespawnAt(pos);
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (timerText) timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
    }

    void GameOver()
    {
        IsGameOver = true;
        if (gameOverText)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Time's Up!\nFinal Score: " + score;
        }
    }
}