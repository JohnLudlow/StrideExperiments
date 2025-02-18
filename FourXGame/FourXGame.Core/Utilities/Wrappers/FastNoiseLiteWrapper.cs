using System;

namespace FourXGame.Utilities.Wrappers.Core.Map;

public interface IFastNoiseLite {
    /// <summary>
    /// 2D noise at given position using current settings
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    float GetNoise(float x, float y);
}

public class FastNoiseLiteWrapper : IFastNoiseLite
{
    private readonly FastNoiseLite.FastNoiseLite _fastNoiseLite;

    public FastNoiseLiteWrapper(FastNoiseLite.FastNoiseLite fastNoiseLite)
    {
        _fastNoiseLite = fastNoiseLite;
    }

    public FastNoiseLiteWrapper()
    {
        _fastNoiseLite = new FastNoiseLite.FastNoiseLite();
    }

    /// <inheritdoc/>
    public float GetNoise(float x, float y)
    {
        return _fastNoiseLite.GetNoise(x, y);
    }
}
