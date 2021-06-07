using System.Collections.Generic;
using UnityEngine;

public class Unit_Selection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelectedList = new List<GameObject>();

    private static Unit_Selection _Instance;
    public static Unit_Selection Instance { get { return _Instance; } }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
        }
    }

    public void ClickSelect(GameObject UnitToAdd)
    {
        DeselectAll();
        unitSelectedList.Add(UnitToAdd);
        UnitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        UnitToAdd.GetComponent<Unit_Movement>().enabled = true;
    }
    public void ShiftClickSelect(GameObject UnitToAdd)
    {
        if(!unitSelectedList.Contains(UnitToAdd))
        {
            ClickSelect(UnitToAdd);
        }
        else
        {
            DeselectUnit(UnitToAdd);
        }    
    }
    public void DragClickSelect(GameObject UnitToAdd)
    {
        if(!unitSelectedList.Contains(UnitToAdd))
        {
            unitSelectedList.Add(UnitToAdd);
            UnitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            UnitToAdd.GetComponent<Unit_Movement>().enabled = true;
        }
    }
    public void DeselectUnit(GameObject UnitToRemove)
    {
        if (UnitToRemove.GetComponent<Unit_Movement>().Patrol == false)
            UnitToRemove.GetComponent<Unit_Movement>().enabled = false;
        UnitToRemove.transform.GetChild(0).gameObject.SetActive(false);
        unitSelectedList.Remove(UnitToRemove);
    }
    public void DeselectAll()
    {
        foreach(var Unit in unitSelectedList)
        {
            if (Unit.GetComponent<Unit_Movement>().Patrol == false)
                Unit.GetComponent<Unit_Movement>().enabled = false;
            Unit.transform.GetChild(0).gameObject.SetActive(false);
        }
        unitSelectedList.Clear();
    }
}
