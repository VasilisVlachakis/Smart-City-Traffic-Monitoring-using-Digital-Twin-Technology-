using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Trafic.System;


namespace Trafic
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class Vehicle : Agent 
	{
		private Road m_CurrentNavSection;
		private NavConnection m_CurrentOutConnection;

		[Header("Vehicle")]
		public Transform front;
     

       
        public virtual void Initialize(Road navSection, NavConnection destination)
		{
			m_CurrentNavSection = navSection;
            RegisterVehicle(m_CurrentNavSection, true);
			m_CurrentOutConnection = destination;
			agent.enabled = true;
			speed = TrafficSystem.Instance.GetAgentSpeedFromKPH(Mathf.Min(navSection.speedLimit, maxSpeed));
			agent.speed = speed;
			agent.destination = destination.transform.position;
		}

        public virtual void RegisterVehicle(Road section, bool isAdd)
        {
            section.RegisterVehicle(this, isAdd);
        }

		
		public override void Update()
		{
			if(agent.isOnNavMesh)
			{
				m_Blocked = CheckBlocked();
			}
            if (m_Blocked == true)
                agent.velocity = Vector3.zero;
            base.Update();

            if (agent.enabled == false)
            {
                Destroy(gameObject);
            }

            
            if (agent.hasPath)
            {

            }
            else
            {
                Destroy(gameObject);
               
            }


           
        }

		public override bool CheckStop()
		{
			if(m_Blocked || isWaiting)
				return true;
			return false;
		}

		// -------------------------------------------------------------------
		// Collisions

		public override void OnTriggerEnter(Collider col)
		{
			if(col.tag == "RoadConnection")
			{
				NavConnection connection = col.GetComponent<NavConnection>();
				if(connection.navSection != m_CurrentNavSection)
					SwitchRoad(connection);
               
                    

            }
			base.OnTriggerEnter(col);


        }
        
            
        

        // -------------------------------------------------------------------
        // Blocked

        private bool m_Blocked;

		private float m_BlockedDistance = 3;
		public float blockedDistance
		{
			get { return m_BlockedDistance; }
			set { m_BlockedDistance = value; }
		}

        [Tooltip("Empty gameobject from where the rays will be casted")]
        public Transform raycastAnchor;

        [Tooltip("Length of the casted rays")]
        public float raycastLength = 5;

        [Tooltip("Spacing between each rays")]
        public int raySpacing = 2;

        [Tooltip("Number of rays to be casted")]
        public int raysNumber = 6;

        private bool CheckBlocked()
		{


            float initRay = (raysNumber / 2f) * raySpacing;
            float hitDist = -1f;
            RaycastHit hit;
            for (float a = -initRay; a <= initRay; a += raySpacing)
            {
                Debug.DrawRay(raycastAnchor.transform.position, Quaternion.Euler(0, a, 0) * this.transform.forward * raycastLength, new Color(1, 0, 0, 0.5f));
                if (Physics.Raycast(raycastAnchor.transform.position, Quaternion.Euler(0, a, 0) * this.transform.forward, out hit, raycastLength))
                {
                    if (Vector3.Distance(raycastAnchor.transform.position, hit.point) < raycastLength)
                    {
                        if (hit.transform.tag == "Gib")
                            return true;
                    }
                }

                }

              
                    return false;
           
		}
       
        // -------------------------------------------------------------------
        // Switch Road

        private void SwitchRoad(NavConnection newConnection)
		{
			RegisterVehicle(m_CurrentNavSection, false);
			speed = TrafficSystem.Instance.GetAgentSpeedFromKPH(Mathf.Min(newConnection.navSection.speedLimit, maxSpeed));
			agent.speed = speed;
			m_CurrentNavSection = newConnection.navSection;
			RegisterVehicle(m_CurrentNavSection, true);
			m_CurrentOutConnection = newConnection.GetOutConnection();
			if(m_CurrentOutConnection != null)
				agent.destination = m_CurrentOutConnection.transform.position;

            
        }

		// -------------------------------------------------------------------
		// Debug

		public override void OnDrawGizmos()
		{
			if(TrafficSystem.Instance.drawGizmos)
			{
				Gizmos.color = CheckStop() ? Color.gray : Color.white;
				if(agent.hasPath)
				{	
					Gizmos.DrawWireSphere(agent.destination, 0.1f);
					for (int i = 0; i < agent.path.corners.Length - 1; i++)
						Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
				}
                else
                {
                    Destroy(gameObject);
                }

				Gizmos.color = m_Blocked ? Color.red : Color.green;
				Vector3 blockedRayEnd = front.TransformPoint(new Vector3(0, 0, m_BlockedDistance));
				Gizmos.DrawLine(front.position, blockedRayEnd);
                

                

            }
		}
	}
}

