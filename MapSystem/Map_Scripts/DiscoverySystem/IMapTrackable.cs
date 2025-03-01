public interface IMapTrackable
{
    void InitializeMapIcon();
    void UpdateIcon();
    void AdjustIconPosition();
    void EnableIcon();
    void DisableIcon();
    bool IsInitialized();
}
