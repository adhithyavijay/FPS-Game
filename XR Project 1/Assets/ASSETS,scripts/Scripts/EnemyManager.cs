using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public Transform[] respawnPoints;
    public float respawnTime = 3f;

    public void RespawnEnemy(GameObject enemy)
    {
        if (enemy == null) return;
        StartCoroutine(RespawnCoroutine(enemy));
    }

    IEnumerator RespawnCoroutine(GameObject enemy)
    {
        yield return new WaitForSeconds(respawnTime);

        if (respawnPoints.Length == 0)
        {
            Debug.LogError("❌ No respawn points assigned in EnemyManager!");
            yield break;
        }

        Transform spawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
            enemyAI.ResetHealth();
        else
            Debug.LogError("❌ EnemyAI script is missing on the respawned enemy!");

        enemy.SetActive(true);
    }
}
