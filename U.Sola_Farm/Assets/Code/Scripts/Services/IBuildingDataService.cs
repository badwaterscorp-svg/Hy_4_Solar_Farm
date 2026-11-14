using UnityEngine;

public interface IBuildingDataService
{
    void Save(string idSite, ResourceWrapper wrapper);
    bool TryLoad(string idSite, out ResourceWrapper wrapper);
}
