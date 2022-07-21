using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    // GLOBAL VARIABLES
    public static int G_GAMEWIDTH = 480;
    public static int G_GAMEHEIGHT = 270;
    public static float G_CELLSIZE = 16.0f;

    public static float G_INERTIA = 0.04f;
    public static float G_FRICTION = 0.16f;

    public static int G_SCALE_TO_HD = 4;

    public static string G_PLAYERNAME = "Player";

    private static Camera _camera;

    // GET MAIN CAMERA
    public static Camera camera
    {
        get {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    // GLOBAL FUNTIONS

    // EASE IN OUT SINE
    public static float EaseInOutSine(float inputvalue,float outputmin = 0f,float outputmax = 1f,float inputmax = 1f) {
        return outputmax * 0.5f * (1f - Mathf.Cos(Mathf.PI * inputvalue / inputmax)) + outputmin; }

    // EASE OUT SINE
    public static float EaseOutSine(float inputvalue,float outputmin = 0f,float outputmax = 1f,float inputmax = 1f) {
        return outputmax * Mathf.Sin(inputvalue / inputmax * (Mathf.PI / 2f)) + outputmin; }

    // EASE IN SINE
    public static float EaseInSine(float inputvalue,float outputmin = 0f,float outputmax = 1f,float inputmax = 1f) {
        return outputmax * (1f - Mathf.Cos(inputvalue / outputmax * (Mathf.PI / 2f))) + outputmin; }

    // APPROACH FLOAT
    public static float Approach(float from, float to, float by)
    {    
        if (from < to)
            return Mathf.Min((from + by), to);
        else 
            return Mathf.Max((from - by), to);
    }

    // RANDOM CHANCE
    public static bool Chance(float chance)
    {
        return (Random.Range(0f,1f) < chance);
    }
}