public interface IPlayerState
{
    void EnterState();
    void UpdateState();
    // Will add FixedUpdateState here
    void ExitState();
}