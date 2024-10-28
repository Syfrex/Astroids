using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public interface IPoolObject
{
    public GameObject GameObject { get; }
    public bool IsBeingUsed();
    public void ActivateObject();
    public void Update();
    public void SetDirection(Vector3 aDirection);
    public void DeactivateObject();

}
