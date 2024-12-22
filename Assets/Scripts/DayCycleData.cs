using UnityEngine;

[CreateAssetMenu(fileName = "DayCycleData", menuName = "ScriptableObjects/DayCycleData", order = 1)]
public class DayCycleData : ScriptableObject
{
    public float duration; // Duration of the cycle in seconds
    public Color ambientLightColor; // Ambient light color for this cycle
    public GameObject[] enemyPrefabs; // Prefabs of enemies that can spawn during this cycle
    public float[] spawnRates; // Spawn rates for each enemy type
}
