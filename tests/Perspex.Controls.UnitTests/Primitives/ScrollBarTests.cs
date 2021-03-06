﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Perspex.Controls.Primitives;
using Perspex.Controls.Templates;
using Perspex.Media;
using Xunit;

namespace Perspex.Controls.UnitTests.Primitives
{
    public class ScrollBarTests
    {
        [Fact]
        public void Setting_Value_Should_Update_Track_Value()
        {
            var target = new ScrollBar
            {
                Template = new FuncControlTemplate<ScrollBar>(Template),
            };

            target.ApplyTemplate();
            var track = target.GetTemplateChild<Track>("track");
            target.Value = 50;

            Assert.Equal(track.Value, 50);
        }

        [Fact]
        public void Setting_Track_Value_Should_Update_Value()
        {
            var target = new ScrollBar
            {
                Template = new FuncControlTemplate<ScrollBar>(Template),
            };

            target.ApplyTemplate();
            var track = target.GetTemplateChild<Track>("track");
            track.Value = 50;

            Assert.Equal(target.Value, 50);
        }

        [Fact]
        public void Setting_Track_Value_After_Setting_Value_Should_Update_Value()
        {
            var target = new ScrollBar
            {
                Template = new FuncControlTemplate<ScrollBar>(Template),
            };

            target.ApplyTemplate();

            var track = target.GetTemplateChild<Track>("track");
            target.Value = 25;
            track.Value = 50;

            Assert.Equal(target.Value, 50);
        }

        [Fact]
        public void ScrollBar_Can_AutoHide()
        {
            var target = new ScrollBar();

            target.Visibility = ScrollBarVisibility.Auto;
            target.Minimum = 0;
            target.Maximum = 100;
            target.ViewportSize = 100;

            Assert.False(target.IsVisible);
        }

        [Fact]
        public void ScrollBar_Should_Not_AutoHide_When_ViewportSize_Is_NaN()
        {
            var target = new ScrollBar();

            target.Visibility = ScrollBarVisibility.Auto;
            target.Minimum = 0;
            target.Maximum = 100;
            target.ViewportSize = double.NaN;

            Assert.True(target.IsVisible);
        }

        [Fact]
        public void ScrollBar_Should_Not_AutoHide_When_Visibility_Set_To_Visible()
        {
            var target = new ScrollBar();

            target.Visibility = ScrollBarVisibility.Visible;
            target.Minimum = 0;
            target.Maximum = 100;
            target.ViewportSize = 100;

            Assert.True(target.IsVisible);
        }

        [Fact]
        public void ScrollBar_Should_Hide_When_Visibility_Set_To_Hidden()
        {
            var target = new ScrollBar();

            target.Visibility = ScrollBarVisibility.Hidden;
            target.Minimum = 0;
            target.Maximum = 100;
            target.ViewportSize = 10;

            Assert.False(target.IsVisible);
        }

        private static Control Template(ScrollBar control)
        {
            return new Border
            {
                Child = new Track
                {
                    Name = "track",
                    [!Track.MinimumProperty] = control[!RangeBase.MinimumProperty],
                    [!Track.MaximumProperty] = control[!RangeBase.MaximumProperty],
                    [!!Track.ValueProperty] = control[!!RangeBase.ValueProperty],
                    [!Track.ViewportSizeProperty] = control[!ScrollBar.ViewportSizeProperty],
                    [!Track.OrientationProperty] = control[!ScrollBar.OrientationProperty],
                    Thumb = new Thumb
                    {
                        Template = new FuncControlTemplate<Thumb>(ThumbTemplate),
                    },
                },
            };
        }

        private static Control ThumbTemplate(Thumb control)
        {
            return new Border
            {
                Background = Brushes.Gray,
            };
        }
    }
}