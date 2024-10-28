public enum ObserverType
{
    ePlayer,
    eBullet,
    eEnemy,
    eUI
}
public interface IObserver
{
    public bool ReciveMessage(PostMasterMessage.Message aMessage);
}
