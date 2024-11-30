using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public short stageNo;
    public StageType type;
    public StageTimeline[] timeline;
}
[System.Serializable]
public class StageTimeline
{
    public EnemyType enemyType;
    public float time;
    public bool isLastEnemy;
}
public enum StageType : byte
{
    Normal = 0,
    Elite = 1,
    Boss = 2
}
public enum EnemyType : byte
{
    Bike = 0,
    Car = 1,
    Truck = 2,
    Elite = 3,
    Boss = 4
}