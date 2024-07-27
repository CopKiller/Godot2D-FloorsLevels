using System.Collections.Generic;
using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

namespace GodotFloorLevels.Scripts.WorldBase.Floors
{
    public partial class FloorManager : Node
{
    [Signal] public delegate void FloorGoUpEventHandler();
    [Signal] public delegate void FloorGoDownEventHandler();
    [Signal] public delegate void CheckFloorChangeEventHandler(Vector2I position);

    private readonly Dictionary<int, Floor> _floors = new Dictionary<int, Floor>();
    private readonly Dictionary<Vector2I, TileMapLayer> _upperLayerHidden = new Dictionary<Vector2I, TileMapLayer>();

    public int CurrentFloorLevel { get; set; }
    private int VisibilityRange { get; set; } = 1;

    public FloorManager()
    {
        FloorGoUp += GoUp;
        FloorGoDown += GoDown;
    }

    #region Receive by world
    
    public void AddFloor(int level)
    {
        if (!_floors.ContainsKey(level))
        {
            _floors[level] = new Floor(level, this);
        }
    }
    public void AddObjectToFloor(int level, TileMapLayer obj)
    {
        if (!_floors.ContainsKey(level)) return;

        if (obj.Name == "AttributesLayer")
        {
            _floors[level].DataLayer = obj;
            obj.Enabled = false;
        }
        else
        {
            _floors[level].Objects.Add(obj);
            obj.Visible = false;
            obj.FixInvalidTiles();
        }
    }
    private void ChangeFloor(int level)
    {
        if (!_floors.ContainsKey(level)) return;

        CurrentFloorLevel = level;
        UpdateVisibleObjects();
    }
    public void HideFloorsAbove(Vector2I position)
    {
        foreach (var floor in _floors.Values)
        {
            if (floor.Level <= CurrentFloorLevel) continue;

            foreach (var layer in floor.Objects)
            {
                if (layer.Name == "AttributesLayer") continue;

                var tileData = layer.GetCellTileData(position);
                if (tileData != null && layer.Visible)
                {
                    layer.TweenModulateAlpha(0, 0.5f)
                        .OnComplete(() => { layer.Visible = false; })
                        .Play();
                }
                else if (tileData == null && !layer.Visible)
                {
                    layer.TweenModulateAlpha(1, 0.5f)
                        .OnStart(() => { layer.Visible = true; })
                        .Play();
                }
            }
        }
    }
    
    #endregion

    #region Update Visible Objects
    
    public void UpdateVisibleObjects()
    {
        foreach (var floor in _floors.Values)
        {
            bool withinRange = IsWithinVisibilityRange(floor.Level);
            UpdateFloorObjectsVisibility(floor, withinRange);
            UpdateFloorDataLayer(floor);
            UpdateFloorChangeEvent(floor);
        }
    }
    private bool IsWithinVisibilityRange(int floorLevel)
    {
        return Mathf.Abs(floorLevel - CurrentFloorLevel) <= VisibilityRange;
    }
    private void UpdateFloorObjectsVisibility(Floor floor, bool withinRange)
    {
        foreach (var obj in floor.Objects)
        {
            int targetAlpha = withinRange ? 1 : 0;

            if (withinRange)
            {
                if (!obj.Visible)
                {
                    obj.Visible = true;
                    obj.Modulate = new Color(1, 1, 1, 0);
                }

                obj.ZIndex = floor.Level - CurrentFloorLevel - 1;
            }
            else
            {
                obj.ZIndex = -1;
            }

            obj.TweenModulateAlpha(targetAlpha, 0.5f)
               .OnComplete(() => { obj.Visible = withinRange; })
               .Play();
        }
    }
    private void UpdateFloorDataLayer(Floor floor)
    {
        floor.DataLayer.Enabled = floor.Level == CurrentFloorLevel;
    }
    private void UpdateFloorChangeEvent(Floor floor)
    {
        if (floor.Level == CurrentFloorLevel)
        {
            CheckFloorChange += floor.CheckPlayerPosition;
            GD.Print("CheckFloorChange += floor.CheckPlayerPosition");
        }
        else
        {
            CheckFloorChange -= floor.CheckPlayerPosition;
            GD.Print("CheckFloorChange -= floor.CheckPlayerPosition");
        }
    }
    
    #endregion
    
    #region Floor Events
    
    public void GoUp()
    {
        ChangeFloor(CurrentFloorLevel + 1);
    }
    public void GoDown()
    {
        ChangeFloor(CurrentFloorLevel - 1);
    }
    
    #endregion

    public override void _ExitTree()
    {
        FloorGoUp -= GoUp;
        FloorGoDown -= GoDown;
        base._ExitTree();
    }
}

}