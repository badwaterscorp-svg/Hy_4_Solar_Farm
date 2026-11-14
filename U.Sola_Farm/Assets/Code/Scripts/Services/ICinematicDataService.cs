public interface ICinematicDataService
{
    void SaveSeen(string cinematicId);
    bool HasBeenSeen(string cinematicId);
}
