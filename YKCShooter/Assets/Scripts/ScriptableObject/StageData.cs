using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public StageType stagetype;

}
public enum StageType
{
    Normal,
    Elite,
    Boss
}
public enum MonsterType
{
    Bike,

}