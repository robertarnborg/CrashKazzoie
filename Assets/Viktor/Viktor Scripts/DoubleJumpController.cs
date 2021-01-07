using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpController : MonoBehaviour
{
    private List<DoubleJumpObject> objectList = new List<DoubleJumpObject>();

    private int listIndex = 0;

    public void AddObject(GameObject doubleJumpObject)
    {
        objectList.Add(doubleJumpObject.GetComponent<DoubleJumpObject>());
        listIndex++;
    }

    public void ResetDoubleJumpObjects()
    {
        if (listIndex == 0)
        {
            return;
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].ResetToken();
        }
        objectList.Clear();
        listIndex = 0;
    }

}
