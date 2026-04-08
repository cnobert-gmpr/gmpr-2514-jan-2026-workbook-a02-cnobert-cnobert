using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class CannonBall : Projectile
{
    private Texture2D _texture;

    private List<Vector2> _trailPositions;
    private float _trailTimer;
    private const float _TrailSpawnInterval = 0.1f;
    private const int _MaxTrailPositions = 8;
    
    //"override" means "I'm hiding the parent method"
    internal override void Initialize(float speed, Rectangle gameBoundingBox)
    {
        base.Initialize(speed, gameBoundingBox);

        _dimensions = new Point(4, 4);
        _trailPositions = new List<Vector2>();
        _trailTimer = 0;
    }
    
    internal override void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("CannonBall");
    }
    
    internal override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * dt;
                _trailTimer += dt;
                if(_trailTimer >= _TrailSpawnInterval)
                {
                    _trailTimer = 0;
                    _trailPositions.Insert(0, _position);
                    if(_trailPositions.Count > _MaxTrailPositions)
                    {
                        // remove the last trail position in the list
                        _trailPositions.RemoveAt(_trailPositions.Count - 1);
                    }
                }

                if(!BoundingBox.Intersects(_gameBoundingBox))
                {
                    //I'm not on screen anymore
                    _state = State.NotFlying;
                    _trailPositions.Clear();
                }
                break;
            case State.NotFlying:
                break;
        }
    }
    
    internal override void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                spriteBatch.Draw(_texture, _position, Color.White);
                DrawTrail(spriteBatch);
                break;
            case State.NotFlying:
                break;
        }
        
    }

    private void DrawTrail(SpriteBatch spriteBatch)
    {
        for(int c = 0; c < _trailPositions.Count; c++)
        {
            // c = 0                    1 - 1/13 = 12/13
            // c = 1                    1 - 2/13 = 11/13
            // c = 2                    1 - 3/13 = 10/13
            // alpha gets closer to 1 as "c" increases
            float alpha = 1f - ((float)(c + 1) / (_trailPositions.Count + 1));
            // c = 0        1 - (0 * 0.1f) = 1
            // c = 1        1 - (1 * 0.1f) = 0.9f
            // c = 2        1 - (2 * 0.1) = 0.8f
            float scale = 1f - (c * 0.1f);
            if(scale < 0.2f)
            {
                scale = 0.2f;
            }
            Vector2 drawPosition = _trailPositions[c];
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Vector2 centeredPosition = drawPosition + new Vector2(_texture.Width / 2f, _texture.Height / 2f);
            spriteBatch.Draw
                (
                    _texture, centeredPosition, null, Color.Gray * (alpha * 0.5f), 
                    0f, origin, scale, SpriteEffects.None, 0f
                );
        }
    }

    internal override bool ProcessCollision(Rectangle otherBoundingBox)
    {
        if(base.ProcessCollision(otherBoundingBox))
        {
            _trailPositions.Clear();
            return true;
        }
        return false;
    }

}