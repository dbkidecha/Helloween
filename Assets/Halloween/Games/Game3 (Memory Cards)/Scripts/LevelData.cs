using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public Sprite cover;
    public LData[] levelData;
}

[System.Serializable]
public class LData
{
    public int name;
    public Color color;
    public Sprite sprite;
}