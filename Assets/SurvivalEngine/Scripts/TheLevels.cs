using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurvivalEngine;

public class TheLevels : MonoBehaviour
{
    public EnemySpawn wolfSpawn;
    public EnemySpawn beerSpawn;
    
    public EnemySpawn boarSpawn1;
    public EnemySpawn boarSpawn2;



    // Start is called before the first frame update
    void Start()
    {
        wolfSpawn.Pause(true);
        beerSpawn.Pause(true);
        boarSpawn1.Pause(true);
        boarSpawn2.Pause(true);
    }

    public int GetTotalEnemyCount()
    {
        return wolfSpawn.GetEnemyCount()+beerSpawn.GetEnemyCount()+boarSpawn1.GetEnemyCount()+boarSpawn2.GetEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerData pdata = PlayerData.Get();
        if(pdata.day<=1)
        {
             wolfSpawn.Pause(false);
        }
        else if(pdata.day>1&&pdata.day<5)
        {
            wolfSpawn.Pause(false);
            beerSpawn.Pause(false);
        }
        else if(pdata.day>=5&&pdata.day<7)
        {
            wolfSpawn.Pause(false);
            beerSpawn.Pause(false);
            boarSpawn1.Pause(false);
        }
        else
        {
            wolfSpawn.Pause(false);
            beerSpawn.Pause(false);
            boarSpawn1.Pause(false);
            boarSpawn2.Pause(false);
        }
    }
}
