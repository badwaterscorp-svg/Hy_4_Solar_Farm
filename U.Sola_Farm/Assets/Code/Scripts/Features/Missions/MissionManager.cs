using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [RequireBadInterface(typeof(IMission))]
    [SerializeField] private List<MissionSeeCinematic> _missions = new List<MissionSeeCinematic>();

    public void StartMission(int index)
    {
        if (index >= 0 && index < _missions.Count && _missions[index] != null)
            _missions[index].StartMission();
    }

    public void CompleteMission(int index)
    {
        if (index >= 0 && index < _missions.Count && _missions[index] != null)
            _missions[index].CompleteMission();
    }

    public void StartAllMissions()
    {
        foreach (var holder in _missions)
        {
            if (holder != null)
                holder.StartMission();
        }
    }
}

[System.Serializable]
public class MissionHolder
{
    [RequireBadInterface(typeof(IMission))]
    [SerializeField] private MonoBehaviour _mission;
    public IMission Mission => _mission as IMission;
}