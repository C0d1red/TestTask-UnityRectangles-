using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GameObject))]
public class RectangleLink : MonoBehaviour
{
    [SerializeField]
    //Prefab of line
    private GameObject lineObj;

    //List with all lines linked of this rectangle
    private List<GameObject> lines;


    void Start()
    {
        lines = new List<GameObject>();
    }


    public void SwitchLink(GameObject linkedRect)
    {
        bool isSameLine = false;

        for (int i = 0; i < lines.Count; i++)
        {
            // If one of the line haven't current gameobjects
            // they are destroying and removing from the lists
            // of course, isSameLine = true 
            if (lines[i].GetComponent<LineController>().Rectangles.Contains(gameObject) && lines[i].GetComponent<LineController>().Rectangles.Contains(linkedRect))
            {
                GameObject temp = lines[i];
                lines.Remove(lines[i]);
                Destroy(temp);
                linkedRect.GetComponent<RectangleLink>().RemoveLine(temp);
                isSameLine = true;
                break;
            }
        }

        // If gameobject and linkedRect haven't link, they will be link 
        if (!isSameLine)
        {
            GameObject tempLine = Instantiate(lineObj, transform.parent);
            tempLine.GetComponent<LineController>().SetRectangles(gameObject, linkedRect);
            lines.Add(tempLine);
            linkedRect.GetComponent<RectangleLink>().AddLine(tempLine);
        }
    }


    public void AddLine(GameObject line)
    {
        lines.Add(line);
    }


    public void RemoveLine(GameObject line)
    {
        if (lines.Contains(line))
        {
            lines.Remove(line);
        }
    }


    public void DestroyAllLines()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i]);
        }
    }

}
