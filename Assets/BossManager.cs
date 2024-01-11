using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnBossInfo
{
    public BossNameType bossNameType;
    public BossInfoReader bossInfoReader;
}
public class BossManager : MonoBehaviour
{
    [SerializeField] private BossInfoReader bossInfo;

    [SerializeField] private List<SpawnBossInfo> spawnBossInfoList;

    [SerializeField] private BossData currentBossData = new BossData();

    [SerializeField] private BossCurrentStatusHUD currentStatusHUD;
    public List<Transform> spawnPointList = new List<Transform>();
    private ManagerRoot managerRoot => ManagerRoot.instance;
    private GameplayController gameplayController => GameplayController.instance;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        GetDataBoss();
        SpawnBoss();
        LoadBossInfo();
    }
    public void LoadBossInfo()
    {
        bossInfo.GetData(currentBossData, currentStatusHUD, gameplayController.PlayerInfoReader);
    }
    public void SpawnBoss()
    {
        BossNameType actionBossNameType = managerRoot.actionBossNameType;
        foreach(var spawnBossInfo in spawnBossInfoList)
        {
            if(spawnBossInfo.bossNameType == actionBossNameType)
            {
                bossInfo = Instantiate(spawnBossInfo.bossInfoReader, spawnPointList[0].position, Quaternion.identity);
                bossInfo.transform.SetParent(transform);
            }
        }
    }
    public void GetDataBoss()
    {
        BossNameType bossNameType = managerRoot.actionBossNameType;
        AvailableBossConfig availableBossConfig = managerRoot.ManagerRootConfig.availableBossConfig;

        BossPackedConfig bossPackedConfig = availableBossConfig.GetBossPackedConfigByNameType(bossNameType);
        
        BossData bossData = new BossData();
        bossData.bossConfig = bossPackedConfig.config;
        bossData.bossStats = bossPackedConfig.stats;

        bossData.lv = bossData.bossStats.lv;
        bossData.maxHealth = bossData.bossStats.maxHealth;
        bossData.health = bossData.bossStats.maxHealth;
        bossData.damage = bossData.bossStats.damage;
        bossData.moveSpeed = bossData.bossStats.moveSpeed;

        currentBossData = bossData;
    }
}
