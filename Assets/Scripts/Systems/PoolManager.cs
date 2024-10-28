using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class PoolManager : MonoBehaviour
{
    private List<IPoolObject> myPoolObjects = new List<IPoolObject>();
    private AsyncOperationHandle<GameObject> myAsyncHandle;
    private AssetReferenceGameObject myAssetReference;
    public void Init(AssetReferenceGameObject anAssetReference)
    {
        myAssetReference = anAssetReference;
        myAsyncHandle = myAssetReference.LoadAssetAsync<GameObject>();

    }
    public void SetAsyncHandleThroughString(string aPath)//This is for testing only
    {
        myAsyncHandle = Addressables.LoadAssetAsync<GameObject>(aPath);
    }
    public bool GetAsyncHandleDone()
    {
        return myAsyncHandle.IsDone;
    }
    public void Start()
    {
        AddPoolObject(); //assuring there will be at least 1 object ready (why else would you need a pool?)
    }
    public void AddPoolObjectBuffer(int buffer)
    {
        for (int i = 0; i < buffer; i++)
        {
            AddPoolObject();
        }
    }
    public void AddPoolObject()
    {
        myAsyncHandle.Completed += AsyncHandle_Completed;
    }
    public void ReturnAllObjects()
    {
        for (int i = 0; i < myPoolObjects.Count; i++)
        {
            if (myPoolObjects[i].IsBeingUsed())
            {
                myPoolObjects[i].DeactivateObject();
            }
        }
    }
    public IPoolObject GetAFreeObject()
    {
        int index = 0;
        for (int i = 0; i < myPoolObjects.Count; i++)
        {
            if (!myPoolObjects[i].IsBeingUsed())
            {
                index = i;
                myPoolObjects[i].ActivateObject();
                if (i == myPoolObjects.Count - 1) //If we asked for the last object in the pool we give it time to spawn a new
                {
                    AddPoolObject();
                }
                break;
            }
        }
        return myPoolObjects[index];
    }
    public void Update()
    {
        for (int i = 0; i < myPoolObjects.Count; i++)
        {
            if (myPoolObjects[i].IsBeingUsed())
            {
                myPoolObjects[i].Update();
            }
        }
    }
    private void AsyncHandle_Completed(AsyncOperationHandle<GameObject> anAsyncHandle)
    {
        if (anAsyncHandle.Status == AsyncOperationStatus.Succeeded)
        {
            myPoolObjects.Add(Instantiate(anAsyncHandle.Result).GetComponent<IPoolObject>());
            myPoolObjects[myPoolObjects.Count - 1].DeactivateObject();
        }
        else
        {
            Debug.Log("ERROR - PoolObject was not loaded correctly");
        }
    }
}
