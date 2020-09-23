using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DudesInSpace
{
    class Graphic2D
    {
        // Class Variables
        private Vector2 _pos;
        protected Texture2D _txr;

        // Class Properties
        public Vector2 Position
        {
            get
            {
                return _pos;
            }

            set 
            {
                _pos = value;
            }
        }

        // Class Constructors
        public Graphic2D(Texture2D texture2D, int xPos = 0, int yPos = 0)
        {

            _pos = new Vector2(xPos, yPos);
            _txr = texture2D;
        }

        // Class Methods
        public virtual void DrawMe(SpriteBatch sBatch)
        {
            sBatch.Draw(_txr, _pos, Color.White);
        }

    }

    class Sprite2D : Graphic2D
    {
        private float _rotation;
        private float _rotSpeed;
        private Vector2 _rotCenter;

        private Color _tint;

        public Sprite2D(Texture2D tex, int xPos, int yPos, Color tint, float rot, float rotSpeed) : base (tex, xPos, yPos)
        {
            _rotation = rot;
            _rotSpeed = rotSpeed;
            _rotCenter = tex.Bounds.Center.ToVector2();

            _tint = tint;
        }

        public override void DrawMe(SpriteBatch sBatch)
        {
            _rotation += _rotSpeed;

            sBatch.Draw(_txr, Position, null, _tint, _rotation, _rotCenter, 1, SpriteEffects.None, 0);
        }
    }

    class SpaceDude
    {
        private Sprite2D _dudeBody;
        private Sprite2D _dudeVisor;

        private Vector2 _velocity;

        public SpaceDude(Texture2D bodyTex, Texture2D visorTex, Color tint, int xPos, int yPos, Vector2 vel, float rot = 0, float rotSpeed = 0)
        {
            _dudeVisor = new Sprite2D(visorTex, xPos, yPos, Color.White, rot, rotSpeed);
            _dudeBody = new Sprite2D(bodyTex, xPos, yPos, tint, rot, rotSpeed);

            _velocity = vel;
        }

        public void UpdateMe(Rectangle bounds)
        {
            _dudeBody.Position += _velocity;
            _dudeVisor.Position += _velocity;

            if (_dudeBody.Position.X < bounds.Left || _dudeBody.Position.X > bounds.Right)
                _velocity.X *= -1;

            if (_dudeBody.Position.Y < bounds.Top || _dudeBody.Position.Y > bounds.Bottom)
                _velocity.Y *= -1;
        }

        public void DrawMe(SpriteBatch sb)
        {
            _dudeBody.DrawMe(sb);
            _dudeVisor.DrawMe(sb);
        }
    }
}
