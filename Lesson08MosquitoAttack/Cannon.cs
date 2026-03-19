using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Cannon
{
    private const int _NumCannonBalls = 10;

    private SimpleAnimation _animation;
    private Vector2 _position, _direction;
    private Point _dimensions;
    private float _speed;

    private Rectangle _gameBoundingBox;

    private CannonBall[] _cBalls;

    internal Vector2 Direction
    {
        set
        {
            // cannon should only move horizontally
            value.Y = 0;
            _direction = value;
            if(_direction.X < 0)
                _animation.Reverse = true;
            else
                _animation.Reverse = false;
        }
    }

    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)_animation.FrameDimensions.X,
                (int)_animation.FrameDimensions.Y
            );
        }
    }

    internal void Initialize(Vector2 position, float speed, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;

        _cBalls = new CannonBall[_NumCannonBalls];
        for(int c = 0; c < _NumCannonBalls; c++)
        {
            _cBalls[c] = new CannonBall();
            _cBalls[c].Initialize(50, _gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Cannon");
        _dimensions = new Point(texture.Width / 4, texture.Height);
        _animation = new SimpleAnimation(texture, _dimensions.X, _dimensions.Y, 4, 2);

        foreach(CannonBall c in _cBalls)
            c.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _direction * _speed * dt;
        if(_direction != Vector2.Zero)
            _animation.Update(gameTime);
        foreach(CannonBall c in _cBalls)
            c.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        if(_animation != null)
            _animation.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(CannonBall c in _cBalls)
            c.Draw(spriteBatch);
    }
    internal void Shoot()
    {
        foreach(CannonBall c in _cBalls)
        {
            if(c.Launchable)
            {
                float cannonBallPositionY = BoundingBox.Top - c.BoundingBox.Height;
                float cannonBallPositionX = BoundingBox.Center.X - c.BoundingBox.Width / 2;
                Vector2 cannonBallPosition = new Vector2(cannonBallPositionX, cannonBallPositionY);
                c.Launch(cannonBallPosition, new Vector2(0, -1));
                return; //break;
            }
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        foreach(CannonBall c in _cBalls)
        {
            if(c.ProcessCollision(boundingBox))
            {
                // game rule: only one cannonBall can hit something each call to update
                return true;
            }
        }
        return false;
    }
}