using UnityEngine;

[CreateAssetMenu(fileName = "Resource" ,menuName = "SO/Resource Sheet")]
public class ResourceSheet : ScriptableObject
{
    [SerializeField] private string path;
    [SerializeField] private string pool;
    public string Path => path;
    public string Pool => pool;
    [SerializeField] private Sprite spt;
    [SerializeField] private ResourceModel model;
    public Sprite Spt => spt;
    public ResourceModel Model => model;
    public ResourceModel GetModelCopy() => model.Copy();
}
