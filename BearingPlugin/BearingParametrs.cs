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
                if (value < 3 || value > 16)
                    throw new ArgumentException("Ширина подшипника не должна быть меньше 3 и превышать 16!");
                else
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
                if (value < 3 || value > 75)
                    throw new ArgumentException("Диаметр внутреннего кольца не должен быть меньше 3 и превышать 75!");
                else
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
                if (value < 3 || value > 105)
                    throw new ArgumentException("Диаметр внешнего кольца не должен быть меньше 3 и превышать 105!");
                else
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
            if (innerRimDiam > outerRimDiam || innerRimDiam == outerRimDiam || outerRimDiam-innerRimDiam < 5)
            {
                throw new ArgumentException("Заданы не верные пропорции!");
            }
            else
            {
                BearingWidth = bearingWidth;
                InnerRimDiam = innerRimDiam;
                OuterRimDiam = outerRimDiam;
            }
        
        }
    }
}
