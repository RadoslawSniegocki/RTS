using UnityEngine;

public class Unit_Drag : MonoBehaviour
{
    private Camera myCam;

    [SerializeField]
    private RectTransform boxVisual;

    private Rect selection_Box;

    Vector2 startPosition;
    Vector2 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selection_Box = new Rect();
        }
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }
    void DrawSelection()
    {
        if(Input.mousePosition.x < startPosition.x)
        {
            selection_Box.xMin = Input.mousePosition.x;
            selection_Box.xMax = startPosition.x;
        }
        else
        {
            selection_Box.xMin = startPosition.x;
            selection_Box.xMax = Input.mousePosition.x;
        }
        if (Input.mousePosition.y < startPosition.y)
        {
            selection_Box.yMin = Input.mousePosition.y;
            selection_Box.yMax = startPosition.y;
        }
        else
        {
            selection_Box.yMin = startPosition.y;
            selection_Box.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        foreach(var Unit in Unit_Selection.Instance.unitList)
        {
            if(selection_Box.Contains(myCam.WorldToScreenPoint(Unit.transform.position)))
            {
                Unit_Selection.Instance.DragClickSelect(Unit);
            }
        }
    }
}
