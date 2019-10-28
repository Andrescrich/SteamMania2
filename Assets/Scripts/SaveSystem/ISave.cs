using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave<T>
{
    void Save(T objectToSave, string key);
    T Load(string key);
}
