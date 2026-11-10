using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZanderGameCoreCS.Monogame.Graphics;

public class SpriteAnimationData
{
    public readonly Sprite[] Frames;
    public readonly Texture2D _texture;
    public readonly float FrameDuration;
    public readonly bool ShouldLoop;

    public SpriteAnimationData(Rectangle firstFrame, int numOfFrames, float frameDuration, bool shouldLoop, Texture2D texture)
    {
        Frames = new Sprite[numOfFrames];
        FrameDuration = frameDuration;
        ShouldLoop = shouldLoop;
        _texture = texture;

        int height = firstFrame.Height;
        int width = firstFrame.Width;
        int startX = firstFrame.X;
        int y = firstFrame.Y;

        for (int i = 0; i < Frames.Length; i++)
        {
            Frames[i] = new Sprite(startX + (width * i), y, width, height, _texture);
        }
    }
}

public struct SpriteAnimationInstance
{
    readonly SpriteAnimationData animation;
    float _currentFrameDuration = 0f;
    byte _currentFrame = 0;
    public bool IsPlaying = false;
    public bool IsFinished = false;

    public SpriteAnimationInstance(SpriteAnimationData anim)
    {
        animation = anim;
    }

    public readonly Sprite CurrentSprite() {
        return animation.Frames[_currentFrame];
    }

    public void Update(GameTime gt)
    {
        if (IsFinished) return;

        if (animation.Frames.Length == 0)
        {
            IsFinished = true;
            IsPlaying = false;
        }


        _currentFrameDuration += (float)gt.ElapsedGameTime.TotalSeconds;

        IsPlaying = true;

        while (_currentFrameDuration >= animation.FrameDuration)
        {
            _currentFrameDuration -= animation.FrameDuration;
            _currentFrame += 1;

            if (_currentFrame < animation.Frames.Length) return;

            // needs to decide to loop or not
            if (animation.ShouldLoop) _currentFrame = 0;
            else
            {
                _currentFrame -= 1;
                IsFinished = true;
                IsPlaying = false;
                break;
            }
        }
    }

    public readonly void Draw(SpriteBatch sb, Vector2 pos)
    {
        CurrentSprite().Draw(sb, pos);
    }

    public void Reset()
    {
        _currentFrame = 0;
        _currentFrameDuration = 0f;
        IsPlaying = false;
        IsFinished = false;
    }
}