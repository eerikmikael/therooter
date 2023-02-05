using UnityEngine;

[CreateAssetMenu(fileName = "LevelDef", menuName = "ScriptableObjects/LevelDef", order = 1)]
public class LevelDef : ScriptableObject
{
    public string LevelName;
    public int EnemyAmount;
    public int LevelGoalAmount;
    public int ObstacleAmount;
}
