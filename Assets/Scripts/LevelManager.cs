using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public event Action LevelFinished;

    [SerializeField]
    private List<LevelDef> levelSettings;
    [SerializeField]
    private List<Material> skyboxes;
    [SerializeField]
    private List<Material> floorMaterials;
    
    [SerializeField]
    private int levelIndex;
    private int currentLevelProgress = 0;
    private bool levelComplete;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void OnEnable()
    {
        RenderSettings.skybox = skyboxes[GetCurrentLevelIndex() % skyboxes.Count];
    }

    public void Start()
    {
        ResetLevelProgress();
    }

    public void AddLevelProgress(int amount)
    {
        currentLevelProgress += amount;

        // TODO error ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
        if(currentLevelProgress >= levelSettings[levelIndex % levelSettings.Count].LevelGoalAmount && !levelComplete)
        {
            AddLevelIndex();
            LevelFinished.Invoke();
            levelComplete = true;
        }
    }

    public int GetEnemyAmount()
    {
        return levelSettings[levelIndex % levelSettings.Count].EnemyAmount;
    }
    
    public void ResetLevelProgress()
    {
        levelComplete = false;
        currentLevelProgress = 0;
    }

    public int GetCurrentLevelIndex()
    {
        return levelIndex;
    }

    private void AddLevelIndex()
    {
        levelIndex++;
    }

    public Material GetSkybox()
    {
        return skyboxes[levelIndex % skyboxes.Count];
    }
    
    public Material GetFloorMaterial()
    {
        return floorMaterials[levelIndex % floorMaterials.Count];
    }

    public int GetObstacleAmount()
    {
        return levelSettings[levelIndex % levelSettings.Count].ObstacleAmount;
    }
}
