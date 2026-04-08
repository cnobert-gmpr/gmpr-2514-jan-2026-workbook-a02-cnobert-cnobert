using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Cannon : Actor
{
    private const int _NumProjectiles = 5;

    private Rectangle _gameBoundingBox;

    private Projectile[] _projectiles;

    internal Vector2 Direction
    {
        set
        {
            // cannon should only move horizontally
            value.Y = 0;
            _direction = value;
            if(_direction.X < 0)
                _animationAlive.Reverse = true;
            else
                _animationAlive.Reverse = false;
        }
    }

    internal void Initialize(Vector2 position, float speed, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;

        _projectiles = new Projectile[_NumProjectiles];
        _projectiles[0] = new CannonBall();
        _projectiles[1] = new FireBall();
        _projectiles[2] = new CannonBall();
        _projectiles[3] = new FireBall();
        _projectiles[4] = new CannonBall();

        for(int c = 0; c < _NumProjectiles; c++)
        {
            _projectiles[c].Initialize(50, _gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Cannon");
        Point dimensions = new Point(texture.Width / 4, texture.Height);
        _animationAlive = new SimpleAnimation(texture, dimensions.X, dimensions.Y, 4, 2);

        foreach(Projectile p in _projectiles)
            p.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _direction * _speed * dt;
        if(_direction != Vector2.Zero)
            _animationAlive.Update(gameTime);
        foreach(Projectile p in _projectiles)
            p.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        if(_animationAlive != null)
            _animationAlive.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(Projectile p in _projectiles)
            p.Draw(spriteBatch);
    }
    internal void Shoot()
    {
        foreach(Projectile p in _projectiles)
        {
            if(p.Launchable)
            {
                float projectilePositionY = BoundingBox.Top - p.BoundingBox.Height;
                float projectilePositionX = BoundingBox.Center.X - p.BoundingBox.Width / 2;
                Vector2 projectilePosition = new Vector2(projectilePositionX, projectilePositionY);
                p.Launch(projectilePosition, new Vector2(0, -1));
                return; //break;
            }
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        foreach(Projectile p in _projectiles)
        {
            if(p.ProcessCollision(boundingBox))
            {
                // game rule: only one cannonBall can hit something each call to update
                return true;
            }
        }
        return false;
    }
}