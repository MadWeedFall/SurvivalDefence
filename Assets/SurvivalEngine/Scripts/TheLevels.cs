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

    // Update is called once per frame
    void Update()
    {
        PlayerData pdata = PlayerData.Get();
        if(pdata.day<=1)
        {
             wolfSpawn.Pause(false);
        }
        else if(pdata.day>1&&pdata.day<3)
        {
            beerSpawn.Pause(false);
        }
        else if(pdata.day>=3&&pdata.day<5)
        {
            boarSpawn1.Pause(false);
        }
        else
        {
            boarSpawn2.Pause(false);
        }
    }
}
