using EventBusSystem;

public interface ISoundHandler : IGlobalSubscriber
{
    void HandleTap();
    void HandleSport();
    void HandleTieBreak();
    void HandleFinish();
}