using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}
public class Manager : Loader<Manager>
{
    [SerializeField]
    private readonly int totalWaves = 10;

    [SerializeField]
    private readonly Text totalMoneyLabel;

    [SerializeField]
    private readonly Text currentWave;

    [SerializeField]
    private readonly Text totalEscapedLabel;

    [SerializeField]
    private readonly Text playBtnLabel;

    [SerializeField]
    private readonly Button playBtn;

    [SerializeField]
    private readonly GameObject spawnPoint;

    [SerializeField]
    private readonly Enemy[] enemies;

    [SerializeField]
    private int totalEnemies = 5;

    [SerializeField]
    private readonly int enemiesPerSpawn;
    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;
    private AudioSource audioSource;

    public List<Enemy> EnemyList = new List<Enemy>();

    private const float spawnDelay = 1f;  //М: Отвечает за перерыв между спаунами противников в секундах

    public int TotalEscaped
    {
        get => totalEscaped;
        set => totalEscaped = value;
    }

    public int RoundEscaped
    {
        get => roundEscaped;
        set => roundEscaped = value;
    }

    public int TotalKilled
    {
        get => totalKilled;
        set => totalKilled = value;
    }

    public int TotalMoney
    {
        get => totalMoney;
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = TotalMoney.ToString();
        }
    }

    public AudioSource AudioSource => audioSource;

    private IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (var i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    var newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        EnemyList.Remove(enemy);
    }

    public void DestroyEnemies()
    {
        foreach (var enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        if (TotalMoney < amount)
        {
            TotalMoney = 0;
        }
        else
        {
            TotalMoney -= amount;
        }
    }

    public void IsWaveOver()
    {
        totalEscapedLabel.text = "Escaped " + TotalEscaped + " / 10";

        if ((RoundEscaped + TotalKilled) == totalEnemies)
        {
            if (waveNumber <= enemies.Length)
            {
                enemiesToSpawn = waveNumber;
            }

            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if (totalEscaped >= 10)
        {
            currentState = gameStatus.gameover;
        }
        else if (waveNumber == 0 && (RoundEscaped + TotalKilled) == 0)
        {
            currentState = gameStatus.play;
        }
        else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
    }

    public void PlayButtonPressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;

            default:
                totalEnemies = 5;
                TotalEscaped = 0;
                TotalMoney = 10;
                enemiesToSpawn = 0;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagBuildSite();
                totalMoneyLabel.text = TotalMoney.ToString();
                totalEscapedLabel.text = "Escaped " + TotalEscaped + "/ 10";
                audioSource.PlayOneShot(SoundManager.Instance.Newgame);
                break;

        }

        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWave.text = "Wave" + (waveNumber + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                playBtnLabel.text = "Play again!";
                AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
                break;

            case gameStatus.next:
                playBtnLabel.text = "Next wave";

                break;

            case gameStatus.play:
                playBtnLabel.text = "Play game";

                break;

            case gameStatus.win:
                playBtnLabel.text = "Play game";

                break;

        }

        playBtn.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDrag();
            TowerManager.Instance.towerButtonPressed = null;

        }
    }
}
