using UnityEngine;


[RequireComponent(typeof(GameObject))]
public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject rectangle;

    // Sizes of rectangle's sprite
    private Vector2 spriteSizeMin;
    private Vector2 spriteSizeMax;

    private void Start()
    {
        CalculationRectangleSize();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnRectangle();
        }
    }


    private void CalculationRectangleSize()
    {
        // Get rectangle's corners coordinates
        spriteSizeMin = rectangle.GetComponent<SpriteRenderer>().sprite.bounds.min;
        spriteSizeMax = rectangle.GetComponent<SpriteRenderer>().sprite.bounds.max;

        // Transformation with scale of rectangle
        spriteSizeMin.x *= rectangle.transform.localScale.x;
        spriteSizeMin.y *= rectangle.transform.localScale.y;
        spriteSizeMax.x *= rectangle.transform.localScale.x;
        spriteSizeMax.y *= rectangle.transform.localScale.y;
    }


    public void SpawnRectangle()
    {
        // Getting, mouse position
        Vector2 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculation rectangle bottom left and under right corners
        Vector2 rectangleCornerA = spawnPos + spriteSizeMin;
        Vector2 rectangleCornerB = spawnPos + spriteSizeMax;

        // Checking by collider to check void space
        Collider2D collider = Physics2D.OverlapArea(rectangleCornerA, rectangleCornerB);
        if (collider)
        {
            Debug.Log("Spawn unreal");
        } else
        {
            Debug.Log("Spawn new rectangle");
            // Spawn prefab as rectangle
            GameObject currentRectangle = Instantiate(rectangle, spawnPos, Quaternion.identity);
            // Set random color
            currentRectangle.GetComponent<SpriteRenderer>().color = GetRandomColor();
        }
    }


    private Color32 GetRandomColor()
    {
        Color32 randomColor = new Color32(
            (byte)Random.Range(0, 255),    // R
            (byte)Random.Range(0, 255),    // G
            (byte)Random.Range(0, 255),    // B
            (byte)Random.Range(0, 255));   // A
        return randomColor;
    }


}
