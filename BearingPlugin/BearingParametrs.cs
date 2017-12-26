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
        /// Форма элемента качения
        /// </summary>
        private RollingElementForm _rollingElementForm;
        /// <summary>
        /// Ширина подшипник
        /// </summary>
        private double _bearingWidth;
        /// <summary>
        /// Диаметр внутреннего обода
        /// </summary>
        private double _innerRimDiam;
        /// <summary>
        /// Диаметр внешнего обода
        /// </summary>
        private double _outerRimDiam;
        /// <summary>
        /// Толщина ободов
        /// </summary>
        private double _rimsThickness;
        /// <summary>
        /// Диаметр элемента качения
        /// </summary>
        private double _rollingElementDiam;
        /// <summary>
        /// Координаты оси подшипника
        /// </summary>
        private readonly double _bearingAxis;
        /// <summary>
        /// Глубина желоба
        /// </summary>
        private readonly double _gutterDepth;
        /// <summary>
        /// Ширина желоба
        /// </summary>
        private readonly double _gutterWidth;
        /// <summary>
        /// Геттер для координаты оси подшипника
        /// </summary>
        public double BearingAxis => _bearingAxis;
        /// <summary>
        /// Геттер для глубины желоба
        /// </summary>
        public double GutterDepth => _gutterDepth;
        /// <summary>
        /// Шеттер для ширины желоба
        /// </summary>
        public double GutterWidth => _gutterWidth;
        /// <summary>
        /// Геттер и сеттер на ирину подшипника
        /// </summary>
        public double BearingWidth
        {
            get => _bearingWidth;
            private set
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
        public double InnerRimDiam
        {
            get => _innerRimDiam;
            private set
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
        public double OuterRimDiam
        {
            get => _outerRimDiam;
            private set
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
        public double RimsThickness
        {
            get => _rimsThickness;
            private set
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
        public double RollingElementDiam
        {
            get => _rollingElementDiam;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Диаметр элемента качения не может быть отрицательным");
                else
                    _rollingElementDiam = value;
            }
        }
        /// <summary>
        /// Геттер и Сеттер для формы элемента качения
        /// </summary>
        public RollingElementForm RollingElementForm
        {
            get => _rollingElementForm;
            private set
            {
                switch (value)
                {
                    case RollingElementForm.Ball:
                        _rollingElementForm = value;
                        break;
                    case RollingElementForm.Cylinder:
                        _rollingElementForm = value;
                        break;
                    default:
                        throw new ArgumentException("Неверная форма элемента качения!");
                }
            }
        }
        /// <summary>
        /// Присвоение значений и проверка пропорций
        /// </summary>
        /// <param name="bearingWidth"></param>
        /// <param name="innerRimDiam"></param>
        /// <param name="outerRimDiam"></param>
        public BearingParametrs(RollingElementForm rollingElementForm, double bearingWidth, 
            double innerRimDiam, double outerRimDiam, double rimsThickness, 
            double rollingElementDiam)
        {
            if (innerRimDiam > outerRimDiam || rimsThickness > (outerRimDiam-innerRimDiam)/4 
                || rimsThickness < (outerRimDiam - innerRimDiam) / 4 - rollingElementDiam / 2 + 0.1
                || innerRimDiam == outerRimDiam || outerRimDiam - innerRimDiam < 5
                || rollingElementDiam > bearingWidth || rollingElementDiam > (outerRimDiam - innerRimDiam) / 2 - 0.2)
            {
                throw new ArgumentException("Неверно заданы пропорции");
            }
            else
            { 
                BearingWidth = bearingWidth;
                InnerRimDiam = innerRimDiam;
                OuterRimDiam = outerRimDiam;
                RimsThickness = rimsThickness;
                RollingElementDiam = rollingElementDiam;
                RollingElementForm = rollingElementForm;
                _bearingAxis = (OuterRimDiam - InnerRimDiam) / 4 + InnerRimDiam / 2;
                _gutterDepth = rollingElementDiam / 2 - (BearingAxis - InnerRimDiam / 2 - RimsThickness);
                if (rollingElementForm is RollingElementForm.Ball)
                {
                    _gutterWidth = (Math.Sqrt((RollingElementDiam / 2) * (RollingElementDiam / 2) - (RollingElementDiam / 2 - GutterDepth) * (RollingElementDiam / 2 - GutterDepth)));
                }
                else
                {
                    _gutterWidth = BearingWidth / 4;
                }
            }
        }
    }
}
