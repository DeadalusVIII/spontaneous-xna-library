﻿using Microsoft.Xna.Framework;

namespace SXL.Cameras.Components
{
    class Trajectory
    {
        private Curve3D positionCurve;
        private Curve3D targetCurve;

        //variable to track the amount of time that has passed since the camera started moving
        double time;

        public Trajectory(CurveLoopType loopType)
        {
            time = 0;

            positionCurve = new Curve3D(loopType);
            targetCurve = new Curve3D(loopType);
        }

        public Curve3D PositionCurve
        {
            get { return positionCurve; }
            set { positionCurve = value; }
        }

        public Curve3D TargetCurve
        {
            get { return targetCurve; }
            set { targetCurve = value; }
        }

        public void AddStep(Vector3 positionPoint, Vector3 targetPoint, float newTime)
        {
            PositionCurve.AddPoint(positionPoint, newTime);
            TargetCurve.AddPoint(targetPoint, newTime);
        }
        
        public void BuildTrajectory()
        {
            PositionCurve.SetTangents();
            TargetCurve.SetTangents();
        }

        public void Update(Camera camera, double elapsedTime)
        {
            camera.Position = PositionCurve.GetPointOnCurve((float)time);
            camera.Target = TargetCurve.GetPointOnCurve((float)time);

            time += elapsedTime;
        }

    }
}