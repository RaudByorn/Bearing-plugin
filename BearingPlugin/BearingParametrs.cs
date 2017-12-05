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
        /// Диаметр внутреннего обода
        /// </summary>
        public double _innerRimDiam;
        /// <summary>
        /// Диаметр внешнего обода
        /// </summary>
        public double _outerRimDiam;

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
        private double InnerRimDiam
        {
            get => _innerRimDiam;
            set
            {
                _innerRimDiam = value;
            }
        }

        /// <summary>
        /// Геттер и сеттер на ширину внутреннего обода
        /// </summary>
        private double OuterRimDiam
        {
            get => _outerRimDiam;
            set
            {
                _outerRimDiam = value;
            }
        }

        /// <summary>
        /// Присваиваем значения
        /// </summary>
        /// <param name="bearingWidth"></param>
        /// <param name="innerRimDiam"></param>
        /// <param name="outerRimDiam"></param>
        public BearingParametrs(double bearingWidth, double innerRimDiam, double outerRimDiam)
        {
                BearingWidth = bearingWidth;
                InnerRimDiam = innerRimDiam;
                OuterRimDiam = outerRimDiam;
        }
    }
}
