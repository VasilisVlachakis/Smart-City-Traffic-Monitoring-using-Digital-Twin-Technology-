using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trafic.System
{
	public class WaitZone : MonoBehaviour 
	{
		

		public TrafficType type;
		public WaitZone opposite;

		
		
		private bool m_CanPass = false;

		public bool canPass 
		{ 
			get {return m_CanPass;} 
			set {m_CanPass = value;}
		}
	}
}