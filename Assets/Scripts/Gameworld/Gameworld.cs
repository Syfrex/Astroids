using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Gameworld : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AssetReferenceGameObject bulletAssetReference;
    [SerializeField] private Player player;
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Enemy")]
    [SerializeField] private AssetReferenceGameObject enemyAssetReference;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private int enemySpeed;
    [Header("UI")]
    [SerializeField] private ScoreText scoreText;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private Image[] playerLifeImages;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Transform waveCountdownTransform;
    [SerializeField] private WaveCountdown waveCountdown;

    void Start()
    {
        GameObject bulletPool = Instantiate(new GameObject());
        bulletPool.AddComponent<PoolManager>();
        bulletPool.GetComponent<PoolManager>().Init(bulletAssetReference);
        GameObject enemyPool = Instantiate(new GameObject());
        enemyPool.AddComponent<PoolManager>();
        enemyPool.GetComponent<PoolManager>().Init(enemyAssetReference);
        player.Init(bulletPool.GetComponent<PoolManager>(), playerStats);
        enemyManager.Init(enemyPool.GetComponent<PoolManager>(), amountOfEnemies, enemySpeed);
        scoreText.Init(scoreText.GetComponent<TextMeshProUGUI>());
        playerLife.Init(playerLifeImages);
        waveCountdown.Init(countdownText, waveCountdownTransform);
    }
}
