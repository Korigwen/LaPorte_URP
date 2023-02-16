using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMove : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    
    void Start()
    {
        // Charger la scène "OtherScene" de manière asynchrone
        SceneManager.LoadSceneAsync("Scene01_Salon", LoadSceneMode.Additive).completed += OnSceneLoaded;
        Debug.Log("LoadScene");
    }
    
    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
         Scene otherScene = SceneManager.GetSceneByName("Scene01_Salon");
        if (otherScene.isLoaded)
        {
            // Rechercher la caméra "MyOtherCamera" dans la scène "OtherScene"
            GameObject Second_portal = GameObject.Find("RenderPlane");
            Debug.Log("Cherche otherPortal");
            if (Second_portal != null)
            {
                // Attribuer la caméra trouvée à la variable "otherCamera"
                otherPortal = Second_portal.GetComponent<Transform>();
                Debug.Log("otherPortal Trouvé");
            }
            
            GameObject PlayerCam = GameObject.Find("Main Camera");
            Debug.Log("Cherche Main Camera Scene01");
            if (PlayerCam != null)
            {
                // Attribuer la caméra trouvée à la variable "otherCamera"
                playerCamera = PlayerCam.GetComponent<Transform>();
                Debug.Log("PlayerCamera Trouvé");
            }
        }
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    Vector3 playerOffSetFromPortal = playerCamera.position - otherPortal.position;
    transform.position = portal.position + playerOffSetFromPortal;
    
    
    float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
    
    
    Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
    Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
    transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
          
          
    }
}
