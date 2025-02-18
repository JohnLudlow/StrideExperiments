using FourXGame.Utilities.Wrappers.Core.Map;

namespace FourXGame.Core.Map.TerrainGeneration;
public enum TerrainType {
    None,
    Water,
    Coast,
    Plain,
    Mountain,
}

public record TerrainRange(float MinElevation, float MaxElevation, TerrainType TerrainType);

public class TerrainGenerator
{
    private IFastNoiseLite _noise;
    private readonly IEnumerable<TerrainRange> _terrainRanges;

    public TerrainGenerator(IEnumerable<TerrainRange> terrainRanges, IFastNoiseLite fastNoiseLite)
    {
        _noise = fastNoiseLite;
        _terrainRanges = terrainRanges;
    }

    public TerrainGenerator(IEnumerable<TerrainRange> terrainRanges)
    {
        _noise = new FastNoiseLiteWrapper();
        _terrainRanges = terrainRanges;
    }

    public TerrainType[,] Generate(int mapSizeX, int mapSizeY)
    {
        var generatedTerrain = new TerrainType[mapSizeX, mapSizeY];

        for (var x = 0; x < mapSizeX; x++)
        {
            for (var y = 0; y < mapSizeY; y++)
            {
                var noiseValue = _noise.GetNoise(x, y);
                generatedTerrain[x, y] = _terrainRanges.FirstOrDefault(
                    range => noiseValue >= range.MinElevation &&
                             noiseValue <= range.MaxElevation
                )?.TerrainType ?? TerrainType.None;
            }
        }

        return generatedTerrain;
    }
}
