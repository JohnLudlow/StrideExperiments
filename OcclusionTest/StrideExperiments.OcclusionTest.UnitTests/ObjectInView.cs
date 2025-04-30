using Stride.Engine;
using Stride.Core.Mathematics;
using Stride.Rendering;
using Stride.Graphics.GeometricPrimitives;
using Stride.Extensions;

namespace StrideExperiments.OcclusionTest.UnitTests;

public class ObjectInView
{
    [Fact]
    public void IsNotOccluded()
    {        
        RunGameTest(async (game, scene) =>
        {
            var entity = new Entity();
            var model = new Model();

            model.Meshes.Add
            (
                new Mesh 
                { 
                    Draw = GeometricPrimitive.Cube.New(game.GraphicsDevice).ToMeshDraw() 
                }
            );

            entity.GetOrCreate<ModelComponent>().Model = model;
            entity.Transform.Position = new Vector3(1, 1, 1);
            scene.Entities.Add(entity);
        });
    }

    private static void RunGameTest(Func<Game, Scene, Task> asyncFunction)
    {
        using var game = new Game();
        
        // Fixed time step to reduce framerate discrepancies
        game.IsFixedTimeStep = true;
        game.IsDrawDesynchronized = false;
        game.TargetElapsedTime = TimeSpan.FromTicks(10000000 / 60); // 60hz, 60fps
        
        game.Script.AddTask(async () =>
        {
            await asyncFunction(game, game.SceneSystem.SceneInstance.RootScene);
            game.Exit();
        });
        game.Run();
    }
}
