using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BoatingManager.instance.canPlayerMoveList.Count == 0)
        {
            IEnumerator UpdateIEnumerator()
            {
                yield return new WaitForSeconds(.1f);

                if (BoatingManager.instance.canPlayerMoveList.Count == 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
                    if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Ocean")))
                    {
                        agent.SetDestination(hit.point);
                    }
                }
            }
            
            StartCoroutine(UpdateIEnumerator());
        }
    }
}
