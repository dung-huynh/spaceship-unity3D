using UnityEngine;
using System.Collections;

public class AIPathFinding : MonoBehaviour {

	public Transform destination;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}

	void OnTriggerStay(Collider hit)
	{
		if(hit.transform.root.tag == "AI")
		{
			hit.transform.root.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination (destination.position);
			hit.transform.root.FindChild("Brain").GetComponent<AISystem>().getDestination(destination.position);
		}
	}
}
