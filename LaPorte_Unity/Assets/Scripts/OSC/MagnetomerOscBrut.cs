using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetomerOscBrut : MonoBehaviour
{
    public OSC osc;
    private float x;
    
    
    void Start(){
        osc.SetAddressHandler("/magneticfield", OnReceiveMagneticFiled);
        
    }
    

    void OnReceiveMagneticFiled(OscMessage message)
    {
        Debug.Log("Osc_OK");
        
            float x = message.GetFloat(2);
        
            // Mettre à jour la rotation de l'objet
            Vector3 rot = transform.localEulerAngles;
            rot.y = ((x + 12) / 30) * 110;
            transform.localEulerAngles = rot;
    
        
    }
       
}