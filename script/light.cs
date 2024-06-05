using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{

    public string ID;
    private bool on = false;
    private Light mylight;
    // Start is called before the first frame update
    void Start()
    {
        mylight = GetComponent<Light>();
        mylight.enabled = false;
    }
    string idli;
    public Dropdownfanaria dropdown3;
    // Update is called once per frame
    void Update()
    {
         dropdown3 = FindObjectOfType<Dropdownfanaria>();
            idli = dropdown3.ID1;
        if (idli == ID)
        {
            mylight.enabled = true;

        }
        if (idli != ID)
        {
            mylight.enabled = false;

        }
    }
}
