using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Cannon
{
    private SimpleAnimation _animation;
    private Vector2 _position, _direction;
    private Point _dimensions;
    private float _speed;

    private Rectangle _gameBoundingBox;

    private CannonBall _cBall;

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

    internal void Initialize(Vector2 position, float speed, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
        _cBall = new CannonBall();
        _cBall.Initialize(50, _gameBoundingBox);

    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Cannon");
        _dimensions = new Point(texture.Width / 4, texture.Height);
        _animation = new SimpleAnimation(texture, _dimensions.X, _dimensions.Y, 4, 2);

        _cBall.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _direction * _speed * dt;
        if(_direction != Vector2.Zero)
            _animation.Update(gameTime);
        _cBall.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        if(_animation != null)
            _animation.Draw(spriteBatch, _position, SpriteEffects.None);
        _cBall.Draw(spriteBatch);
    }
    internal void Shoot()
    {
        _cBall.Shoot(_position, new Vector2(0, -1));
    }
}