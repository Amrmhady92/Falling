using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Renderer m_renderer;
    private bool m_highlighted;
    public Color m_color;

    void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //Remove for Final
        if (Input.GetKeyDown(KeyCode.F))
        {
            HighlightObject();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            m_renderer.material.shader = Shader.Find("Diffuse");
        }
    }

    public void HighlightObject()
    {
        m_renderer.material.shader = Shader.Find("Outline");
        m_renderer.material.SetColor("_OutlineColor", m_color);
        m_renderer.material.SetFloat("_Outline", 1.25f);
    }
}
