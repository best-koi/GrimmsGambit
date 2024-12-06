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

    [SerializeField]
    private Color spawnedColor;//A color for the enemy spawned 

    private EnemyTemplate enemy;//The enemy spawned at this location

    [SerializeField]
    private float offset;//The offset between enemy and spawner

    [SerializeField]
    private Renderer renderer;//The enemy spawner's color

    private GameObject m_SpawnedEnemy; // Reference to spawned enemy

    // Start is called before the first frame update
    void Start()
    {
        renderer.material.color = spawnerColor;
        enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)]);
        enemy.SetColor(spawnedColor);
        enemy.transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        m_SpawnedEnemy = enemy.gameObject;
    }
    void Update(){
        if(enemy == null)
         Destroy(gameObject);
    }


    public int GetEnemyOrder()
    {
        return enemyOrder;
    }

    public EnemyTemplate GetEnemy()
    {
        return enemy;
    }

    public Color GetColor()
    {
        return spawnerColor;

    }

    public GameObject GetSpawnedEnemy()
    {
        return m_SpawnedEnemy;
    }
}
