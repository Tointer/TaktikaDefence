using System;
using UnityEngine;

public class MathUtils : MonoBehaviour
{
    
    public static Func<float, float> GetFunction(Functions myEnum)
    {
        switch (myEnum)
        {
            case Functions.Linear:
                return (x) => x;
            case Functions.Log:
                return (x) => Mathf.Log(Mathf.Max(x, 1), 2);
            case Functions.Squared:
                return (x) => Mathf.Pow((x),2);
            default:
                throw new ArgumentOutOfRangeException(nameof(myEnum), myEnum, null);
        }
    }
}
public enum Functions{Linear, Log, Squared}