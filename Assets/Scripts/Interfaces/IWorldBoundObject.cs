using Vector3 = UnityEngine.Vector3;

public interface IWorldBoundObject
{
    public void SetPosition(Vector3 aPosition);
    public Vector3 GetPosition();
    public void AddMyselfToWorldObjects();
}