using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTemplate> enemiesToSpawn;//A list of enemies to randomly spawn

    [SerializeField]
    private int enemyOrder;//To be used to determine what enemy attacks first

    [SerializeField]
    private Color spawnerColor;//The color of the spawner for Intent

    private EnemyTemplate enemy;//The enemy spawned at this location

    [SerializeField]
    private float offset;//The offset between enemy and spawner

    [SerializeField]
    private Renderer renderer;//The enemy spawner's color

    // Start is called before the first frame update
    void Start()
    {
        renderer.material.color = spawnerColor;
        enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)]);
        enemy.transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }

    public int GetEnemyOrder()
    {
        return enemyOrder;
    }

    public Color GetColor()
    {
        return spawnerColor;

    }
}