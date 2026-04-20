using UnityEngine;

public class BuildingSiteDataService : IBuildingSiteDataService
{
    public void Save(string idSite, ResourceWrapper wrapper)
    {
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("BuildingSite" + idSite, json);
        PlayerPrefs.Save();
        Debug.Log($"[BuildingSiteDataService] Saved site: BuildingSite{idSite}");
    }

    public bool TryLoad(string idSite, out ResourceWrapper wrapper)
    {
        string key = "BuildingSite" + idSite;
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            wrapper = JsonUtility.FromJson<ResourceWrapper>(json);
            Debug.Log($"[BuildingSiteDataService] Loaded site: BuildingSite{idSite}");
            return true;
        }
        wrapper = null;
        Debug.Log($"[BuildingSiteDataService] No saved data for site: BuildingSite{idSite}");
        return false;
    }
}
