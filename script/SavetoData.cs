using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trafic.System;

public class SavetoData : MonoBehaviour
{

    Junction[] junc1;
    public DataBaseCross1 cross;


    private void Start()
    {
        cross = GameObject.FindGameObjectWithTag("DataBaseCross").GetComponent<DataBaseCross1>();

    }



    public void SavetoBase()
    {
        


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

}
