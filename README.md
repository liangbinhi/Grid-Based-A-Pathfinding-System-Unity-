# Grid-Based A* Pathfinding System (Unity)

<img width="1920" height="872" alt="image" src="https://github.com/user-attachments/assets/d0d1ec6d-d206-4cef-b5c3-8d61448582f2" />

<img width="1920" height="872" alt="image" src="https://github.com/user-attachments/assets/7d334de6-2fad-461e-817a-31a9f8340a80" />

---

## 项目简介

本项目基于 Unity 实现了一个二维网格（Grid-Based）的路径寻路系统，核心算法采用 A*（A-Star）算法。
系统支持动态障碍物、实时路径计算以及路径可视化，适用于策略类游戏、RPG、塔防等场景。

项目重点在于算法实现与系统结构设计，强调模块解耦与可扩展性。

---

## 功能展示

【在这里放第二张图或GIF】

* 鼠标点击目标点，角色自动寻路移动
* 实时生成路径并使用 LineRenderer 显示
* 支持右键动态设置/取消障碍物
* 自动避开障碍物重新计算路径
* 支持 8方向移动（含对角线）

---

## 技术栈

* Unity（C#）
* A* Pathfinding Algorithm
* 自定义 Grid 网格系统
* LineRenderer 路径可视化

---

## 核心功能

* A* 算法路径搜索（gCost / hCost / fCost）
* OpenList / ClosedList 节点管理
* 世界坐标与网格坐标转换
* 动态障碍物系统（实时更新）
* 路径可视化（替代 Debug.DrawLine）
* 角色沿路径移动

---

## 技术亮点

### 1. A* 算法实现

* 使用 fCost = gCost + hCost 作为评估函数
* 支持对角线移动优化路径长度
* 使用启发函数减少搜索空间，提高效率

---

### 2. 网格系统封装（Grid<T>）

* 泛型设计，提高复用性
* 支持世界坐标 ↔ 网格坐标转换
* 使用事件机制（OnGridObjectChanged）驱动更新

---

### 3. 表现层与逻辑解耦

项目结构清晰拆分为：

* Pathfinding（算法层）
* Grid（数据层）
* Visual（表现层）

实现逻辑与渲染分离，便于扩展与维护。

---

### 4. 路径可视化优化

* 使用 LineRenderer 绘制路径
* 支持动态更新路径点
* 避免 Debug.DrawLine 仅限调试的问题

---

## 核心代码

### 路径计算

```csharp
public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
{
    grid.GetXY(startWorldPosition, out int startX, out int startY);
    grid.GetXY(endWorldPosition, out int endX, out int endY);

    List<PathNode> path = FindPath(startX, startY, endX, endY);

    if (path == null) return null;

    List<Vector3> vectorPath = new List<Vector3>();

    foreach (PathNode node in path)
    {
        vectorPath.Add(new Vector3(node.x, node.y) * grid.GetCellSize());
    }

    return vectorPath;
}
```

---

### 路径渲染

```csharp
private void ShowPath(List<Vector3> path)
{
    lineRenderer.positionCount = path.Count;

    for (int i = 0; i < path.Count; i++)
    {
        lineRenderer.SetPosition(i, path[i]);
    }
}
```

---

## 项目结构

```
Scripts/
├── Grid.cs                // 网格系统
├── PathNode.cs           // 节点数据结构
├── Pathfinding.cs        // A*算法核心
├── PathfindingVisual.cs  // 网格渲染
├── Testing.cs            // 输入与路径控制
```

---

## 可扩展方向

* Jump Point Search（JPS）优化性能
* 不同地形权重（如道路/泥地）
* 多单位寻路系统
* 动态避障（Steering / RVO）
* 与 NavMesh 混合寻路

---

## 项目价值

本项目完整实现了从数据结构设计、算法实现到可视化展示的完整流程，具备良好的工程结构与扩展能力。

适合作为：

* 游戏开发基础模块
* AI路径规划学习案例
* Unity工程结构设计参考

---

