using UnityEngine;

public class PlayerGridMover : MonoBehaviour
{
    public float tileSize = 1.1f;
    public float moveSpeed = 18f;          // Higher = more responsive

    public int gridWidth = 8;
    public int gridHeight = 8;

    Vector3 targetPos;
    bool isMoving;

    // Input buffer
    Vector3 bufferedDirection = Vector3.zero;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        // Always read input (even while moving)
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) inputDir = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) inputDir = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) inputDir = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) inputDir = Vector3.right;

        if (inputDir != Vector3.zero)
        {
            bufferedDirection = inputDir;
        }

        // Handle movement
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                isMoving = false;
                CheckTileUnderPlayer();

                // Immediately use buffered input if there is any
                if (bufferedDirection != Vector3.zero)
                {
                    TryMove(bufferedDirection);
                    bufferedDirection = Vector3.zero;
                }
            }
            return;
        }

        // Not moving — try to use buffered or fresh input
        if (bufferedDirection != Vector3.zero)
        {
            TryMove(bufferedDirection);
            bufferedDirection = Vector3.zero;
        }
    }

    void TryMove(Vector3 direction)
    {
        Vector3 nextPos = transform.position + direction * tileSize;

        // Keep player inside the grid
        float maxX = (gridWidth - 1) * tileSize;
        float maxZ = (gridHeight - 1) * tileSize;

        nextPos.x = Mathf.Clamp(nextPos.x, 0f, maxX);
        nextPos.z = Mathf.Clamp(nextPos.z, 0f, maxZ);

        // Only move if the position actually changes
        if (Vector3.Distance(nextPos, transform.position) > 0.05f)
        {
            targetPos = nextPos;
            isMoving = true;
        }
    }

    void CheckTileUnderPlayer()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 2f))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
                tile.OnStepped();
        }
    }
}