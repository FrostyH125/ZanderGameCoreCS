using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZanderGameCoreCS.Monogame.Graphics;

public readonly struct Sprite
{
    public readonly Rectangle SourceRect;
    readonly Texture2D? _texture;

    public Sprite(int x, int y, int width, int height, Texture2D texture) {
        SourceRect = new(x, y, width, height);
        _texture = texture;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_texture, position, SourceRect, Color.White);
    }
}