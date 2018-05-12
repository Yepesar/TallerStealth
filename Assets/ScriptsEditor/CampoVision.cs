using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Patrullero))]

public class CampoVision : Editor {

    private void OnSceneGUI()
    {
        Patrullero guardia = (Patrullero)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(guardia.transform.position, Vector3.up, Vector3.forward, 360, guardia.Radio_vision);
        Vector3 anguloVisionA = guardia.DireccionAngulo(-guardia.Angulo_vision/2, false);
        Vector3 anguloVisionB = guardia.DireccionAngulo(guardia.Angulo_vision / 2, false);

        Handles.DrawLine(guardia.transform.position, guardia.transform.position + anguloVisionA * guardia.Radio_vision);
        Handles.DrawLine(guardia.transform.position, guardia.transform.position + anguloVisionB * guardia.Radio_vision);

        Handles.color = Color.red;
        foreach (Transform objetivoVisible in guardia.objetivosVisibles)
        {
            Handles.DrawLine(guardia.transform.position, objetivoVisible.position);
        }
    }

}
