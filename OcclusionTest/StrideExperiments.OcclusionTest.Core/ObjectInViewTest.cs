using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Extensions;
using Stride.Graphics.GeometricPrimitives;
using Stride.Rendering;

namespace StrideExperiments.OcclusionTest.UnitTests;

public class ObjectInViewTest
{
    public static void IsNotInView(Game game, Scene scene)
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
    }
}
