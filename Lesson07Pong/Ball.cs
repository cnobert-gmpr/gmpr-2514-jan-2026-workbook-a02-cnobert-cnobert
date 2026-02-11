using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07Pong;

public class Ball
{
    private Texture2D _texture;
    private Vector2 _position, _dimensions, _direction;
    private float _speed;

    private Rectangle _playAreaBoundingBox;

    internal void Initialize (Vector2 position, Vector2 dimensions, Vector2 direction, float speed, Rectangle playAreaBoundingBox)
    {
        _position = position;
        _dimensions = dimensions;
        _direction = direction;
        _speed = speed;
        _playAreaBoundingBox = playAreaBoundingBox;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _position += _direction * _speed * dt;

        //bounce the ball off left and right sides
        if(_position.X <= _playAreaBoundingBox.Left || 
            _position.X + _dimensions.X >= _playAreaBoundingBox.Right)
        {
            _direction.X *= -1;
        }
        if(_position.Y <= _playAreaBoundingBox.Top || 
            _position.Y + _dimensions.Y >= _playAreaBoundingBox.Bottom)
        {
            _direction.Y *= -1;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle ballRectangle = new Rectangle((int) _position.X, (int) _position.Y, (int) _dimensions.X, (int) _dimensions.Y);

        spriteBatch.Draw(_texture, ballRectangle, Color.White);
    }
}