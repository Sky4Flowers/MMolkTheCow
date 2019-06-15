using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamInterest
{
    bool isOutOfView();
    Vector3 getPosition();
    void OnBecameInvisible();
    void OnBecameVisible();
}