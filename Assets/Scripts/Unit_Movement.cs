using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera myCam;
    private NavMeshAgent myAgent;
    public LayerMask grounded;
    private Vector3 unitGoingPosition = new Vector3();
    private Vector3 unitPositionsActual = new Vector3();
    private bool MoveBack = false;
    public bool Patrol = false;
    public float unitDistanceFormation;
    public int unitNumberInRow;

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.P))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            Patrol = true;

            if (Unit_Selection.Instance.unitSelectedList.Count > 1)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, grounded))
                {
                    unitPositionsActual = gameObject.transform.position;
                    int index = Unit_Selection.Instance.unitSelectedList.IndexOf(gameObject);
                    unitGoingPosition = new Vector3(hit.point.x + (unitDistanceFormation * (index % unitNumberInRow)), hit.point.y, hit.point.z - (unitDistanceFormation * (index / unitNumberInRow)));
                    myAgent.SetDestination(unitGoingPosition);
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, grounded))
                {
                    unitPositionsActual = gameObject.transform.position;
                    unitGoingPosition = hit.point;
                    myAgent.SetDestination(unitGoingPosition);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            Patrol = false;

            if (Unit_Selection.Instance.unitSelectedList.Count > 1)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, grounded))
                {
                    Vector3 tmpVector = new Vector3();
                    int index = Unit_Selection.Instance.unitSelectedList.IndexOf(gameObject);
                    tmpVector = new Vector3(hit.point.x + (unitDistanceFormation * (index % unitNumberInRow)), hit.point.y, hit.point.z - (unitDistanceFormation * (index / unitNumberInRow)));
                    myAgent.SetDestination(tmpVector);
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, grounded))
                {
                    myAgent.SetDestination(hit.point);
                }
            }
        }
        if (Patrol == true)
        {
            if (MoveBack == false)
            {
                if (gameObject.transform.position.x == unitGoingPosition.x && gameObject.transform.position.z == unitGoingPosition.z)
                {
                    MoveBack = true;
                    myAgent.SetDestination(unitPositionsActual);
                }
            }
            else
            {
                if (gameObject.transform.position.x == unitPositionsActual.x && gameObject.transform.position.z == unitPositionsActual.z)
                {
                    MoveBack = false;
                    myAgent.SetDestination(unitGoingPosition);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Water")
            GetComponent<NavMeshAgent>().speed = 3.5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Water")
            GetComponent<NavMeshAgent>().speed = 7f;
    }
}
