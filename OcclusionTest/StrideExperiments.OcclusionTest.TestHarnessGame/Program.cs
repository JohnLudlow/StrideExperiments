using Stride.CommunityToolkit.Bepu;
using Stride.CommunityToolkit.Engine;
using Stride.CommunityToolkit.Rendering.ProceduralModels;
using Stride.CommunityToolkit.Skyboxes;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Games;
using Stride.Graphics;
using Stride.Input;

var game = new Game();

game.Run(start: Start, update: Update);

void Start (Scene rootScene)
{
    game.SetupBase3DScene();
    game.AddSkybox();
    game.Add3DGround();

    var entity = game.Create3DPrimitive(PrimitiveModelType.Cube);
    entity.Name = "TestCube";
    entity.Transform.Position = new Vector3(1, 1, 1);

    rootScene.Entities.Add(entity);
}

void Update(Scene rootScene, GameTime gameTime)
{
    var camera = rootScene.GetCamera();
    if (camera == null) return;

    var backBuffer = game.GraphicsDevice.Presenter.BackBuffer;
    var viewport = new Viewport(0, 0, backBuffer.Width, backBuffer.Height);

    if (camera.Raycast(game.Input.Mouse.Position, 100, out var hitInfo))
    {
        if (hitInfo.Collidable.Entity.Name == "TestCube")
        {            
            game.DebugTextSystem.Print($"{hitInfo.Collidable.Entity.Name} is hovered", new Int2(100, 100)); 
        }
    }

}