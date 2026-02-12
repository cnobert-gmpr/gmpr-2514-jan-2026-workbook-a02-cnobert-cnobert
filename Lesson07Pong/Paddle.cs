using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07Pong;

public class Paddle
{
    private Texture2D _texture;
    private Vector2 _position, _dimensions, _direction;
    private float _speed;

    private Rectangle _playAreaBoundingBox;

    internal Vector2 Direction 
    { 
        set => _direction = value; 
    }

    internal void Initialize (Vector2 position, Vector2 dimensions, float speed, Rectangle playAreaBoundingBox)
    {
        _position = position;
        _dimensions = dimensions;
        _direction = Vector2.Zero;
        _speed = speed;
        _playAreaBoundingBox = playAreaBoundingBox;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _position += _direction * _speed * dt;

        if(_position.Y <= _playAreaBoundingBox.Top)
        {
            _position.Y = _playAreaBoundingBox.Top;
        }
        else if( (_position.Y + _dimensions.Y) >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _dimensions.Y;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle paddleRectangle = new Rectangle((int) _position.X, (int) _position.Y, (int) _dimensions.X, (int) _dimensions.Y);

        spriteBatch.Draw(_texture, paddleRectangle, Color.Azure);
    }
}