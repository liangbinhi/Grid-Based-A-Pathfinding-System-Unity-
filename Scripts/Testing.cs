using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{

    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;

    [Header("Path Line")]
    [SerializeField] private float lineWidth = 2f;
    [SerializeField] private float lineZOffset = -2f;

    private Pathfinding pathfinding;
    private LineRenderer lineRenderer;

    private void Start()
    {
        pathfinding = new Pathfinding(20, 10);

        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());

        CreatePathLine();
    }

    private void CreatePathLine()
    {
        GameObject pathLineObj = new GameObject("PathLine");
        lineRenderer = pathLineObj.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // ✅ 红色路径
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = false;

        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        lineRenderer.sortingOrder = 100;
    }

    private void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            // ✅ 核心修复：起点改为角色当前位置
            Vector3 startPosition = characterPathfinding.transform.position;

            List<Vector3> path = pathfinding.FindPath(startPosition, mouseWorldPosition);

            ShowPath(path);

            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            PathNode node = pathfinding.GetNode(x, y);
            if (node != null)
            {
                node.SetIsWalkable(!node.isWalkable);
            }

            ClearPath();
        }
    }

    private void ShowPath(List<Vector3> path)
    {
        if (path == null || path.Count == 0)
        {
            ClearPath();
            return;
        }

        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 pos = path[i];
            pos.z = lineZOffset;
            lineRenderer.SetPosition(i, pos);
        }
    }

    private void ClearPath()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }
}