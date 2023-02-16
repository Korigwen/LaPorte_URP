using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTexture : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Porte; //If porte fermer -> LoadScene
    public Camera cameraB;
    public Material cameraMatB;
    
    void Start()
    {
    
     SceneManager.LoadSceneAsync("Scene02_Chambre", LoadSceneMode.Additive).completed += OnSceneLoaded;
     Debug.Log("LoadScene2");
        
        
    }
    
     private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        Scene otherScene = SceneManager.GetSceneByName("Scene02_Chambre");
        if (otherScene.isLoaded)
        {
            // rechercher la caméra "MyOtherCamera" dans la scène "OtherScene"
            GameObject cameraBObj = GameObject.Find("Main Camera");
            if (cameraBObj != null)
            {
                // attribuer la caméra trouvée à la variable "otherCamera"
                cameraB = cameraBObj.GetComponent<Camera>();
            }
         }
    
        if(cameraB.targetTexture != null)
        {
        cameraB.targetTexture.Release();
        
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;
        
    }


}
