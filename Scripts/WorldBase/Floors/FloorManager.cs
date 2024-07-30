using System;
using System.Collections.Generic;
using Godot;
using GodotFloorLevels.Scripts.ControlsBase.MapBase;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

namespace GodotFloorLevels.Scripts.WorldBase.Floors
{
    public partial class FloorManager : GodotObject
    {
        [Signal]
        public delegate void FloorGoUpEventHandler();

        [Signal]
        public delegate void FloorGoDownEventHandler();

        [Signal]
        public delegate void CheckFloorChangeEventHandler(Vector2I position, Node2D player);

        private readonly Dictionary<int, Floor> _floors = new Dictionary<int, Floor>();

        public int CurrentFloorLevel { get; set; }

        private Vector2I _playerPosition;

        public FloorManager()
        {
            FloorGoUp += GoUp;
            FloorGoDown += GoDown;
        }

        #region Receive by world

        public void AddFloor(int level, FloorTool floorTool)
        {
            if (!_floors.ContainsKey(level))
            {
                _floors[level] = new Floor(level, this, floorTool);
                
                foreach (var layer in floorTool.GetChildren())
                {
                    if (layer is TileMapLayer obj)
                    {
                        AddObjectToFloor(level, obj);
                    } 
                }
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
            }

            obj.FixInvalidTiles();
        }

        public void AddPlayerToFloor(int level, Node2D player)
        {
            if (player.IsInsideTree())
            {
                foreach (var floor in _floors.Values)
                {
                    if (floor.FloorTool.Players.GetNodeOrNull(player.Name.ToString()) != null)
                    {
                        floor.FloorTool.Players.RemoveChild(player);
                        break;
                    }
                }
            }

            if (_floors.TryGetValue(level, out var floor1))
            {
                floor1.FloorTool.Players.AddChild(player);
            }
        }

        private void ChangeFloor(int level)
        {
            if (!_floors.ContainsKey(level)) return;

            CurrentFloorLevel = level;
            UpdateVisibleLayers();
        }

        public void HideFloorsAbove(Vector2I position)
        {
            _playerPosition = position; // --> Armazenar o ultimo lugar que o player pisou no mapa

            var playerUnderStructure = false;

            var floorsToCheck = new HashSet<int>();

            //Adicionar todos os pisos visíveis pelo piso adicionado manualmente para se tornar visivel pelo VisibleFloors
            foreach (var floor in _floors.Values)
            {
                foreach (var visibleFloor in floor.FloorTool.VisibleFloors)
                {
                    floorsToCheck.Add(visibleFloor);
                }
            }

            // Adicionar o piso atual e os pisos dentro do alcance de visibilidade padrão
            floorsToCheck.Add(CurrentFloorLevel);
            foreach (var floor in _floors.Values)
            {
                if (IsWithinVisibilityRange(floor.Level, floor.FloorTool.CustomRangeVisible))
                {
                    floorsToCheck.Add(floor.Level);
                }
            }

            RemoveUnderStructure(floorsToCheck);

            // Atualizar a visibilidade dos pisos
            foreach (var floor in _floors.Values)
            {
                bool withinRange = floorsToCheck.Contains(floor.Level);
                UpdateFloorObjectsVisibility(floor, withinRange);
                UpdatePlayersVisibility(floor, withinRange);
            }
        }

        private void UpdatePlayersVisibility(Floor floor, bool withinRange)
        {
            floor.FloorTool.Players.Visible = withinRange;
        }

        private void RemoveUnderStructure(HashSet<int> floorsToCheck)
        {
            if (floorsToCheck.Count == 0) return;

            if (_playerPosition == Vector2I.Zero) return;

            var playerUnderStructure = false;

            foreach (var floor in _floors.Values)
            {
                if (floor.Level <= CurrentFloorLevel) continue;

                if (!floorsToCheck.Contains(floor.Level)) continue;

                foreach (var layer in floor.Objects)
                {
                    if (layer.Name == "AttributesLayer") continue;

                    var currentTileData = layer.GetCellTileData(_playerPosition);

                    if (currentTileData != null)
                    {
                        playerUnderStructure = true;
                    }

                    if (playerUnderStructure)
                    {
                        if (floorsToCheck.Contains(floor.Level))
                        {
                            floorsToCheck.Remove(floor.Level);
                        }
                    }
                    else
                    {
                        if (!layer.Visible)
                        {
                            floorsToCheck.Add(floor.Level);
                        }
                    }
                }
            }
        }

        #endregion

        #region Update Visible Objects

        public void UpdateVisibleLayers()
        {
            var floorsToCheck = new HashSet<int>();

            //Adicionar todos os pisos visíveis pelo piso adicionado manualmente para se tornar visivel pelo VisibleFloors
            foreach (var floor in _floors.Values)
            {
                foreach (var visibleFloor in floor.FloorTool.VisibleFloors)
                {
                    floorsToCheck.Add(visibleFloor);
                }
            }

            // Adicionar o piso atual e os pisos dentro do alcance de visibilidade padrão
            floorsToCheck.Add(CurrentFloorLevel);
            foreach (var floor in _floors.Values)
            {
                if (IsWithinVisibilityRange(floor.Level, floor.FloorTool.CustomRangeVisible))
                {
                    floorsToCheck.Add(floor.Level);
                }
            }

            RemoveUnderStructure(floorsToCheck);

            // Atualizar a visibilidade dos pisos
            foreach (var floor in _floors.Values)
            {
                bool withinRange = floorsToCheck.Contains(floor.Level);
                UpdateFloorObjectsVisibility(floor, withinRange);
                UpdateFloorDataLayer(floor);
                UpdateFloorChangeEvent(floor);
                UpdatePlayersVisibility(floor, withinRange);
            }
        }

        private bool IsWithinVisibilityRange(int floorLevel, int customRange)
        {
            return Mathf.Abs(floorLevel - CurrentFloorLevel) <= customRange;
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

        private void GoUp()
        {
            ChangeFloor(CurrentFloorLevel + 1);
        }

        private void GoDown()
        {
            ChangeFloor(CurrentFloorLevel - 1);
        }

        #endregion

        public void _Free()
        {
            FloorGoUp -= GoUp;
            FloorGoDown -= GoDown;
            base.Free();
        }
    }
}