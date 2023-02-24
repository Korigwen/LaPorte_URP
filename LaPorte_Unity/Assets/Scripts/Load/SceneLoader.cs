using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLoader
{
    private static readonly List<string> sceneOrder = new List<string>()
    {
        "Scene0_Void",
        "Scene01_Salon",
        "Scene02_Chambre",
        "Scene3_Chambre",
        // Ajouter d'autres noms de scènes ici dans l'ordre
    };

    public static string GetNextSceneName(string currentSceneName)
    {
        int currentIndex = sceneOrder.IndexOf(currentSceneName);
        if (currentIndex < 0 || currentIndex >= sceneOrder.Count - 1)
        {
            return null; // Retourne null s'il n'y a pas de prochaine scène
        }

        return sceneOrder[currentIndex + 1];
    }
}
