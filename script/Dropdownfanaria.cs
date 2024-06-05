using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dropdownfanaria : MonoBehaviour
{
    public string ID1;
    public TextMeshProUGUI output;

    public void HandleInputData1(int val)
    {
        if (val == 0)
        {
            ID1 = " ";
        }

        if (val == 1)
        {
            ID1 = "Iolkou me sokratous";
        }
       
        if (val == 2)
        {
            ID1 = "Iolkou me dhmitriados";
        }
        if (val == 3)
        {
            ID1 = "Iolkou me 28ohs";
        }
        
        if (val == 4)
        {
            ID1 = "Iolkou me iasonos";
        }
        if (val == 5)
        {
            ID1 = "kartalh me iasonos";
        }
       
        if (val == 6)
        {
            ID1 = "kartalh me dimitriados";
        }
        
        if (val == 7)
        {
            ID1 = "kartalh me taki ikonomaki";
        }
        
       
        
        if (val == 8)
        {
            ID1 = "Iasonos me topali";
        }
               
        if (val == 9)
        {
            ID1 = "dimitriados me topali";
        }
        
       
        if (val == 10)
        {
            ID1 = "Iasonos me spiridi";
        }
        if (val == 11)
        {
            ID1 = "dimitriados me spiridi";
        }
        if (val == 12)
        {
            ID1 = "kartalh me sokratous";
        }
        if (val == 13)
        {
            ID1 = "kartalh me 28ohs";
        }
        
        if (val == 14)
        {
            ID1 = "Iolkou me taki ikonomaki";

        }


        if (val == 15)
        {
            ID1 = "topali me sokratous";

        }

        if (val == 16)
        {
            ID1 = "topali me 28ohs";

        }

    }
    public InputField inputText;
    public InputField inputText2;
    string text;
    public int time;
    public int time2;
    string text2;

    public void SavePhase()
    {
        text = inputText.text;
        int.TryParse(text, out time);

    }
    public void SavePhase2()
    {
        text2 = inputText2.text;
        int.TryParse(text2, out time2);

    }

}
