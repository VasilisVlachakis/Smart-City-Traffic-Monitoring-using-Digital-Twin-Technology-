using UnityEngine;
using System.Collections.Generic;
using System;

namespace Trafic.System
{
    [RequireComponent(typeof(Collider))]
	public class NavConnection : MonoBehaviour 
	{
		private Road m_NavSection;
		public Road navSection 
		{
			get { return m_NavSection; }
			set { m_NavSection = value; }
		}

        public string ID ;

       // private DataBase d;
       
        public NavConnection[] outConnections;

	    public int Left=30;
        public int Right=30;
        public int Straight=40;


        public DataBasePith pi;

        public virtual void Start()
        {
            pi = GameObject.FindGameObjectWithTag("DataBasePith").GetComponent<DataBasePith>();

            if (outConnections.Length > 2)
            {

             

                InvokeRepeating("GetPithanothtes", 2f, 3f);


            }

        }

        private async void GetPithanothtes()
        {
           
            var task = pi.GetPithanothtesFromDataBase();
            var result = await task;

            foreach (var lightco in result)
            {
                if (lightco.ID2 == ID)
                {
                    Left = lightco.left2;
                    Right= lightco.right2;
                    Straight= lightco.straight2;

                }
            }

        }
              
        public void Update()
        {
           
        }


        public NavConnection GetOutConnection()
		{


            if (outConnections.Length == 0)
            {
                return null;
                
               
            }
            else if (outConnections.Length == 1)
            {
                return outConnections[0];
            
            }

            else if (outConnections.Length == 2)
            {
                int a = UnityEngine.Random.Range(0, 99);
                if (a < Left)
                {
                    return outConnections[1];
                }
                
                else
                {
                    return outConnections[0];
                }
            }
            else 
            {
                int a = UnityEngine.Random.Range(0, 99);
                if (a < Left)
                {
                    return outConnections[0];
                }
                else if (a > Left && a < Right + Left)
                {
                    return outConnections[1];
                }
                else
                {
                    return outConnections[2];
                }
            }

        }

		private void OnDrawGizmos()
		{
			if(TrafficSystem.Instance.drawGizmos)
			{
				Gizmos.color = Color.white;
				Gizmos.DrawSphere(transform.position - new Vector3(0, transform.localScale.y * 0.5f, 0), 0.05f);
			}
		}
	}
}

public class PithanotitesScore1
{
    public string ID2 { get; set; }
    public int left2 { get; set; }
    public int right2 { get; set; }
    public int straight2 { get; set; }

}

