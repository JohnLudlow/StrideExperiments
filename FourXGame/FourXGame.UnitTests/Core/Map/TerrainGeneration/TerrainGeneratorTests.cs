using FourXGame.Core.Map.TerrainGeneration;
using FourXGame.Utilities.Wrappers.Core.Map;
using Moq;

namespace FourXGame.UnitTests.Core.Map.TerrainGeneration;

public class TerrainGeneratorTests
{
    public static TheoryData<float[,], TerrainType[,]> TerrainData
    => new()
    {
        {
            new float[,] {
                { 0f, 0f, 0f, 0f },
                { 0f, 0f, 0f, 0f },
                { 0f, 0f, 0f, 0f },
                { 0f, 0f, 0f, 0f },
            }, 

             new TerrainType [,] {
                {TerrainType.Water, TerrainType.Water, TerrainType.Water, TerrainType.Water},
                {TerrainType.Water, TerrainType.Water, TerrainType.Water, TerrainType.Water},
                {TerrainType.Water, TerrainType.Water, TerrainType.Water, TerrainType.Water},
                {TerrainType.Water, TerrainType.Water, TerrainType.Water, TerrainType.Water}
            }
        },

        {
            new float[,] {
                { 0.35f, 0.35f, 0.35f, 0.35f },
                { 0.35f, 0.35f, 0.35f, 0.35f },
                { 0.35f, 0.35f, 0.35f, 0.35f },
                { 0.35f, 0.35f, 0.35f, 0.35f },
            }, 

             new TerrainType [,] {
                {TerrainType.Coast, TerrainType.Coast, TerrainType.Coast, TerrainType.Coast},
                {TerrainType.Coast, TerrainType.Coast, TerrainType.Coast, TerrainType.Coast},
                {TerrainType.Coast, TerrainType.Coast, TerrainType.Coast, TerrainType.Coast},
                {TerrainType.Coast, TerrainType.Coast, TerrainType.Coast, TerrainType.Coast}
            }
        },

        {
            new float[,] {
                { 0.1f, 0.4f, 0.6f, 0.9f },
                { 0.1f, 0.4f, 0.6f, 0.9f },
                { 0.1f, 0.4f, 0.6f, 0.9f },
                { 0.1f, 0.4f, 0.6f, 0.9f },
            }, 

             new TerrainType [,] {
                {TerrainType.Water, TerrainType.Coast, TerrainType.Plain, TerrainType.Mountain},
                {TerrainType.Water, TerrainType.Coast, TerrainType.Plain, TerrainType.Mountain},
                {TerrainType.Water, TerrainType.Coast, TerrainType.Plain, TerrainType.Mountain},
                {TerrainType.Water, TerrainType.Coast, TerrainType.Plain, TerrainType.Mountain}
            }
        }
    };

    [Fact]
    public void TerrainGenerator_ctor_CreatesTerrainGenerator()
    {
        var terrainGenerator = new TerrainGenerator([]);
        Assert.IsType<TerrainGenerator>(terrainGenerator);
    }

    [Theory, MemberData(nameof(TerrainData))]
    public void TerrainGenerator_GenerateTerrain_GeneratesTerrain(float[,] noiseDataInput, TerrainType[,] expectedTerrainDataOutput)
    {        
        var fastNoiseLiteMock = new Mock<IFastNoiseLite>();
        
        for (var x = 0; x < noiseDataInput.GetLength(0); x++)
        {
            for (var y = 0; y < noiseDataInput.GetLength(1); y++)
            {
                fastNoiseLiteMock
                    .Setup(n => n.GetNoise(x, y))
                    .Returns(noiseDataInput[x, y]);
            }
        }

        var terrainGenerator = new TerrainGenerator(
            [
                new TerrainRange(0  ,  .3f, TerrainType.Water),
                new TerrainRange(.3f,  .4f, TerrainType.Coast),
                new TerrainRange(.4f,  .8f, TerrainType.Plain),
                new TerrainRange(.8f,    1, TerrainType.Mountain),
            ],
            fastNoiseLiteMock.Object
        );

        var terrain = terrainGenerator.Generate(noiseDataInput.GetLength(0), noiseDataInput.GetLength(1));

        Assert.NotEmpty(terrain);
        Assert.Equal(expectedTerrainDataOutput, terrain);
    }
}
