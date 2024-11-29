using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public short stageNo;
    public StageType type;
    public StageTimeLine[] timeLine;
}
[System.Serializable]
public class StageTimeLine
{
    public MonsterData monsterData;
    public float time;
    public bool isLastEnemy;
}
public enum StageType
{
    Normal,
    Elite,
    Boss
}
public enum MonsterData
{
    Bike,
    Car,
    Truck,
    Elite,
    Boss
}