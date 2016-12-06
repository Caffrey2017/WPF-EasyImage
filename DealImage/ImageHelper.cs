﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DealImage
{
    public static class ImageHelper
    {

        //public static Rect GetMinContainerBounds(this FrameworkElement element)
        //{
        //    // Get the size of the Visual and its descendants.
        //    var rect = VisualTreeHelper.GetDescendantBounds(element);

        //    var topLeftPoint = element.TranslatePoint(new Point(0, 0), null);
        //    var topRightPoint = element.TranslatePoint(new Point(rect.Width, 0), null);
        //    var bottomRightPoint = element.TranslatePoint(new Point(rect.Width, rect.Height), null);
        //    var bottomLeftPoint = element.TranslatePoint(new Point(0, rect.Height), null);
        //    var width = Math.Max(Math.Abs(topLeftPoint.X - bottomRightPoint.X), Math.Abs(topRightPoint.X - bottomLeftPoint.X));
        //    var height = Math.Max(Math.Abs(topLeftPoint.Y - bottomRightPoint.Y), Math.Abs(topRightPoint.Y - bottomLeftPoint.Y));

        //    return new Rect(0, 0, width, height);
        //}

        public static RenderTargetBitmap GetRenderTargetBitmap(this IDictionary<FrameworkElement, FrameworkElement> dictionary, SolidColorBrush backgrand = null)
        {
            var rect = dictionary.Values.GetMinContainerBounds();
            var relationPoint = new Point(rect.X, rect.Y);
            rect.X = rect.Y = 0;

            var drawingVisual = new DrawingVisual();
            using (var context = drawingVisual.RenderOpen())
            {
                context.DrawRectangle(null, null, rect);
                foreach (var item in dictionary)
                {
                    var viewbox = item.Key.GetChildViewbox(item.Value);
                    var brush = new VisualBrush(item.Key)
                    {
                        ViewboxUnits = BrushMappingMode.RelativeToBoundingBox,
                        Viewbox = viewbox,
                    };
                    context.DrawRectangle(brush, null, item.Value.GetRelationBounds(relationPoint));
                }
            }

            var renderBitmap = new RenderTargetBitmap((int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Pbgra32);
            var rectangle = new Rectangle
            {
                Width = (int) rect.Width,
                Height = (int) rect.Height,
                Fill = backgrand??Brushes.Transparent,
            };
            rectangle.Measure(rect.Size);
            rectangle.Arrange(new Rect(rect.Size));
           
            renderBitmap.Render(rectangle);

            renderBitmap.Render(drawingVisual);
            return renderBitmap;
        }

        public static Rect GetMinContainerBounds(this IEnumerable<FrameworkElement> elements)
        {
            double minX = double.MaxValue, minY = double.MaxValue, maxX = double.MinValue, maxY = double.MinValue;
            foreach (var element in elements)
            {
                var rect = VisualTreeHelper.GetDescendantBounds(element);
                var topLeftPoint = element.TranslatePoint(new Point(0, 0), null);
                var topRightPoint = element.TranslatePoint(new Point(rect.Width, 0), null);
                var bottomRightPoint = element.TranslatePoint(new Point(rect.Width, rect.Height), null);
                var bottomLeftPoint = element.TranslatePoint(new Point(0, rect.Height), null);
                minX = Math.Min(minX, Math.Min(Math.Min(topLeftPoint.X, topRightPoint.X), Math.Min(bottomLeftPoint.X, bottomRightPoint.X)));
                maxX = Math.Max(maxX, Math.Max(Math.Max(topLeftPoint.X, topRightPoint.X), Math.Max(bottomLeftPoint.X, bottomRightPoint.X)));
                minY = Math.Min(minY, Math.Min(Math.Min(topLeftPoint.Y, topRightPoint.Y), Math.Min(bottomLeftPoint.Y, bottomRightPoint.Y)));
                maxY = Math.Max(maxY, Math.Max(Math.Max(topLeftPoint.Y, topRightPoint.Y), Math.Max(bottomLeftPoint.Y, bottomRightPoint.Y)));
            }
            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public static Rect GetRelationBounds(this FrameworkElement element, Point relationPoint)
        {
            double minX = double.MaxValue, minY = double.MaxValue;
            var rect = VisualTreeHelper.GetDescendantBounds(element);

            var topLeftPoint = element.TranslatePoint(new Point(0, 0), null);
            var topRightPoint = element.TranslatePoint(new Point(rect.Width, 0), null);
            var bottomRightPoint = element.TranslatePoint(new Point(rect.Width, rect.Height), null);
            var bottomLeftPoint = element.TranslatePoint(new Point(0, rect.Height), null);
            var width = Math.Max(Math.Abs(topLeftPoint.X - bottomRightPoint.X), Math.Abs(topRightPoint.X - bottomLeftPoint.X));
            var height = Math.Max(Math.Abs(topLeftPoint.Y - bottomRightPoint.Y), Math.Abs(topRightPoint.Y - bottomLeftPoint.Y));
            minX = Math.Min(minX, Math.Min(Math.Min(topLeftPoint.X, topRightPoint.X), Math.Min(bottomLeftPoint.X, bottomRightPoint.X)));
            minY = Math.Min(minY, Math.Min(Math.Min(topLeftPoint.Y, topRightPoint.Y), Math.Min(bottomLeftPoint.Y, bottomRightPoint.Y)));

            return new Rect(minX - relationPoint.X, minY - relationPoint.Y, width, height);
        }

        public static Rect GetChildViewbox(this FrameworkElement element, FrameworkElement childElement)
        {
            var rect = VisualTreeHelper.GetDescendantBounds(element);
            var childRect = VisualTreeHelper.GetDescendantBounds(childElement);

            var topLeftPoint1 = element.TranslatePoint(rect.TopLeft, null);
            var topRightPoint1 = element.TranslatePoint(rect.TopRight, null);
            var bottomRightPoint1 = element.TranslatePoint(rect.BottomRight, null);
            var bottomLeftPoint1 = element.TranslatePoint(rect.BottomLeft, null);

            var minX1 = Math.Min(Math.Min(topLeftPoint1.X, topRightPoint1.X), Math.Min(bottomLeftPoint1.X, bottomRightPoint1.X));
            var minY1 = Math.Min(Math.Min(topLeftPoint1.Y, topRightPoint1.Y), Math.Min(bottomLeftPoint1.Y, bottomRightPoint1.Y));

            var topLeftPoint2 = childElement.TranslatePoint(childRect.TopLeft, null);
            var topRightPoint2 = childElement.TranslatePoint(childRect.TopRight, null);
            var bottomRightPoint2 = childElement.TranslatePoint(childRect.BottomRight, null);
            var bottomLeftPoint2 = childElement.TranslatePoint(childRect.BottomLeft, null);

            var minX2 = Math.Min(Math.Min(topLeftPoint2.X, topRightPoint2.X), Math.Min(bottomLeftPoint2.X, bottomRightPoint2.X));
            var minY2 = Math.Min(Math.Min(topLeftPoint2.Y, topRightPoint2.Y), Math.Min(bottomLeftPoint2.Y, bottomRightPoint2.Y));

            var y = Math.Abs(minY2 - minY1);
            var x = Math.Abs(minX2 - minX1);
            var width = Math.Max(Math.Abs(topLeftPoint1.X - bottomRightPoint1.X), Math.Abs(topRightPoint1.X - bottomLeftPoint1.X));
            var height = Math.Max(Math.Abs(topLeftPoint1.Y - bottomRightPoint1.Y), Math.Abs(topRightPoint1.Y - bottomLeftPoint1.Y));
            var childWidth = Math.Max(Math.Abs(topLeftPoint2.X - bottomRightPoint2.X), Math.Abs(topRightPoint2.X - bottomLeftPoint2.X));
            var childHeight = Math.Max(Math.Abs(topLeftPoint2.Y - bottomRightPoint2.Y), Math.Abs(topRightPoint2.Y - bottomLeftPoint2.Y));

            //Trace.WriteLine($"{x} | {y} | {width} | {height} | {childWidth} | {childHeight}");
            return new Rect(x / width, y / height, childWidth / width, childHeight / height);
        }

    }
}