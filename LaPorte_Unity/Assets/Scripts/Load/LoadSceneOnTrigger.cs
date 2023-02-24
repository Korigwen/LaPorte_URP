using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class LoadSceneOnTrigger : MonoBehaviour
{
    // Nom de la prochaine scène à charger
    private string nextSceneName;

    // Est-ce que le joueur est dans le trigger ?
    private bool playerInTrigger;

    // Coroutine pour charger la scène de manière asynchrone
    private Coroutine loadSceneCoroutine;

    // Délai avant de charger la scène suivante (en secondes)
    public float delayBeforeLoading = 1f;

    // On enregistre le nom de la scène courante quand le script est activé
    private void Awake()
    {
        // On s'assure que la scène Void est bien chargée
        SceneManager.LoadScene("Scene0_Void", LoadSceneMode.Additive);

        // On récupère le nom de la première scène à charger
        nextSceneName = SceneLoader.GetNextSceneName("Scene0_Void");
    }

    // Lorsque le joueur entre dans le trigger, on charge la scène suivante de manière asynchrone
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerInTrigger)
        {
            playerInTrigger = true;

            // On commence à charger la scène suivante de manière asynchrone
            loadSceneCoroutine = StartCoroutine(LoadSceneAsync(nextSceneName));
        }
    }

    // Coroutine pour charger la scène de manière asynchrone
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Attendre que la scène soit complètement chargée
        while (!operation.isDone)
        {
            yield return null;
        }

        // Décharger la scène précédente
        if (SceneManager.sceneCount > 2)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 2));
        }

        // Mettre à jour le nom de la prochaine scène à charger
        nextSceneName = SceneLoader.GetNextSceneName(sceneName);
    }
}
