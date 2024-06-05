using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trafic.System;

public class ToggleCrossRoads : MonoBehaviour
{

    public Toggle togs;
    Junction[] jun;
    public DataBaseCross1 cro;

    public Button bt;
    public Button bt2;

    void Start()
    {
        togs.onValueChanged.AddListener(delegate { togvals(togs); });
        cro = GameObject.FindGameObjectWithTag("DataBaseCross").GetComponent<DataBaseCross1>();
        bt.onClick.AddListener(togga);
        bt2.onClick.AddListener(togga);
    }

    public bool Databaseonandoffs = true;
    void togga(){

        togs.isOn = false;
            }

    void togvals(Toggle tglva)
    {
        Databaseonandoffs = tglva.isOn;
        if (Databaseonandoffs == true)
        {
            
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
