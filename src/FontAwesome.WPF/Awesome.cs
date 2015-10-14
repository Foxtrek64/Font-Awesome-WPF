﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Provides attached properties to set FontAwesome icons on controls.
    /// </summary>
    public static class Awesome
    {
        /// <summary>
        /// Identifies the FontAwesome.WPF.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached(
                "Content",
                typeof(FontAwesomeIcon),
                typeof(Awesome),
                new PropertyMetadata(_defaultContent, ContentChanged));

        /// <summary>
        /// Gets the content of a ContentControl, expressed as a FontAwesome icon.
        /// </summary>
        /// <param name="target">The ContentControl subject of the query</param>
        /// <returns>FontAwesome icon found as content</returns>
        public static FontAwesomeIcon GetContent(DependencyObject target)
        {
            return (FontAwesomeIcon)target.GetValue(ContentProperty);
        }
        
        /// <summary>
        /// Sets the content of a ContentControl expressed as a FontAwesome icon. This will cause the content to be redrawn.
        /// </summary>
        /// <param name="target">The ContentControl where to set the content</param>
        /// <param name="value">FontAwesome icon to set as content</param>
        public static void SetContent(DependencyObject target, FontAwesomeIcon value)
        {
            target.SetValue(ContentProperty, value);
        }

        private static void ContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
        {
            // If target is not a ContenControl just ignore: Awesome.Content property can only be set on a ContentControl element
            if (!(sender is ContentControl)) return;

            ContentControl target = sender as ContentControl;

            // If value is not a FontAwesomeIcon just ignore: Awesome.Content property can only be set to a FontAwesomeIcon value
            if (!(evt.NewValue is FontAwesomeIcon)) return;

            FontAwesomeIcon symbolIcon = (FontAwesomeIcon)evt.NewValue;
            int symbolCode = (int)symbolIcon;
            char symbolChar = (char)symbolCode;

            target.Content = symbolChar;
        }

        private static readonly FontAwesomeIcon _defaultContent = FontAwesomeIcon.None;

        /// <summary>
        /// Identifies the FontAwesome.WPF.Awesome.SpinDuration attached dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.RegisterAttached(
                "SpinDuration",
                typeof(double),
                typeof(Awesome),
                new PropertyMetadata(1d, SpinDurationChanged));

        /// <summary>
        /// Gets the duration of the spinning animation (in seconds).
        /// </summary>
        /// <param name="target">The ContentControl subject of the query</param>
        /// <returns>Duration of the spinning animation</returns>
        public static double GetSpinDuration(DependencyObject target)
        {
            return (double)target.GetValue(SpinDurationProperty);
        }

        /// <summary>
        /// Sets the duration of the spinning animation (in seconds). This will stop and start the spin animation.
        /// </summary>
        /// <param name="target">The ContentControl where to set the content</param>
        /// <param name="value">Duration of the spinning animation</param>
        public static void SetSpinDuration(DependencyObject target, double value)
        {
            target.SetValue(SpinDurationProperty, value);
        }

        private static void SpinDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
        {
            ISpinable target = sender as ISpinable;

            if (null == target || !target.Spin || !(evt.NewValue is double) || evt.NewValue.Equals(evt.OldValue)) return;

            target.Spin = false;
            target.Spin = true;
        }
    }
}
