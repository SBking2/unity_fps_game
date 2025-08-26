using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }
    Editor m_editor;
    public InspectorView()
    {
        
    }
    public void UpdateSelector(NodeView view)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(m_editor);
        m_editor = Editor.CreateEditor(view.node);
        IMGUIContainer container = new IMGUIContainer(() => { 
            if(m_editor.target)
            {
                m_editor.OnInspectorGUI();
            }
        });
        Add(container);
    }
}
