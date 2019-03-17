using UnityEngine;
using UnityEditor;

public static class TestState
{
    public static bool selected = false;

    public static void SetSelectedTrue()
    {
        selected = true;
    }
    public static void SetSelectedFalse()
    {
        selected = false;
    }
    public static bool isSelected()
    {
        return selected;
    }
}