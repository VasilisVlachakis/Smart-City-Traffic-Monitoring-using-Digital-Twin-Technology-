using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trafic.System;


public class toggleRoads : MonoBehaviour
{

    public Toggle tog;

    Road[] rod;

    public Button bts;

    public DataBaseRoads rods;
    void Start()
    {
        tog.onValueChanged.AddListener(delegate { togval(tog); });
        rods = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBaseRoads>();
        bts.onClick.AddListener(toggas);

    }
    void toggas()
    {

        tog.isOn = false;
    }

    public bool Databaseonandoff=true;
     void togval(Toggle tglva)
    {
        Databaseonandoff = tglva.isOn;

        if (Databaseonandoff==true)
        {
            
            }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
