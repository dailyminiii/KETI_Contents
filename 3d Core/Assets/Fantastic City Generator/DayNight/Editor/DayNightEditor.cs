using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayNight))]
public class DayNightEditor : Editor
{

    DayNight dayNight;
    bool moonLight;
    bool _moonLight;

    bool spotLights;
    bool _spotLights;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        dayNight = (DayNight)target;

        if (dayNight.gameObject.activeInHierarchy)
        {


            GUILayout.Space(10);

            if (GUILayout.Button("Day"))
            {
                dayNight.ChangeMaterial(false, moonLight, spotLights);
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Night"))
            {
                dayNight.ChangeMaterial(true, moonLight, spotLights);
            }


            if (dayNight.direcionalLight && dayNight.isNight)
            {
                GUILayout.Space(10);
                moonLight = GUILayout.Toggle(dayNight.isMoonLight, " MoonLight", GUILayout.Width(240));

                GUILayout.Space(10);
                spotLights = GUILayout.Toggle(dayNight.isSpotLights, " SpotLights on Street lighting", GUILayout.Width(240));

                if ((_spotLights != spotLights) || (_moonLight != moonLight))
                    dayNight.ChangeMaterial(dayNight.isNight, moonLight, spotLights);

                _moonLight = moonLight;
                _spotLights = spotLights;
                
            }



            GUILayout.Space(10);

        }

    }



}
