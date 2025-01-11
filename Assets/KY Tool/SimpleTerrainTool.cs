using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SimpleTerrainTool : EditorWindow
{
    private Terrain terrain; // 地形对象
    private float height = 10f; // 设置的高度
    private float brushSize = 5f; // 画笔大小
    private float smoothStrength = 0.5f; // 平滑强度

    // 多语言文本变量
    private string toolTitle;//工具标题
    private string selectTerrainText;//选择地形
    private string setHeightText; //设置文本高度                   
    private string applyHeightButton;//应用高度按钮
    private string brushSizeText;//画笔大小
    private string smoothStrengthText;//平滑强度
    private string smoothTerrainButton;//平滑地形按钮
    private string warningMessage;//警告信息    
    private string heightSetMessage;//高度设置信息
    private string terrainSmoothMessage;//地形平滑信息

    [MenuItem("Ky Tools/Simple Terrain Tool")]
    public static void ShowWindow()
    {
        GetWindow<SimpleTerrainTool>("Terrain Tool");
    }

    private void OnEnable()
    {
        SetLanguage();
    }

    //setting terrain
    private void OnGUI()
    {
        GUILayout.Label(toolTitle, EditorStyles.boldLabel);

        // 选择地形对象
        terrain = (Terrain)EditorGUILayout.ObjectField(selectTerrainText, terrain, typeof(Terrain), true);

        if (terrain == null)
        {
            EditorGUILayout.HelpBox(warningMessage, MessageType.Warning);
            return;
        }

        // 设置高度
        height = EditorGUILayout.Slider(setHeightText, height, 0, 50);
        if (GUILayout.Button(applyHeightButton))
        {
            SetTerrainHeight();
        }

        // 平滑地形
        brushSize = EditorGUILayout.Slider(brushSizeText, brushSize, 1, 50);
        smoothStrength = EditorGUILayout.Slider(smoothStrengthText, smoothStrength, 0, 1);
        if (GUILayout.Button(smoothTerrainButton))
        {
            SmoothTerrain();
        }
    }

    // 设置地形高度
    private void SetTerrainHeight()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = this.height / terrainData.size.y;
            }
        }

        terrainData.SetHeights(0, 0, heights);
        Debug.Log(heightSetMessage + this.height);
    }
                       
    // 平滑地形
    private void SmoothTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                float avg = (heights[x - 1, y] + heights[x + 1, y] + heights[x, y - 1] + heights[x, y + 1]) / 4f;
                heights[x, y] = Mathf.Lerp(heights[x, y], avg, smoothStrength);
            }
        }

        terrainData.SetHeights(0, 0, heights);
        Debug.Log(terrainSmoothMessage);
    }

    // 设置多语言支持 setLanguage
    private void SetLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.ChineseSimplified:
                toolTitle = "简单地形工具";
                selectTerrainText = "选择地形";
                setHeightText = "设置高度";
                applyHeightButton = "应用高度";
                brushSizeText = "画笔大小";
                smoothStrengthText = "平滑强度";
                smoothTerrainButton = "平滑地形";
                warningMessage = "请选择一个地形对象。";
                heightSetMessage = "地形高度设置为: ";
                terrainSmoothMessage = "地形已平滑。";
                break;

            case SystemLanguage.Spanish:
                toolTitle = "Herramienta de Terreno Simple";
                selectTerrainText = "Seleccionar Terreno";
                setHeightText = "Configurar Altura";
                applyHeightButton = "Aplicar Altura";
                brushSizeText = "Tamaño del Pincel";
                smoothStrengthText = "Fuerza de Suavizado";
                smoothTerrainButton = "Suavizar Terreno";
                warningMessage = "Por favor seleccione un terreno.";
                heightSetMessage = "Altura del terreno establecida en: ";
                terrainSmoothMessage = "Terreno suavizado.";
                break;

            default: // 英文 English
                toolTitle = "Simple Terrain Tool";
                selectTerrainText = "Select Terrain";
                setHeightText = "Set Height";
                applyHeightButton = "Apply Height";
                brushSizeText = "Brush Size";
                smoothStrengthText = "Smooth Strength";
                smoothTerrainButton = "Smooth Terrain";
                warningMessage = "Please select a terrain.";
                heightSetMessage = "Terrain height set to: ";
                terrainSmoothMessage = "Terrain smoothed.";
                break;
        }
    }
}
