using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450;
    private const int _PlayAreaEdgeLineWidth = 12;
    private const int _PaddleWidth = 8, _PaddleHeight = 124;
    private const float _PaddleSpeed = 240;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture, _paddleTexture;

    private Ball _ball;

    private Paddle _paddleRight, _paddleLeft;

    // C# properties are the "getters and setters" for C#
    // They are used to expose data in a controlled way.
    // PlayAreaBoundingBox is a "read only" property (there is no setter)
    internal Rectangle PlayAreaBoundingBox
    {
        get
        {
            return new Rectangle(0, _PlayAreaEdgeLineWidth, _WindowWidth, _WindowHeight - (2 * _PlayAreaEdgeLineWidth));
        }
    }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ball = new Ball();
        _ball.Initialize(new Vector2(150, 195), new Vector2(21, 21), new Vector2(-1, -1), 60, PlayAreaBoundingBox);

        _paddleRight = new Paddle();
        _paddleRight.Initialize(new Vector2(690, 198), new Vector2(8, 124), 240, PlayAreaBoundingBox);

        _paddleLeft = new Paddle();
        _paddleLeft.Initialize(new Vector2(54, 198), new Vector2(8, 124), 240, PlayAreaBoundingBox);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ball.LoadContent(this.Content);
        _paddleRight.LoadContent(Content);
        _paddleLeft.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _ball.Update(gameTime);

        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Up)) 
            _paddleRight.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.Down))
            _paddleRight.Direction = new Vector2(0, 1);
        else
            _paddleRight.Direction = Vector2.Zero;

        _paddleRight.Update(gameTime);

        if(kbState.IsKeyDown(Keys.W)) 
            _paddleLeft.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.S))
            _paddleLeft.Direction = new Vector2(0, 1);
        else
            _paddleLeft.Direction = Vector2.Zero;
        _paddleLeft.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

        _ball.Draw(_spriteBatch);
        _paddleRight.Draw(_spriteBatch);
        _paddleLeft.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
