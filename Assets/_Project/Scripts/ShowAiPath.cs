using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShowAiPath : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private float elapsed;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.Log("No NavMeshAgent found");
            return;
        }
        StartCoroutine(Draw());
    }

    private IEnumerator Draw()
    {
        while (_navMeshAgent != null)
        {
            if (_navMeshAgent.hasPath)
            {
                Vector3[] corners = _navMeshAgent.path.corners;
                for (int i = 0; i < corners.Length - 1; i++)
                    Debug.DrawLine(corners[i], corners[i + 1], Color.red, 1f, false);
            }
            yield return new WaitForSeconds(1f); //draw every 1s;
        }
    }
}
