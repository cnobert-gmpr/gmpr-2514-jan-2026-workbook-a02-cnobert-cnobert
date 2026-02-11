using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07Pong;

public class Ball
{
    private Texture2D _texture;
    private Vector2 _position, _dimensions, _direction;
    private float _speed;

    internal void Initialize (Vector2 position, Vector2 dimensions, Vector2 direction, float speed)
    {
        _position = position;
        _dimensions = dimensions;
        _direction = direction;
        _speed = speed;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle ballRectangle = new Rectangle((int) _position.X, (int) _position.Y, (int) _dimensions.X, (int) _dimensions.Y);

        spriteBatch.Draw(_texture, ballRectangle, Color.White);
    }
}