using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave<T>
{
    void Save(string key);
    T Load(string key);
}
