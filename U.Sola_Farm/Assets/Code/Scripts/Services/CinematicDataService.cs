using UnityEngine;

public class CinematicDataService : ICinematicDataService
{
    public void SaveSeen(string cinematicId)
    {
        PlayerPrefs.SetInt("CinematicSeen_" + cinematicId, 1);
        PlayerPrefs.Save();
        Debug.Log($"[CinematicDataService] Cinematic '{cinematicId}' marked as seen");
    }

    public bool HasBeenSeen(string cinematicId)
    {
        return PlayerPrefs.HasKey("CinematicSeen_" + cinematicId);
    }
}
