/*
 * Unity Simple Radar - the simplest radar for Unity
 * 18.12.2020
 * by Centrix14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usr : MonoBehaviour
{
    public float search_range = 10;
    public float scale = 0.2f;
    public float panx = 0f, pany = 0f, panz = 0f;
    public float point_size = 0.2f;

    public List<GameObject> visible_go = new List<GameObject>();
    public GameObject base_go;

    public Color line_color;

    public GameObject GOfindByIndexGroup(string group, int index)
    {
        string name = string.Concat(group, Convert.ToString(index));

        return GameObject.Find(name);
    }

    public bool IsGOvisible(GameObject bo, GameObject go)
    {
        float dist = Vector3.Distance(bo.transform.position, go.transform.position);

        return (dist <= search_range);
    }

    public void CrawlGroup(string group, int start, int end)
    {
        GameObject elm;

        visible_go.Clear();

        for (int i = start; i <= end; i++)
        {
            elm = GOfindByIndexGroup(group, i);

            if (elm == null)
                continue;

            if (IsGOvisible(base_go, elm))
                visible_go.Add(elm);
            else
            {
                if (elm.GetComponent<LineRenderer>())
                    Destroy(elm.GetComponent<LineRenderer>());
            }
        }
    }

    public void DisplayVisibleGO()
    {
        float px, py;

        foreach (GameObject elm in visible_go)
        {
            px = (elm.transform.position.x - base_go.transform.position.x) * scale + panx;
            py = (elm.transform.position.y - base_go.transform.position.y) * scale + pany;

            if (elm != null)
                DrawRect(elm, px, py);
        }
    }

    public void DrawRect(GameObject go, float x, float y)
    {
        LineRenderer l = go.AddComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();

        if (l == null)
            l = go.GetComponent<LineRenderer>();

        pos.Add(new Vector3(x, y, panz));
        pos.Add(new Vector3(x + point_size, y, panz));

        l.startWidth = point_size;
        l.endWidth = point_size;

        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
        l.alignment = LineAlignment.TransformZ;

        l.material = new Material(Shader.Find("Sprites/Default"));
        l.startColor = line_color;
        l.endColor = line_color;
    }

	public void USRoptions(GameObject main, Color opt_color, float opt_range, float opt_scale, float opt_panel_x, float opt_panel_y, float opt_panel_z, float opt_point_size)
    {
        base_go = main;

        line_color = opt_color;

        search_range = opt_range;
        scale = opt_scale;
        panx = opt_panel_x;
        pany = opt_panel_y;
        panz = opt_panel_z;

        point_size = opt_point_size;
    }
}
