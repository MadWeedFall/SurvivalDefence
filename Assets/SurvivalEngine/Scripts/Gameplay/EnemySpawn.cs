using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurvivalEngine;

public class EnemySpawn : MonoBehaviour
{
    List<GameObject> _enemyPrefabs;
    public int DesiredEnemyCount = 100;
    public float SpawnInterval = 3.0f;
    public GameObject EnemyPrefab;
    public PlayerCharacter PlayerCharacterInst;

    private float _spawnDuration = 0.0f;

    [SerializeField]
    private bool _pauseSpawn;

    public bool PauseSpawn
    {
        get => _pauseSpawn;
        set => _pauseSpawn = value;
    } 

    void Awake()
    {
        if(_enemyPrefabs==null) _enemyPrefabs = new List<GameObject>();
        _spawnDuration = 0.0f;
        _pauseSpawn = false;
        TheUI.Get().game_over_panel.onShow+= delegate
        {
            Clear();
        };
    }

    public void SpwanOneEnemy()
    {
        if(EnemyPrefab!=null&&_enemyPrefabs.Count<DesiredEnemyCount)
        {
            var enemyGo = Instantiate(EnemyPrefab);
            if(enemyGo!=null) 
            {
                enemyGo.transform.parent = transform;
                enemyGo.transform.position = transform.position;
                _enemyPrefabs.Add(enemyGo);
                var enemyDesctruct = enemyGo.transform.GetComponent<Destructible>();
                var enemyAnimalWild = enemyGo.transform.GetComponent<AnimalWild>();
                enemyAnimalWild.AttackTarget(PlayerCharacterInst);
                enemyDesctruct.onDeath = delegate{
                    _enemyPrefabs.Remove(enemyGo);
                };
                
                _spawnDuration=0;
            }

        }
    }

    public void Pause(bool flag=true)
    {
        _pauseSpawn = flag;
    }

    public int GetEnemyCount()
    {
        return _enemyPrefabs.Count;
    }

    public void Clear()
    {
        if(_enemyPrefabs.Count>0)
        {
            for(int i=0;i<_enemyPrefabs.Count;i++)
            {
                Destroy(_enemyPrefabs[i]);
            }
        }
        _enemyPrefabs.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(_pauseSpawn) return;
        if (TheGame.Get().IsPaused())
                return;

        if (PlayerCharacterInst!=null&&PlayerCharacterInst.IsDead())
                return;
        PlayerData pdata = PlayerData.Get();
        int time_hours = Mathf.FloorToInt(pdata.day_time);
        // if(time_hours<PlayerCharacterInst.WorkHourStart||time_hours>=PlayerCharacterInst.WorkHourEnd) return;

        _spawnDuration += Time.deltaTime;
        if(_spawnDuration>=SpawnInterval)
        {
            SpwanOneEnemy();
        }
    }
}
