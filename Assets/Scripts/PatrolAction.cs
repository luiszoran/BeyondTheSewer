using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : StateAction
{
	public override void Act(LizardController controller)
	{
		Patrol (controller);
	}

	private void Patrol(LizardController controller)
	{
		controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
		controller.navMeshAgent.Resume ();

		if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) 
		{
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Length;
		}
	}
}