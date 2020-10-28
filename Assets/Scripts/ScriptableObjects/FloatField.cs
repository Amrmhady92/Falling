using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Fields/FloatField")]
public class FloatField : ScriptableObject
{

    [SerializeField]
    private float value;

    public event System.Action<float> OnValueChanged;
    public float Value
    {
        get
        {
            return value;
        }

        set
        {
            if (Mathf.Abs(value - this.value) > 0.0001f)
            {
                if (value < .001f && value > -.001f)
                {
                    value = 0;
                }
                if (OnValueChanged != null)
                    OnValueChanged(value);
                this.value = value;

            }
        }
    }

    //public static implicit operator float(FloatField b)
    //{
    //    return b.Value;
    //}


}

