using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public static bool IsLayer(int mask, string layer)
    {
        return ((1 << mask) & LayerMask.GetMask(layer)) != 0;
    }
}
