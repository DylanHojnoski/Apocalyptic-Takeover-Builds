using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING}
    Vector2 position;
    [SerializeField] UnityEngine.Camera mainCam = null;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<Transform> enemy = new List<Transform>();
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        waveCountdown -= Time.deltaTime;
        if(state == SpawnState.WAITING)
        {          
                WaveCompleted();
        } 

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                // Start Spawning Wave
                StartCoroutine(SpawnWave(waves[nextWave]));
                waveCountdown = timeBetweenWaves;
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Dificulty(); 
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            int whichEnemy = UnityEngine.Random.Range(0, _wave.enemy.Count);
            SpawnEnemy(_wave.enemy[whichEnemy]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        //Spawn enemy
        randomSpawn(_enemy);
    }

    public void randomSpawn(Transform _enemy)
    {
        float halfHeight = mainCam.orthographicSize;
        float halfWidth = halfHeight * mainCam.aspect;

        float x1 = Random.Range(halfWidth + 1, halfWidth + 3);
        float y1 = Random.Range(-halfHeight, halfHeight);
        y1 = Random.Range(-2, 3);

        float x2 = Random.Range(-halfWidth - 1, -halfWidth - 3);

        float xRand = Random.Range(0, 11);

        if (xRand > 5)
        {
            position = new Vector2(x1, y1);
        }
        else
        {
            position = new Vector2(x2, y1);
        }
        Transform.Instantiate(_enemy, position, Quaternion.identity);
    }

    void Dificulty ()
    {
        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].count *= 2;
            waves[i].rate *= 1.25f;
        }
    }
}
