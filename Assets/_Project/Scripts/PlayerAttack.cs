using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        if (go == null)
        {
            Debug.Log("No MainCamera tag found");
            return;
        }
        _camera = go.GetComponent<Camera>();
    }

 
    public void DoAttack()
    {

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("DoAttack");
        }
    }
}
