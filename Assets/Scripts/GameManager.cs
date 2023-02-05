using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private GameObject crater;
    [SerializeField]
    private GameObject levelDoor;
    [SerializeField]
    private List<GameObject> rootlessMen;
    [SerializeField]
    private GameObject floor;
    [SerializeField]
    private List<GameObject> obstacles;
    
    public GameObject GetCrater()
    {
        return crater;
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.ResetLevelProgress();
        StartCoroutine(TrashOldLevelAndCreateNewLevel());
    }

    public GameObject GetObstacle()
    {
        int currentLevel = LevelManager.Instance.GetCurrentLevelIndex();
        if(currentLevel != 0 && (currentLevel + 1) % 3 == 0)
        {
            return obstacles[1];
        }

        return obstacles[0];
    }
    
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

    private void Start()
    {
        LevelManager.Instance.LevelFinished += OnLevelFinished;
        CreateLevel();
    }

    private void CreateLevel()
    {
        if(LevelManager.Instance.GetCurrentLevelIndex() == 0)
        {
            GameUIManager.Instance.FadeFromBlack();
        }
        else
        {
            GameUIManager.Instance.FadeFromWhite();
        }

        RenderSettings.skybox = LevelManager.Instance.GetSkybox();
        floor.GetComponent<Renderer>().material = LevelManager.Instance.GetFloorMaterial();
        
        for(int i = 0; i < LevelManager.Instance.GetEnemyAmount(); i++)
        {
            GameObject enemy = Instantiate(rootlessMen[Random.Range(0, rootlessMen.Count)], transform, true);
            enemy.GetComponent<BouncingEnemy>().AddBounceStrength();
            enemy.transform.localScale = Vector3.zero;
            enemy.transform.position = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
            enemy.transform.DOScale(Vector3.one, Random.Range(0.3f, 0.5f));
        }

        for(int i = 0; i < LevelManager.Instance.GetObstacleAmount(); i++)
        {
            GameObject rock = Instantiate(GetObstacle(), transform, true);
            rock.transform.position = new Vector3(Random.Range(-30, 30), -0.041f, Random.Range(-30, 30));
            float scale = Random.Range(5, 10);
            rock.transform.localScale = Vector3.one * 150 * scale;
            rock.transform.Rotate(new Vector3(0, Random.Range(0, 180), 0));
        }
    }

    private IEnumerator TrashOldLevelAndCreateNewLevel()
    {
        AudioManager.Instance.ResetAudioSources();

        float time = 0.5f;
        GameUIManager.Instance.FadeToWhite(time);
        yield return new WaitForSeconds(time + 0.2f);
        
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject toBeTrashed = transform.GetChild(i).gameObject;
            Destroy(toBeTrashed);
        }

        CreateLevel();
    }

    private void OnLevelFinished()
    {
        for(int i = 0; i < 3; i++)
        {
            SpawnDoor();
        }
    }

    private void SpawnDoor()
    {
        GameObject door = Instantiate(levelDoor, transform, true);
        door.transform.position = new Vector3(Random.Range(-10f,10f), 0.5f, Random.Range(-10f,10f));
        door.transform.localScale = Vector3.zero;
        door.transform.DOScale(Vector3.one, Random.Range(0.8f, 1.2f));
    }
}
