using UnityEngine;

[System.Serializable]
public class PlayerModel : Extensions.ICopy<PlayerModel>
{
    public float MoveSpeed = 5f;

    public PlayerModel Copy()
    {
        return (PlayerModel)MemberwiseClone();
    }
}
