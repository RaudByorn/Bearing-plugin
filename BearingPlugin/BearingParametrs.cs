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
        /// Толщина ободов
        /// </summary>
        public double _rimsThickness;
        /// <summary>
        /// Диаметр шарика
        /// </summary>
        public double _ballDiam;

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
        /// Геттер и Сеттер для толщины колец
        /// </summary>
        private double RimsThickness
        {
            get => _rimsThickness;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Толщина ободов не может быть отрицательной");
                else
                    _rimsThickness = value;
            }
        }
        /// <summary>
        /// Геттер и Сеттер для диаметра шариков
        /// </summary>
        private double BallDiam
        {
            get => _ballDiam;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Диаметр шарика не может быть отрицательным");
                else
                    _ballDiam = value;
            }
        }

        /// <summary>
        /// Присвоение значений и проверка пропорций
        /// </summary>
        /// <param name="bearingWidth"></param>
        /// <param name="innerRimDiam"></param>
        /// <param name="outerRimDiam"></param>
        public BearingParametrs(double bearingWidth, double innerRimDiam, double outerRimDiam, double rimsThickness, double ballDiam)
        {
            if (innerRimDiam > outerRimDiam || rimsThickness > (outerRimDiam-innerRimDiam)/4 
                || rimsThickness < (outerRimDiam - innerRimDiam) / 4 - ballDiam / 2 + 0.1
                || innerRimDiam == outerRimDiam || outerRimDiam - innerRimDiam < 5
                || ballDiam > bearingWidth || ballDiam > (outerRimDiam - innerRimDiam) / 2 - 0.2)
            {
                throw new ArgumentException("Неверно заданы пропорции");
            }
            else
            { 
                BearingWidth = bearingWidth;
                InnerRimDiam = innerRimDiam;
                OuterRimDiam = outerRimDiam;
                RimsThickness = rimsThickness;
                BallDiam = ballDiam;

            }
        }
    }
}
