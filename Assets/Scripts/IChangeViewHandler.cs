using EventBusSystem;

public interface IChangeViewHandler : IGlobalSubscriber
{
    void HandleShowTimer();
    void HandleShowSettings();
}