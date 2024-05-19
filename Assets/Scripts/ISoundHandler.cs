using EventBusSystem;

public interface ISoundHandler : IGlobalSubscriber
{
    void HandleTap();
    void HandleToggle();
    void HandleSport();
    void HandleTieBreak();
}