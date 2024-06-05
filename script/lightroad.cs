using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightroad : MonoBehaviour
{

    public string ID;
    private bool on = false;
    private Light mylightro;
    // Start is called before the first frame update
    void Start()
    {
        mylightro = GetComponent<Light>();
        mylightro.enabled = false;
    }
    string idro;
    public Dropdown dropdown17;
    // Update is called once per frame
    void Update()
    {
        dropdown17 = FindObjectOfType<Dropdown>();
        idro = dropdown17.ID1;
        if (idro == ID)
        {
            mylightro.enabled = true;

        }
        if (idro != ID)
        {
            mylightro.enabled = false;

        }
    }
}
