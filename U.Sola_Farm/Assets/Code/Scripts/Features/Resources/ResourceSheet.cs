using UnityEngine;

[CreateAssetMenu(fileName = "Resource" ,menuName = "SO/Resource Sheet")]
public class ResourceSheet : ScriptableObject
{
    [SerializeField] private string path;
    public string Path => path;
    [SerializeField] private Sprite spt;
    [SerializeField] private ResourceModel model;
    public Sprite Spt => spt;
    public ResourceModel Model => model;
    public ResourceModel GetModelCopy() => model.Copy();
}
