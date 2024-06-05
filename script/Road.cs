using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trafic.System
{

    
    public class Road : MonoBehaviour
    {
        [Header("Nav Section")]
        public VehicleSpawn[] vehicleSpawns;
        public NavConnection[] connections;
        public int speedLimit = 20;
      

        public GameObject vehiclePrefab;


        public bool spawnOnStart = true;
        public int car = 0;
        int x = 0;

        public string ID;

        bool databaseon = true;

        private List<Road> m_Roads = new List<Road>();


        public ToggleCrossRoads tog;

        public Dropdown dropdown;
        public Dropdown dropdown1;
        int a;
        string b;

        public Button btn;
        public DataBaseRoads ro;
        public virtual void Start()
        {
           
            btn.onClick.AddListener(getCar);

            

            foreach (NavConnection connection in connections)
                connection.navSection = this;
            Road[] roadsFound = FindObjectsOfType<Road>();
            foreach (Road r in roadsFound)
                m_Roads.Add(r);



            ro = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBaseRoads>();
            
            
        }

      

       public async void GetScoresFromDataBase2()
        {
            var tas = ro.GetScoresFromDataBase();
             var result = await tas;

            foreach(var lightco in result)
               {
                if (lightco.ID == ID)
                  {
                    car = lightco.car;

              }
             }
        }
        public void getCar()
        {
            dropdown = FindObjectOfType<Dropdown>();
            a = dropdown.car1;

            dropdown1 = FindObjectOfType<Dropdown>();
            b = dropdown1.ID1;

             
            if (b == ID)
            {

                

                    car = a;

              


            }
        }

        

        public bool TryGetVehicleSpawn(out VehicleSpawn spawn)
        {
            if (m_CurrentVehicles.Count == 0 && vehicleSpawns.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, vehicleSpawns.Length);
                spawn = vehicleSpawns[index];
                return true;
            }
            spawn = null;
            return false;
        }

        private float time = 0.0f;
        public float interpolationPeriod = 1.2f;
     
        private void Update()
        {
            tog = FindObjectOfType<ToggleCrossRoads>();
            databaseon = tog.Databaseonandoffs;

            time += Time.deltaTime;

            if (time >= interpolationPeriod)
            {
                time = 0.0f;
                if (databaseon == true)
                {
                    GetScoresFromDataBase2();
                }
            }

            if (x < car)
                {


                    int index = UnityEngine.Random.Range(0, vehicleSpawns.Length);

                    VehicleSpawn spawn;
                    spawn = vehicleSpawns[index];
                    int index1 = UnityEngine.Random.Range(0, m_Roads.Count);
                    Road road = m_Roads[index1];

                    Vehicle newVehicle = Instantiate(vehiclePrefab, spawn.spawn.position, spawn.spawn.rotation).GetComponent<Vehicle>();
                    newVehicle.Initialize(road, spawn.destination);
                    x++;



                }
                if (x > car)
                {
                    Destroy(GameObject.FindWithTag("Gib"));


                    x--;
                }
            

            }

       

        
        private List<Vehicle> m_CurrentVehicles = new List<Vehicle>();

        public void RegisterVehicle(Vehicle input, bool isAdd)
        {
            if (isAdd)
                m_CurrentVehicles.Add(input);
            else
            {
                if (m_CurrentVehicles.Contains(input))
                    m_CurrentVehicles.Remove(input);
               
            }
        }


        
    }

    [Serializable]
    public class VehicleSpawn
    {
        public Transform spawn;
        public NavConnection destination;
    }
}

public class HighScore
{
    public string ID { get; set; }
    public int car { get; set; }

}

