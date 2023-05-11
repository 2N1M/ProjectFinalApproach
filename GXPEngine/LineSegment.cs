using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using GXPEngine.Core;
using GXPEngine.OpenGL;
using GXPEngine.PhysicsEngine;

namespace GXPEngine
{
    public class LineSegment : Entity
    {
        EasyDraw line;

        public Vec2 start;
        public Vec2 end;

        public Color color = Color.White;
        public uint lineWidth = 1;

        private Vec2 _normal;

        public Vec2 Normal
        {
            get
            {
                return _normal;
            }
        }
        public float Length
        {
            get
            {
                return (start - end).Length;
            }
        }

        public LineSegment(Vec2 pStart, Vec2 pEnd, Color color, uint pLineWidth = 1) : base()
        {
            SetCollider(ColliderType.Line);
            line = new EasyDraw(Game.main.width, Game.main.height);

            start = pStart;
            end = pEnd;
            _normal = Vec2.Normal(start, end);
            this.color = color;
            lineWidth = pLineWidth;

            line.LineCap(System.Drawing.Drawing2D.LineCap.Round);
            line.StrokeWeight(pLineWidth);
            line.Stroke(color);
            line.Line(start, end);
        }

        public bool BetweenSegment(Vec2 point)
        {
            Vec2 lineVector = end - start;
            Vec2 poiVector = point - start;
            float d = poiVector.Dot(lineVector.Normalized()); // Distance along line
            return (d >= 0 && d <= Length);
        }
    }
}

