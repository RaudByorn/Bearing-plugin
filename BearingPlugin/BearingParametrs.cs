using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearingPlugin
{
    public class BearingParametrs
    {
        /// <summary>
        /// Ширина подшипник
        /// </summary>
        public double _bearingWidth;
        /// <summary>
        /// Радиус внутреннего обода
        /// </summary>
        public double _innerRimRad;
        /// <summary>
        /// Ширина внутреннего обода
        /// </summary>
        public double _innerRimWidth;
        /// <summary>
        /// Глубина желоба
        /// </summary>
        public double _gutterDepth;
        /// <summary>
        /// Радиус шариков
        /// </summary>
        public double _ballRad;

        /// <summary>
        /// Геттер и сеттер на ирину подшипника
        /// </summary>
        private double BearingWidth
        {
            get => _bearingWidth;
            set
            {
                _bearingWidth = value;
            }
        }

        /// <summary>
        /// Геттер и сеттер на радиус внутреннего обода
        /// </summary>
        private double InnerRimRad
        {
            get => _innerRimRad;
            set
            {
                _innerRimRad = value;
            }
        }

        /// <summary>
        /// Геттер и сеттер на ширину внутреннего обода
        /// </summary>
        private double InnerRimWidth
        {
            get => _innerRimWidth;
            set
            {
                _innerRimWidth = value;
            }
        }

        /// <summary>
        /// Геттер и сеттер на глубину желоба
        /// </summary>
        private double GutterDepth
        {
            get => _gutterDepth;
            set
            {
                _gutterDepth = value;
            }
        }

        /// <summary>
        /// Геттер и сеттер на радиус шарика
        /// </summary>
        private double BallRad
        {
            get => _ballRad;
            set
            {
                _ballRad = value;
            }
        }

        /// <summary>
        /// Присваиваем переменные
        /// </summary>
        /// <param name="bearingWidth"></param>
        /// <param name="innerRimRad"></param>
        /// <param name="innerRimWidth"></param>
        /// <param name="gutterDepth"></param>
        /// <param name="ballRad"></param>
        public BearingParametrs(double bearingWidth, double innerRimRad, double innerRimWidth, double gutterDepth, double ballRad)
        {
                BearingWidth = bearingWidth;
                InnerRimRad = innerRimRad;
                InnerRimWidth = innerRimWidth;
                GutterDepth = gutterDepth;
                BallRad = ballRad;
        }
    }
}
