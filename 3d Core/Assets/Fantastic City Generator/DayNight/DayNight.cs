using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class DayNight : MonoBehaviour
{

    public Material[] materialDay;
    public Material[] materialNight;

    public Material skyBoxDay;
    public Material skyBoxNight;

    public Light direcionalLight;

    [HideInInspector]
    public bool isNight;

    [HideInInspector]
    public bool isMoonLight;

    [HideInInspector]
    public bool isSpotLights;


    public void ChangeMaterial(bool night = false, bool moonLight = false, bool spotLights = false)
    {

        isNight = night;
        isMoonLight = moonLight;
        isSpotLights = spotLights;

        GameObject GmObj = GameObject.Find("City-Maker"); ;

        if (GmObj == null) return;

        Renderer[] children = GmObj.GetComponentsInChildren<Renderer>();

        Material[] myMaterials;

        for (int i = 0; i < children.Length; i++)
        {
            myMaterials = children[i].GetComponent<Renderer>().sharedMaterials;

            for (int m = 0; m < myMaterials.Length; m++)
            {
                for (int mt = 0; mt < materialDay.Length; mt++)
                if (night)
                {
                    if(myMaterials[m] == materialDay[mt])
                        myMaterials[m] = materialNight[mt];

                }
                else
                {
                    if (myMaterials[m] == materialNight[mt])
                        myMaterials[m] = materialDay[mt];
                }


                children[i].GetComponent<MeshRenderer>().sharedMaterials = myMaterials;
            }


        }

        ShiftStreetLights(night, spotLights);

        if (direcionalLight)
        {
            direcionalLight.GetComponent<Light>().enabled = (!night || moonLight);
            direcionalLight.intensity = (night) ? 0.2f : 1;
        }

        RenderSettings.skybox = (night) ? skyBoxNight : skyBoxDay;
        
        if (night)
        {
            RenderSettings.ambientMode = AmbientMode.Trilight;
            
            if (!moonLight)
                RenderSettings.ambientSkyColor = new Color(0.68f, 0.62f, 0.62f);
            else
                RenderSettings.ambientSkyColor = new Color(0.53f, 0.49f, 0.49f);


            RenderSettings.ambientEquatorColor = new Color(0.16f, 0.16f, 0.16f);
            RenderSettings.ambientGroundColor = new Color(0.07f, 0.07f, 0.07f); ;
        }
        else
        {

            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.64f, 0.74f, 0.74f);
            RenderSettings.ambientEquatorColor = new Color(0.74f, 0.74f, 0.74f);
            RenderSettings.ambientGroundColor = new Color(0.4f, 0.4f, 0.4f); ;

        }



        Debug.Log("Concluido");


    }

    void ShiftStreetLights(bool night, bool spotLight = false)
    {
        GameObject[] tempArray = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == ("_LightV")).ToArray();

        foreach (GameObject lines in tempArray)
        {
            lines.GetComponent<MeshRenderer>().enabled = night;
            if(lines.transform.GetChild(0))
                lines.transform.GetChild(0).GetComponent<Light>().enabled = (spotLight && night);
        }

        tempArray = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == ("_Spot_Light")).ToArray();

        foreach (GameObject lines in tempArray)
            lines.GetComponent<Light>().enabled = (spotLight && night);

    }


}
