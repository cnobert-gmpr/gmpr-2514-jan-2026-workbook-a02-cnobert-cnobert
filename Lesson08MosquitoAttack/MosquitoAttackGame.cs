using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;

namespace Lesson08MosquitoAttack;

public class MosquitoAttackGame : Game
{
    private const int _WindowWidth = 550;
    private const int _WindowHeight = 400;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SpriteFont _font;
    private string _message = "";
    private KeyboardState _kbCurrentState, _kbPreviousState;
    private enum GameState { Playing, Paused, Over }
    private GameState _gameState;

    Cannon _cannon;
    Mosquito _mosquito;
    
    private Rectangle BoundingBox
    {
        get { return new Rectangle(0, 0, _WindowWidth, _WindowHeight); }
    }

    public MosquitoAttackGame()
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

        _cannon = new Cannon();
        _cannon.Initialize(new Vector2(50, 325), 150);

        _mosquito = new Mosquito();
        _mosquito.Initialize(new Vector2(50, 20), 200, new Vector2(-1, 0), BoundingBox);

        _gameState = GameState.Playing;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("Background");

        _cannon.LoadContent(Content);
        _mosquito.LoadContent(Content);

        #region students, don't look here
        //MacOS ONLY
         byte[] fontBytes = File.ReadAllBytes("Content/Tahoma.ttf");
        _font = TtfFontBaker.Bake(fontBytes, 30, 1024, 1024, new[] { CharacterRange.BasicLatin }).CreateSpriteFont(GraphicsDevice);
        #endregion
    }

    protected override void Update(GameTime gameTime)
    {
        _kbCurrentState = Keyboard.GetState();
        switch(_gameState)
        {
            case GameState.Playing:
                #region keyboard input
                if(_kbCurrentState.IsKeyDown(Keys.A)) 
                    _cannon.Direction = new Vector2(-1, 0);
                else if(_kbCurrentState.IsKeyDown(Keys.D))
                    _cannon.Direction = new Vector2(1, 0);
                else
                    _cannon.Direction = Vector2.Zero;

                if(Pressed(Keys.P))
                {
                    _gameState = GameState.Paused;
                    _message = "Game Paused, press P to start playing again.";
                }
                #endregion
                _cannon.Update(gameTime);
                _mosquito.Update(gameTime);
                break;
            case GameState.Paused:
                if(Pressed(Keys.P))
                {
                    _gameState = GameState.Playing;
                    _message = "";
                }
                break;
            case GameState.Over:
                break;
        }
        _kbPreviousState = _kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        switch(_gameState)
        {
            case GameState.Playing:
                _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
                _cannon.Draw(_spriteBatch);
                _mosquito.Draw(_spriteBatch);
                break;
            case GameState.Paused:
                _spriteBatch.Draw(_background, Vector2.Zero, Color.Silver);
                _cannon.Draw(_spriteBatch);
                _spriteBatch.DrawString(_font, _message, new Vector2(10, 135), Color.White);
                _mosquito.Draw(_spriteBatch);
                break;
            case GameState.Over:
                break;
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
    private bool Pressed(Keys key)
    {
        return _kbCurrentState.IsKeyDown(key) && _kbPreviousState.IsKeyUp(key);
    }
}
