using EventBusSystem;

public interface ISoundHandler : IGlobalSubscriber
{
    void HandleTap();
    void HandleToggleStatus();
    void HandleSport();
    void HandleTieBreak();
}