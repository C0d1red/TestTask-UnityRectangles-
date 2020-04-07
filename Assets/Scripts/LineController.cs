using UnityEngine;
using System.Collections.Generic;
public class LineController : MonoBehaviour
{
    private LineRenderer line;
    private List<GameObject> rectangles;

    void Update()
    {
        DrawLine();
    }


    private void DrawLine()
    {
        if (rectangles[0] != null && rectangles[1] != null)
        {
            line.SetPosition(0, rectangles[0].transform.position);
            line.SetPosition(1, rectangles[1].transform.position);
        }
    }


    // Set rectangles to the new line
    public void SetRectangles(GameObject rect1, GameObject rect2)
    {
        rectangles = new List<GameObject>();
        rectangles.Add(rect1);
        rectangles.Add(rect2);
        line = GetComponent<LineRenderer>();
    }


    public List<GameObject> Rectangles
    {
        get
        {
            return rectangles;
        }
    }


    private void OnDestroy()
    {
        // Remove all links before destroy
        foreach(GameObject rect in rectangles)
        {
            if (rect != null)
            {
                rect.GetComponent<RectangleLink>().RemoveLine(gameObject);
            }
        }
        
    }
}
