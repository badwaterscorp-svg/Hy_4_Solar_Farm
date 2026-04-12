using B_Extensions;
using UnityEngine;
using System.Linq;

public class ResourcesRepository : Singleton<ResourcesRepository>
{
    [SerializeField] private ResourceSheet[] sheets;

    public ResourceSheet GetResourcesRandom() => sheets[Random.Range(0, sheets.Length)];

    public ResourceSheet GetSheetByName(string name) 
    {
        return System.Array.Find(sheets, s => s.Model.Name.Equals(name));
    }

    public Sprite GetSpriteByNameAndQuality(string nameResource) 
    {
        var _sheet = System.Array.Find(sheets, s => s.Model.Name.Equals(nameResource));
        if (_sheet)
            return _sheet.Spt;
        return null;
    }
}
