using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObjectsOnTrigger : MonoBehaviour
{
    // GameObjects à activer et désactiver
    public GameObject[] activateOnEnter;
    public GameObject[] deactivateOnEnter;
    public GameObject[] activateOnExit;
    public GameObject[] deactivateOnExit;

    // Nombre de fois que le joueur est entré dans le trigger
    private int enterCount = 0;

    // Le joueur est-il dans le trigger ?
    private bool playerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;

            // Si c'est la première fois que le joueur entre dans le trigger, on active les GameObjects "Scene1_Salon" et "Scene2_Chambre"
            if (enterCount == 0)
            {
                ActivateGameObjects(activateOnEnter);
            }
            // Sinon, on désactive les GameObjects "Scene1_Salon" et on active les GameObjects "Scene3_Chambre" (ou "Scene4_", "Scene5_", etc. en fonction du nombre de fois que le joueur est entré dans le trigger)
            else
            {
                DeactivateGameObjects(deactivateOnEnter);

                int nextSceneIndex = enterCount + 2;
                if (nextSceneIndex > 8)
                {
                    nextSceneIndex = 8;
                }

                GameObject[] nextSceneObjects = GetSceneObjects(nextSceneIndex);
                ActivateGameObjects(nextSceneObjects);
            }

            enterCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;

            // Si le joueur sort du trigger, on désactive les GameObjects "Scene3_Chambre" (ou "Scene4_", "Scene5_", etc. en fonction du nombre de fois que le joueur est entré dans le trigger) et on active les GameObjects "Scene2_Chambre" (ou "Scene3_", "Scene4_", etc. en fonction du nombre de fois que le joueur est entré dans le trigger)
            int exitCount = enterCount - 1;
            if (exitCount >= 0)
            {
                DeactivateGameObjects(deactivateOnExit);

                int previousSceneIndex = exitCount + 1;
                GameObject[] previousSceneObjects = GetSceneObjects(previousSceneIndex);
                ActivateGameObjects(previousSceneObjects);
            }
        }
    }

    // Active tous les GameObjects dans le tableau
    private void ActivateGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    // Désactive tous les GameObjects dans le tableau
    private void DeactivateGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    // Retourne les GameObjects correspondant à l'index de la scène
    private GameObject[] GetSceneObjects(int sceneIndex)
    {
        string sceneName = "Scene" + sceneIndex.ToString() + "_";
        GameObject[] sceneObjects = GameObject.FindGameObjectsWithTag(sceneName);
        return sceneObjects;
    }
}
