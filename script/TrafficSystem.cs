using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trafic.System
{
	public enum TrafficType { Vehicle }

	public class TrafficSystem : MonoBehaviour 
	{

        public int kph = 50;
        private static TrafficSystem _Instance;
		public static TrafficSystem Instance
		{
			get
			{
				if(_Instance == null)
					_Instance = FindObjectOfType<TrafficSystem>();
				return _Instance;
			}
		}

        [SerializeField]
        public bool drawGizmos = false;
		
		
			
		
		


		

		private void Start () 
		{
			
			

			
		}

            

       

		

		

		
		public float GetAgentSpeedFromKPH(int kph)
		{
			return kph * .05f;
		}
	}
}

