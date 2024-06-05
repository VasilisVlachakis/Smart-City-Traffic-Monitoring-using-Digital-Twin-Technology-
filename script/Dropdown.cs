using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public string ID1;
    public TextMeshProUGUI output;
    
    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            ID1 = "";
        }

        if (val == 1)
        {
            ID1= "Iolkou 45";
        }
        if (val == 2)
        {
            ID1 = "Iolkou 35";
        }
        
        if (val == 3)
        {
            ID1 = "Iolkou 25";
        }
        
        if (val == 4)
        {
            ID1 = "Iolkou 15";
        }
       
        if (val == 5)
        {
            ID1 = "Iasonos60";
        }
        if (val == 6)
        {
            ID1 = "kartalh 10";
        }
        
        if (val == 7)
        {
            ID1 = "dimitriados 115";
        }
        if (val == 8)
        {
            ID1 = "taki ikonomaki 10";
        }
        
        if (val == 9)
        {
            ID1 = "sokratous 10";
        }
        if (val == 10)
        {
            ID1 = "28h Oktobriou 10";
        }
        if (val == 11)
        {
            ID1 = "kartalh 20";
        }
        if (val == 12)
        {
            ID1 = "kartalh 30";
        }
        if (val == 13)
        {
            ID1 = "kartalh 40";
        }
        if (val == 14)
        {
            ID1 = "Iasonos 65";
        }
        
        if (val == 15)
        {
            ID1 = "Iasonos75";
        }
        if (val == 16)
        {
            ID1 = "dimitriados 110";
        }
        
        if (val == 17)
        {
            ID1 = "dimitriados 100";
        }
        if (val == 18)
        {
            ID1 = "topali 10";
        }
                   
        if (val == 19)
        {
            ID1 = "spiridi 10";
        }

        if (val == 20)
        {
            ID1 = "spiridi 20";
        }

        if (val == 21)
        {
            ID1 = "spiridi 30";
        }

        if (val == 22)
        {
            ID1 = "spiridi 40";
        }

        if (val == 23)
        {
            ID1 = "topali 20";
        }

        if (val == 24)
        {
            ID1 = "topali 30";
        }

        if (val == 25)
        {
            ID1 = "sokratous 20";
        }

        if (val == 26)
        {
            ID1 = "sokratous 30";
        }

        if (val == 27)
        {
            ID1 = "28h Oktobriou 20";
        }

        if (val == 28)
        {
            ID1 = "28h Oktobriou 30";
        }

        if (val == 29)
        {
            ID1 = "taki ikonomaki 20";
        }
    }
    public InputField inputText;
    string text;
    public int car1;
   

    public void SaveCar()
    {
        text = inputText.text;
        int.TryParse(text, out car1);
       
    }

}
