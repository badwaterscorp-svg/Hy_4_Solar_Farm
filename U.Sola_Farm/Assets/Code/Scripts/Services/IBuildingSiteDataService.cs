using UnityEngine;

public interface IBuildingSiteDataService
{
    void Save(string idSite, ResourceWrapper wrapper);
    bool TryLoad(string idSite, out ResourceWrapper wrapper);
}
