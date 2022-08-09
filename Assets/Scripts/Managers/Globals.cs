using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    // GLOBAL VARIABLES

    // DISPLAY
    public static int G_GAMEWIDTH = 480;
    public static int G_GAMEHEIGHT = 270;
    public static int G_SCALE_TO_HD = 4;
    public static float G_CELLSIZE = 16.0f;

    // PHYSICS
    public static float G_INERTIA = 0.04f;
    public static float G_FRICTION = 0.16f;
    public static float G_MOVEFORCE = 1.5f;
    public static float G_LNRDRAG = 2f;

    // GAME MANAGEMENT
    public static string G_PLAYERNAME = "Player";

    // INPUT
    public static float G_LS_DEADZONE = 0.2f;
    public static float G_RS_DEADZONE = 0.2f;

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

    // ROUND FLOAT TO PIXEL PERFECT
    public static float PPPos(float input)
    {
        return Mathf.RoundToInt(input * G_CELLSIZE) / G_CELLSIZE;
    }

    // ROUND VECTOR2 TO PIXEL PERFECT
    public static Vector2 PPPos(Vector2 input)
    {
        return new Vector2(
            Mathf.RoundToInt(input.x * G_CELLSIZE) / G_CELLSIZE, 
            Mathf.RoundToInt(input.y * G_CELLSIZE) / G_CELLSIZE);
    }

    // ROUND VECTOR2 TO PIXEL PERFECT
    public static Vector3 PPPos(Vector3 input)
    {
        return new Vector3(
            Mathf.RoundToInt(input.x * G_CELLSIZE) / G_CELLSIZE, 
            Mathf.RoundToInt(input.y * G_CELLSIZE) / G_CELLSIZE, 
            Mathf.RoundToInt(input.z * G_CELLSIZE) / G_CELLSIZE);
    }

    // HANDLE DEADZONES FLOAT
    public static float SmoothDZ(float input, float min, float max)
    {
        return Mathf.Max(0f, input - min) / (max - min);
    }

    // HANDLE DEADZONES VECTOR2
    public static Vector2 SmoothDZ(Vector2 input, float min, float max)
    {
        return new Vector2(SmoothDZ(input.x,min,max), SmoothDZ(input.y,min,max));
    }

    // CLOSEST IN LIST
    public static GameObject ClosestInList(Vector2 position, List<GameObject> list)
    {
        if (list.Count < 1) return null;

        float distance = Mathf.Infinity;
        float tempDist;
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            tempDist = Vector2.Distance(position, list[i].transform.position);
            if (tempDist < distance) {
                distance = tempDist;
                index = i;
            }
        }

        return list[index];
    }

    // FURTHEST OBJECT IN LIST
    public static GameObject FurthestInList(Vector2 position, List<GameObject> list)
    {
        if (list.Count < 1) return null;

        float distance = 0;
        float tempDist;
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            tempDist = Vector2.Distance(position, list[i].transform.position);
            if (tempDist > distance) {
                distance = tempDist;
                index = i;
            }
        }

        return list[index];
    }
}