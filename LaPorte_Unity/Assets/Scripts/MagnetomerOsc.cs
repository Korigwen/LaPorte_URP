using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetomerOsc : MonoBehaviour
{
    public OSC osc;
    private float x;
    private float previousX;
    private LowPassFilter filter = new LowPassFilter(2f);
    public float threshold = 2f; // Seuil pour la variation de x
    
    void Start(){
        osc.SetAddressHandler("/magneticfield", OnReceiveMagneticFiled);
        
    }
    

    void OnReceiveMagneticFiled(OscMessage message)
    {
        float x = message.GetFloat(2);
        
        x = filter.Filter(x);
        

        if (Mathf.Abs(x - previousX) < threshold)
        {
            // Mettre à jour la rotation de l'objet
            Vector3 rot = transform.localEulerAngles;
            rot.y = ((x + 12) / 30) * 110;
            transform.localEulerAngles = rot;

            // Sauvegarder la valeur précédente de x
            previousX = x;
        }
        
    }
    
    public class LowPassFilter
    {
        private float _smoothing;
        private float _lastValue;

        public LowPassFilter(float smoothing)
        {
            _smoothing = smoothing;
        }

        public float Filter(float value)
        {
            _lastValue = Mathf.Lerp(_lastValue, value, _smoothing);
            return _lastValue;
        }
    }
    
}
