using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trafic.System

{
	public class Junction : Road 
	{	
		
		public enum PhaseType { Timed }

		
		[Header("Junction")]
		public PhaseType type = PhaseType.Timed;
		public Phase[] phases;
		
		
        public int phaseIntervalNorth = 2;
        public int phaseIntervalWest = 10;

        bool toggle = true;

        int test, test2;


       public DataBaseCross1 li;

    

        public override void Start()
        {
            test = phaseIntervalWest;
            test2 = phaseIntervalNorth;

            btn.onClick.AddListener(getPhase);
            base.Start();
            if (phases.Length > 0)
                phases[0].Enable();

            li = GameObject.FindGameObjectWithTag("DataBaseCross").GetComponent<DataBaseCross1>();
            

           
        }




        private async void GetCrossFromDataBase2()
        {
            var tas = li.GetCrossFromDataBase();
            var result = await tas;

            foreach(var lightco in result)
            {
                if (lightco.ID == ID)
                {
                    phaseIntervalNorth = lightco.phaseIntervalShort1;
                    phaseIntervalWest = lightco.phaseIntervalLong1;

                }
            }


        }
       

        

        public Dropdownfanaria dropdown2;
        public Dropdownfanaria dropdown3;
        public Dropdownfanaria dropdown4;
        int ph;
        string ide;
        int ph2;


        private float time2 = 0.0f;
        public float interpolationPeriod2 = 1.2f;

        public ToggleCrossRoads togs;
        private void Update()
		{
            time2 += Time.deltaTime;

            if (time2 >= interpolationPeriod2)
            {
                togs = FindObjectOfType<ToggleCrossRoads>();
                toggle = togs.Databaseonandoffs;
                if (toggle == true)
                {
                    time2 = 0.0f;
                    GetCrossFromDataBase2();
                }
            }

            if (ide == ID) {
                if (test != phaseIntervalWest || test2 != phaseIntervalNorth)
                {
                    test = phaseIntervalWest;
                    test2 = phaseIntervalNorth;

                }
            }
            if (type == PhaseType.Timed)
			{
                if (m_CurrentPhase==0) {
                    m_PhaseTimer += Time.deltaTime;
                    if (!m_PhaseEnded && m_PhaseTimer > (phaseIntervalNorth  ))
                        EndPhase();
                    if (m_PhaseTimer > phaseIntervalNorth)
                        ChangePhase();
                }
                if (m_CurrentPhase == 1)
                {
                    m_PhaseTimer += Time.deltaTime;
                    if (!m_PhaseEnded && m_PhaseTimer > (phaseIntervalWest ) )
                        EndPhase();
                    if (m_PhaseTimer > phaseIntervalWest)
                        ChangePhase();
                }
            }
		}

        public void getPhase()
        {
            dropdown2 = FindObjectOfType<Dropdownfanaria>();
            ph = dropdown2.time;

            dropdown4 = FindObjectOfType<Dropdownfanaria>();
            ph2 = dropdown4.time2;


            dropdown3 = FindObjectOfType<Dropdownfanaria>();
            ide = dropdown3.ID1;
            toggle = false;
            if (ide == ID)
            {

                phaseIntervalWest = ph;
                phaseIntervalNorth = ph2;
                

            }
        }
		float m_PhaseTimer;
		bool m_PhaseEnded;
		private int m_CurrentPhase;

		private void EndPhase()
		{
			m_PhaseEnded = true;
			phases[m_CurrentPhase].End();
		}

		public void ChangePhase()
		{
			m_PhaseTimer = 0;
			m_PhaseEnded = false;
			if(m_CurrentPhase < phases.Length - 1)
				m_CurrentPhase++;
			else
				m_CurrentPhase = 0;
			phases[m_CurrentPhase].Enable();
		}

		private Mesh m_Cube;
		public Mesh cube
		{
			get
			{
				if(m_Cube == null)
				{
					GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
					m_Cube = gameObject.GetComponent<MeshFilter>().sharedMesh;
					GameObject.DestroyImmediate(gameObject);
				}
				return m_Cube;
			}
		}

		private void OnDrawGizmos()
		{
			if(TrafficSystem.Instance.drawGizmos)
			{
				Phase phase = phases[m_CurrentPhase];
				foreach(WaitZone zone in phase.positiveZones)
				{	
					Gizmos.color = zone.canPass ? Color.green : Color.red;
					DrawAreaGizmo(zone.transform);

                }
				Gizmos.color = Color.red;
				foreach(WaitZone zone in phase.negativeZones)
					DrawAreaGizmo(zone.transform);
			}
		}

		private void DrawAreaGizmo(Transform t)
		{
			Matrix4x4 rotationMatrix1 = Matrix4x4.TRS(t.position - new Vector3(0, t.localScale.y * 0.5f, 0), t.rotation, Vector3.Scale(t.lossyScale, new Vector3(1f, 0.1f, 1f)));
			Gizmos.matrix = rotationMatrix1;
			Gizmos.DrawWireMesh(cube, Vector3.zero, Quaternion.identity);
		}

		[Serializable]
		public class Phase
		{
			public WaitZone[] positiveZones;
			public WaitZone[] negativeZones;
			public TrafficLight[] positiveLights;
			public TrafficLight[] negativeLights;

			public void Enable()
			{
				foreach(WaitZone zone in positiveZones)
					zone.canPass = true;
				foreach(TrafficLight light in positiveLights)
					light.SetLight(true);
				foreach(WaitZone zone in negativeZones)
					zone.canPass = false;
				foreach(TrafficLight light in negativeLights)
					light.SetLight(false);
			}

			public void End()
			{
				foreach(WaitZone zone in positiveZones)
					zone.canPass = false;
			}
		}
	}
}

public class LightScore
{
    public string ID { get; set; }
    public int phaseIntervalLong1 { get; set; }
    public int phaseIntervalShort1 { get; set; }

}