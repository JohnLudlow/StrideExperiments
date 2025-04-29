using Stride.Engine;
using Stride.CommunityToolkit.Bepu;
using Stride.CommunityToolkit.Rendering.ProceduralModels;
using Stride.CommunityToolkit.Engine;

namespace StrideExperiments.OcclusionTest.UnitTests;

public class ObjectInView
{
    [Fact]
    public void IsNotOccluded()
    {        
        RunGameTest(async (game, scene) => {
            scene.Entities.Add(game.Create3DPrimitive(PrimitiveModelType.Cube));

            Assert.NotEmpty(scene.Entities);

            var mainCamera = scene.GetCamera();
            
            Assert.NotNull(mainCamera);
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
