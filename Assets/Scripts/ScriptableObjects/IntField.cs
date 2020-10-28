using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Fields/IntField")]
public class IntField : ScriptableObject
{

    [SerializeField]
    private int value;
    public event System.Action<int> OnValueChanged;
    public int Value
    {
        get
        {
            return value;
        }

        set
        {
            if (Mathf.Abs(value - this.value) > 0)
            {
                OnValueChanged?.Invoke(value);
                this.value = value;
            }
        }
    }

    //public static implicit operator int(IntField b)
    //{
    //    return b.value;
    //}
    //public static implicit operator IntField(int b)
    //{
    //    return b;
    //}

}
