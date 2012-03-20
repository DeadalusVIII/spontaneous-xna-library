using Microsoft.Xna.Framework;

namespace SXL.Cameras.Components
{
    class TrajectoryComponent : CameraComponent
    {
        private Trajectory trajectory;

        public void AddTrajectory(Trajectory newTrajectory)
        {
            trajectory = newTrajectory;
        }

        public override void Update(GameTime gameTime)
        {
            trajectory.Update(Camera, gameTime.ElapsedGameTime.TotalMilliseconds);
        }
    }
}
