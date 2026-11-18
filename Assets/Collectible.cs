using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1;
    public float respawnDelay = 2.5f;   // seconds before it comes back

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);

            // Hide instead of destroying so we can reuse it
            gameObject.SetActive(false);

            // Tell the GameManager to respawn it later
            GameManager.Instance.RespawnCollectible(this, respawnDelay);
        }
    }

    public void RespawnAt(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}