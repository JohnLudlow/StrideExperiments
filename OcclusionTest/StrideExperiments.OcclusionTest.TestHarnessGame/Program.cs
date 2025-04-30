using Stride.CommunityToolkit.Bepu;
using Stride.CommunityToolkit.Engine;
using Stride.CommunityToolkit.Skyboxes;
using Stride.Engine;
using StrideExperiments.OcclusionTest.UnitTests;

var game = new Game();

game.Run(start: rootScene =>
{
    game.SetupBase3DScene();
    game.AddSkybox();
    game.Add3DGround();

    ObjectInViewTest.IsNotInView(game, rootScene);
});
