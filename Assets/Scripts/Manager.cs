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
    private int totalWaves= 10;

    [SerializeField]
    private Text totalMoneyLabel;

    [SerializeField]
    private Text currentWave;

    [SerializeField]
    private Text totalEscapedLabel;

    [SerializeField]
    private Text playBtnLabel;

    [SerializeField]
    private Button playBtn;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private Enemy[] enemies;

    [SerializeField]
    private int totalEnemies = 5;

    [SerializeField]
    private int enemiesPerSpawn;

    int waveNumber = 0;
    int totalMoney = 10;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled = 0;
    int whichEnemiesToSpawn = 0;
    int enemiesToSpawn = 0;
    gameStatus currentState = gameStatus.play;
    AudioSource audioSource;

    public List<Enemy> EnemyList = new List<Enemy>();

    private const float spawnDelay = 1f;  //М: Отвечает за перерыв между спаунами противников в секундах

    public int TotalEscaped
    {
        get { return totalEscaped; }
        set { totalEscaped = value; }
    }

    public int RoundEscaped
    {
        get { return roundEscaped; }
        set { roundEscaped = value; }
    }

    public int TotalKilled 
    {
        get { return totalKilled; }
        set { totalKilled = value; }
    }
    
    public int TotalMoney
    {
        get 
        { 
            return totalMoney; 
        }
        set 
        { 
            totalMoney = value; 
            totalMoneyLabel.text = TotalMoney.ToString();   
        }
    }

    public AudioSource AudioSource
    {
        get { return audioSource; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy;  //М: тут можно задавать, какие противники будут спауниться на карту
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
        foreach (Enemy enemy in EnemyList)
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
        if (TotalMoney < amount){
            TotalMoney = 0;
        }
        else{
            TotalMoney -= amount;
        }
    }

    public void IsWaveOver()
    {
        totalEscapedLabel.text= "Escaped " + TotalEscaped + " / 10";

        if ((RoundEscaped + TotalKilled)== totalEnemies)
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
        else if (waveNumber ==0 && (RoundEscaped + TotalKilled) ==0 )
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
        currentWave.text = "Wave" + (waveNumber +1);
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
