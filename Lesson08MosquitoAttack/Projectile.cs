using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Projectile
{
    // the "private" access modifier hides from children
    // so, whatever we want the children to see, we will designate as "protected"
    protected Vector2 _position, _direction;
    protected float _speed;

    protected Rectangle _gameBoundingBox;

    protected enum State { Flying, NotFlying}
    protected State _state = State.NotFlying;

    internal bool Launchable { get => _state == State.NotFlying; }

     //"virtual" means "my children MAY override this method, but they don't have to"
    internal virtual void Initialize(float speed, Rectangle gameBoundingBox)
    {
        _position = Vector2.Zero;
        _direction = Vector2.Zero;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
    }
}