using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject collectiblePrefab;
    public int width = 8;
    public int height = 8;
    public float tileSize = 1.1f;
    public int collectibleCount = 5;

    void Start()
    {
        // Spawn tiles
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }

        // Spawn collectibles on random tiles
        for (int i = 0; i < collectibleCount; i++)
        {
            int x = Random.Range(0, width);
            int z = Random.Range(0, height);
            Vector3 pos = new Vector3(x * tileSize, 0.6f, z * tileSize);
            Instantiate(collectiblePrefab, pos, Quaternion.identity);
        }
    }
}