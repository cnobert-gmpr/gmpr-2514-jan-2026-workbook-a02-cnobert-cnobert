using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson03Loops;

public class LoopGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _pixel;
    private Vector2 _position, _dimensions;

    private int _count;
    private float _spacing;

    private Rectangle[] _rectangles;

    public LoopGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _position = new Vector2(50, 200);
        _dimensions = new Vector2(60, 40);
        _count = 6;
        _spacing = 10;

        _rectangles = new Rectangle[_count]; 

        for(int c = 0; c < _count; c++)
        {
            float x = _position.X + c * (_dimensions.X + _spacing);

            _rectangles[c] = new Rectangle((int)x, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);
        }


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new [] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
        //_position.X += 60 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        foreach(Rectangle r in _rectangles)
        {
            _spriteBatch.Draw(_pixel, r, Color.Aquamarine);
        }
        

        

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
