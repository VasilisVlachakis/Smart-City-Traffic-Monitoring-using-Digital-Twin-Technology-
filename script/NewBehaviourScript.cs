using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trafic.System;
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    Road[] rod1;
    Junction[] junc1;
    public DataBaseRoads rodss;
    public DataBaseCross1 cross;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        rodss = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBaseRoads>();
        cross = GameObject.FindGameObjectWithTag("DataBaseCross").GetComponent<DataBaseCross1>();

        btn.onClick.AddListener(SavetoBase);
    }

    public void SavetoBase()
    {
        rod1 = FindObjectsOfType<Road>();
        foreach (var roadss in rod1)
        {
            if (roadss.car > 0)
            {

                rodss.SaveScoreToDataBase(roadss.ID, roadss.car);
            }
        }


        junc1 = FindObjectsOfType<Junction>();
        foreach (var crosses1 in junc1)
        {
            if (crosses1.car == 0)
            {
                if (crosses1.ID != "topali me spiridi" && crosses1.ID != "28h Oktobriou me spiridi" && crosses1.ID != "taki ikonomaki me spiridi")
                    cross.SaveCrossToDataBase(crosses1.ID, crosses1.phaseIntervalNorth, crosses1.phaseIntervalWest);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
