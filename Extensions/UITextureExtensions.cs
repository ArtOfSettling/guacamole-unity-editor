using System;
using System.IO;
using System.Linq;
using UnityEngine;
using WellFired.Guacamole.Drawing;
using Rect = WellFired.Guacamole.Drawing.Rect;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class Texture2DExtensions
	{
		public static Texture2D CreateTexture(int width, int height, Color colour)
		{
			var pixelColors = new Color[width * height];
			for (var i = 0; i < pixelColors.Length; i++)
				pixelColors[i] = colour;

			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Repeat
			};

			result.SetPixels(pixelColors);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			return result;
		}

		public static Texture2D CreateRoundedTexture(int width, int height, UIColor backgroundColor, UIColor outlineColor, float radius)
		{
			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Clamp
			};

			var pixelData = new UIColor[width * height];
			var path = new GraphicsPath(new Rect(0, 0, width, height));

			path.FillWith(backgroundColor);
			path.Fill(ref pixelData, width, height);

			/*
			var widthOrHeightSmallest = Math.Min(width, height);
			var xCornerOffset = Math.Min(10, widthOrHeightSmallest);
			var yCornerOffset = Math.Min(10, widthOrHeightSmallest);

			var cornerDataTopLeft = GetRoundedCorner(
				new Vector(0.0, 0.0),
				new Vector(xCornerOffset, 0.0), 
				new Vector(0.0, yCornerOffset), 
				radius);

			var cornerDataTopRight = GetRoundedCorner(
				new Vector(width, 0.0), 
				new Vector(width - xCornerOffset, 0.0), 
				new Vector(width, yCornerOffset), 
				radius);

			var cornerDataBottomRight = GetRoundedCorner(
				new Vector(width, height), 
				new Vector(width - xCornerOffset, height), 
				new Vector(width, height - yCornerOffset), 
				radius);

			var cornerDataBottomLeft = GetRoundedCorner(
				new Vector(0.0, height), 
				new Vector(xCornerOffset, height), 
				new Vector(0.0, height - yCornerOffset), 
				radius);

			var cornerData = cornerDataTopLeft.Concat(cornerDataTopRight).Concat(cornerDataBottomRight).Concat(cornerDataBottomLeft);

			foreach (var corner in cornerData)
			{
				var x = (int)corner.X;
				var y = (int)corner.Y;

				if (x < 0)
					continue;
				if (y < 0)
					continue;
				if (x >= width)
					continue;
				if (y >= height)
					continue;

				pixelColors[x + y * width] = Color.black;
			}
			*/

			result.SetPixels(pixelData.ToList().Select(pixel => pixel.ToUnityColor()).ToArray());
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			var bytes = result.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

			return result;
		}

		private static Vector[] GetRoundedCorner(Vector angularPoint, Vector p1, Vector p2, float radius)
		{
			//Vector 1
			var dx1 = angularPoint.X - p1.X;
			var dy1 = angularPoint.Y - p1.Y;

			//Vector 2
			var dx2 = angularPoint.X - p2.X;
			var dy2 = angularPoint.Y - p2.Y;

			//Angle between vector 1 and vector 2 divided by 2
			var angle = (Math.Atan2(dy1, dx1) - Math.Atan2(dy2, dx2)) / 2;

			// The length of segment between angular point and the
			// points of intersection with the circle of a given radius
			var tan = Math.Abs(Math.Tan(angle));
			var segment = radius / tan;

			//Check the segment
			var length1 = GetLength(dx1, dy1);
			var length2 = GetLength(dx2, dy2);

			var length = Math.Min(length1, length2);

			if (segment > length)
			{
				segment = length;
				radius = (float)(length * tan);
			}

			// Points of intersection are calculated by the proportion between 
			// the coordinates of the vector, length of vector and the length of the segment.
			var p1Cross = GetProportionPoint(angularPoint, segment, length1, dx1, dy1);
			var p2Cross = GetProportionPoint(angularPoint, segment, length2, dx2, dy2);

			// Calculation of the coordinates of the circle 
			// center by the addition of angular vectors.
			var dx = angularPoint.X * 2 - p1Cross.X - p2Cross.X;
			var dy = angularPoint.Y * 2 - p1Cross.Y - p2Cross.Y;

			var L = GetLength(dx, dy);
			var d = GetLength(segment, radius);

			var circlePoint = GetProportionPoint(angularPoint, d, L, dx, dy);

			//StartAngle and EndAngle of arc
			var startAngle = Math.Atan2(p1Cross.Y - circlePoint.Y, p1Cross.X - circlePoint.X);
			var endAngle = Math.Atan2(p2Cross.Y - circlePoint.Y, p2Cross.X - circlePoint.X);

			//Sweep angle
			var sweepAngle = endAngle - startAngle;

			//Some additional checks
			if (sweepAngle < 0)
			{
				startAngle = endAngle;
				sweepAngle = -sweepAngle;
			}

			if (sweepAngle > Math.PI)
				sweepAngle = Math.PI - sweepAngle;

			// Draw
			const double degreeFactor = 180 / Math.PI;
			var pointsCount = (int)Math.Abs(sweepAngle * degreeFactor);
			var sign = Math.Sign(sweepAngle);

			var points = new Vector[pointsCount];

			for (var i = 0; i < pointsCount; ++i)
			{
				var pointX =
				   (float)(circlePoint.X
						   + Math.Cos(startAngle + sign * (double)i / degreeFactor)
						   * radius);

				var pointY =
				   (float)(circlePoint.Y
						   + Math.Sin(startAngle + sign * (double)i / degreeFactor)
						   * radius);

				points[i] = new Vector(pointX, pointY);
			}

			return points;
		}
		
		public static GraphicsPath RoundCorners(Vector[] points, float radius)
		{
			/*
			var retval = new GraphicsPath();

			if (points.Length < 3)
			{
				throw new ArgumentException();
			}
			
			Vector pt1, pt2;

			//Vectors for polygon sides and normal vectors
			Vector v1, v2, n1 = new Vector(), n2 = new Vector();

			//Rectangle that bounds arc
			var size = new Size(2 * radius, 2 * radius);

			//Arc center
			var center = new Vector();

			for (int i = 0; i < points.Length; i++)
			{
				pt1 = points[i]; //First vertex
				pt2 = points[i == points.Length - 1 ? 0 : i + 1];
				v1 = new Vector(pt2.X, pt2.Y) - new Vector(pt1.X, pt1.Y);
				pt2 = points[i == 0 ? points.Length - 1 : i - 1];
				v2 = new Vector(pt2.X, pt2.Y) - new Vector(pt1.X, pt1.Y);
																		
				var sweepangle = (float)Vector.AngleBetween(v1, v2);

				//Direction for normal vectors
				if (sweepangle < 0)
				{
					n1 = new Vector(v1.Y, -v1.X);
					n2 = new Vector(-v2.Y, v2.X);
				}
				else {
					n1 = new Vector(-v1.Y, v1.X);
					n2 = new Vector(v2.Y, -v2.X);
				}

				n1.Normalize(); n2.Normalize();
				n1 *= radius; n2 *= radius;
				
				var pt = points[i];
				pt1 = new Vector(pt.X + n1.X, pt.Y + n1.Y);
				pt2 = new Vector(pt.X + n2.X, pt.Y + n2.Y);
				double m1 = v1.Y / v1.X, m2 = v2.Y / v2.X;

				//Arc center
				if (Math.Abs(v1.X) < 0.0001)
				{
					// first line is parallel OY
					center.X = pt1.X;
					center.Y = (float)(m2 * (pt1.X - pt2.X) + pt2.Y);
				}
				else if (Math.Abs(v1.Y) < 0.0001)
				{
					// first line is parallel OX
					center.X = (float)((pt1.Y - pt2.Y) / m2 + pt2.X);
					center.Y = pt1.Y;
				}
				else if (Math.Abs(v2.X) < 0.0001)
				{
					// second line is parallel OY
					center.X = pt2.X;
					center.Y = (float)(m1 * (pt2.X - pt1.X) + pt1.Y);
				}
				else if (Math.Abs(v2.Y) < 0.0001)
				{
					//second line is parallel OX
					center.X = (float)((pt2.Y - pt1.Y) / m1 + pt1.X);
					center.Y = pt2.Y;
				}
				else
				{
					center.X = (float)((pt2.Y - pt1.Y + m1 * pt1.X - m2 * pt2.X) / (m1 - m2));
					center.Y = (float)(pt1.Y + m1 * (center.X - pt1.X));
				}

				n1.Negate();
				n2.Negate();
				pt1 = new Vector((float)(center.X + n1.X), (float)(center.Y + n1.Y));
				pt2 = new Vector((float)(center.X + n2.X), (float)(center.Y + n2.Y));

				//Rectangle that bounds tangent arc
				var rect = new Rect(center.X - radius, center.Y - radius, size);
				sweepangle = (float)Vector.AngleBetween(n2, n1);
				retval.AddArc(rect, (float)Vector.AngleBetween(new Vector(1, 0), n2), sweepangle);
			}
			retval.CloseAllFigures();
			return retval;
			*/
			return null;
		}

		private static double GetLength(double dx, double dy)
		{
			return Math.Sqrt(dx * dx + dy * dy);
		}

		private static Vector GetProportionPoint(Vector point, double segment, double length, double dx, double dy)
		{
			double factor = segment / length;

			return new Vector((float)(point.X - dx * factor), (float)(point.Y - dy * factor));
		}
	}
}