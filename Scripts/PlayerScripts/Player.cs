using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;
using GodotFloorLevels.Scripts.WorldBase;
using GodotFloorLevels.Scripts.WorldBase.Floors;

namespace GodotFloorLevels.Scripts.PlayerScripts
{
    public partial class Player : CharacterBody2D
    {
        private Vector2I _gridSnapped = new Vector2I(32, 32);

        public Vector2I TargetPosition = new Vector2I();

        private bool _isMoving;

        private float _speed;

        private FloorManager _floorManager;

        public override void _Ready()
        {
            _speed = 0.5f;

            // Supondo que o FloorManager é um nó filho ou de alguma forma acessível
            _floorManager = GetParent().GetParent().GetParent<World>().FloorManager;
        }

        public override void _PhysicsProcess(double delta)
        {
            if (_isMoving)
                return;

            var direction = new Vector2I
            {
                X = (int)Input.GetActionStrength("ui_right") - (int)Input.GetActionStrength("ui_left"),
                Y = (int)Input.GetActionStrength("ui_down") - (int)Input.GetActionStrength("ui_up")
            };

            if (direction == Vector2I.Zero) return;

            if (ValidateNextPosition(direction * _gridSnapped))
                return;

            TargetPosition += direction * _gridSnapped;

            _isMoving = true;

            StartMovement();
        }

        private void StartMovement()
        {
            if (!_isMoving) return;

            this.TweenPosition(TargetPosition, _speed)
                .OnComplete(() => _isMoving = false)
                .Play();
        }

        private bool ValidateNextPosition(Vector2I position)
        {
            var posI = new Vector2I((int)Position.X, (int)Position.Y);
            
            var nextPosition = (posI + position) / _gridSnapped;
            
            var signal = _floorManager.EmitSignal(FloorManager.SignalName.CheckFloorChange, nextPosition, this);
            
            GD.Print("Signal: " + signal);
            
            return false;
        }
        
        public void SetPlayerPosition(Vector2I position)
        {
            TargetPosition = position * _gridSnapped;
            Position = TargetPosition;
        }
    }
}