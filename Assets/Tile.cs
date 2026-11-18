using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer rend;
    Color originalColor;
    public Color steppedColor = new Color(0.3f, 0.8f, 0.4f); // greenish

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void OnStepped()
    {
        rend.material.color = steppedColor;
    }
}