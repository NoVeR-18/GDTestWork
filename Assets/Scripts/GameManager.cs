using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Player;
    public List<Transform> Enemies;
    public GameObject Lose;
    public GameObject Win;
    public TextMeshProUGUI waves;

    private int currWave = 0;
    public LevelConfig Config;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWave();
    }

    public void RemoveEnemie()
    {
        if (Enemies.Count == 0)
        {

            if (currWave >= Config.Waves.Length)
            {
                Win.SetActive(true);
                return;
            }
            else
                StartCoroutine(SwitchWaves());
        }
    }
    IEnumerator SwitchWaves()
    {
        yield return new WaitForSeconds(5f);
        SpawnWave();
    }

    public void GameOver()
    {
        foreach (Transform enemie in Enemies)
        {
            if (enemie.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.Agent.speed = 0;
                enemyController.enabled = false;
            }
        }
        Lose.SetActive(true);
    }

    void SpawnWave()
    {
        waves.text = $"Waves: {currWave + 1} / {Config.Waves.Length}";
        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Enemies.Add(Instantiate(character, pos, Quaternion.identity).transform);
        }
        currWave++;
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
