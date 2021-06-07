using UnityEngine;

public class UnitClick : MonoBehaviour
{

    private Camera myCam;
    public GameObject ground_Marker;

    public LayerMask clickable;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Unit_Selection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    Unit_Selection.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Unit_Selection.Instance.DeselectAll();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                ground_Marker.transform.position = hit.point;
                ground_Marker.SetActive(true);
                //ground_Marker.SetActive(false);
            }
        }
    }
}
