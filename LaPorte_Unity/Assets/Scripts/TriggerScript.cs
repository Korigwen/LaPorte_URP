using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour 
{

    public GameObject[] scenes; // tableau contenant les GameObjects à activer/désactiver
    public GameObject player; // le GameObject qui déclenche le changement de scène
    private int currentSceneIndex = 0; // l'index de la scène actuelle

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player && currentSceneIndex < scenes.Length) { // si c'est le joueur qui a touché le trigger et s'il reste des scènes à activer/désactiver
            if (currentSceneIndex > 0) { // si ce n'est pas la première scène
                scenes[currentSceneIndex - 1].SetActive(false); // désactive la scène précédente
            }
            scenes[currentSceneIndex].SetActive(true); // active la scène suivante
            currentSceneIndex++; // incrémente l'index de la scène actuelle
        }
    }
}






