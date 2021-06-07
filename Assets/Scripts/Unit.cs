using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Unit_Selection.Instance.unitList.Add(this.gameObject);
    }
    private void OnDestroy()
    {
        Unit_Selection.Instance.unitList.Remove(this.gameObject);
    }
}
