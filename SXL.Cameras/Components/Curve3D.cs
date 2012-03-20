using System;
using Microsoft.Xna.Framework;

namespace SXL.Cameras.Components
{
    class Curve3D
    {
        readonly Curve curveX = new Curve();
        readonly Curve curveY = new Curve();
        readonly Curve curveZ = new Curve();

        public Curve3D(CurveLoopType loopType)
        {
            curveX.PostLoop = loopType;
            curveY.PostLoop = loopType;
            curveZ.PostLoop = loopType;

            curveX.PreLoop = loopType;
            curveY.PreLoop = loopType;
            curveZ.PreLoop = loopType;
        }

        public void AddPoint(Vector3 point, float time)
        {
            curveX.Keys.Add(new CurveKey(time, point.X));
            curveY.Keys.Add(new CurveKey(time, point.Y));
            curveZ.Keys.Add(new CurveKey(time, point.Z));
        }

        public void SetTangents()
        {
            for (int i = 0; i < curveX.Keys.Count; i++)
            {
                int prevIndex = i - 1;
                if (prevIndex < 0) prevIndex = i;

                int nextIndex = i + 1;
                if (nextIndex == curveX.Keys.Count) nextIndex = i;

                CurveKey prev = curveX.Keys[prevIndex];
                CurveKey next = curveX.Keys[nextIndex];
                CurveKey current = curveX.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveX.Keys[i] = current;
                prev = curveY.Keys[prevIndex];
                next = curveY.Keys[nextIndex];
                current = curveY.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveY.Keys[i] = current;

                prev = curveZ.Keys[prevIndex];
                next = curveZ.Keys[nextIndex];
                current = curveZ.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveZ.Keys[i] = current;
            }
        }
        static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur,
                                       ref CurveKey next)
        {
            float dt = next.Position - prev.Position;
            float dv = next.Value - prev.Value;
            if (Math.Abs(dv) < float.Epsilon)
            {
                cur.TangentIn = 0;
                cur.TangentOut = 0;
            }
            else
            {
                // The in and out tangents should be equal to the 
                // slope between the adjacent keys.
                cur.TangentIn = dv * (cur.Position - prev.Position) / dt;
                cur.TangentOut = dv * (next.Position - cur.Position) / dt;
            }
        }

        public Vector3 GetPointOnCurve(float time)
        {
            return new Vector3 { X = curveX.Evaluate(time), Y = curveY.Evaluate(time), Z = curveZ.Evaluate(time) };
        }
    }
}