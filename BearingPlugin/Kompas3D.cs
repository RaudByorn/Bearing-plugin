using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using KAPITypes;
using KompasAPI7;
using System.Runtime.InteropServices;
using reference = System.Int32;

namespace BearingPlugin
{
    class Kompas3D
    {
        /// <summary>
        /// Объект компаса
        /// </summary>
        private KompasObject _kompas = null;
        /// <summary>
        /// Запускаем компас
        /// </summary>
        private void RunningKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }

                if (_kompas == null)
                {
                    var t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(t);

                    RunningKompas();

                    if (_kompas == null) throw new Exception("Нет связи с Kompas3D.");
                }
            }
            catch (COMException)
            {
                _kompas = null;
                RunningKompas();
            }
        }
        /// <summary>
        /// строим подшипник
        /// </summary>
        /// <param name="bearing"></param>
        public void BuildBearing(BearingParametrs bearing)
        {
            RunningKompas();

            if (_kompas == null) throw new Exception("Не возможно построить деталь. Нет связи с Kompas3D.");

            if (bearing == null)
                throw new ArgumentNullException(nameof(bearing));

            ksDocument3D doc = _kompas.Document3D();
            doc.Create();

            //указатель на деталь
            ksPart rims = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part);
            //определим плоскость XY
            ksEntity planeXOY = rims.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            //создаем переменную эскиза
            ksEntity rimsSketch = rims.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch);
            //получим указатель на параметры эскиза
            ksSketchDefinition rimsSketchDef = rimsSketch.GetDefinition();
            //зададим плоскость на которой создаем эскиз
            rimsSketchDef.SetPlane(planeXOY);
            // создаем эскиз
            rimsSketch.Create();
            //входим в режим редактирование эскиза
            ksDocument2D rimsDoc = rimsSketchDef.BeginEdit();
            DrawInnerRim(rimsDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam, bearing._rimsThickness, bearing._ballDiam);
            DrawOuterRim(rimsDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam, bearing._rimsThickness, bearing._ballDiam);
            //закончили редактировать эскиз
            rimsSketchDef.EndEdit(); 

            ksPart ball = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part);
            ksEntity planeXOY1 = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            ksEntity ballSketch = ball.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch);
            ksSketchDefinition ballSketchDef = ballSketch.GetDefinition();
            ballSketchDef.SetPlane(planeXOY1);
            ballSketch.Create();
            ksDocument2D ballDoc = ballSketchDef.BeginEdit();
            DrawBalls(ballDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam, bearing._rimsThickness, bearing._ballDiam);
            ballSketchDef.EndEdit();

            
            ksEntity planeXOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOZ);
            ksEntity planeYOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            

            BossRotatedExtrusion(rims, rimsSketch, (short)Direction_Type.dtNormal);
            BallsConcentricArray(ball, ballSketch, (short)Direction_Type.dtNormal, planeXOZ, planeYOZ);

            ksEntity mas = ball.NewEntity((short)Obj3dType.o3d_circularCopy);
        }
        /// <summary>
        /// Создание массива шариков по концентрической сетке
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="type"></param>
        /// <param name="planeXOZ"></param>
        /// <param name="planeYOZ"></param>
        private static void BallsConcentricArray(ksPart part, ksEntity sketch, short type, ksEntity planeXOZ, ksEntity planeYOZ)
        {
            ksEntity rotate = part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition rotDef = rotate.GetDefinition();
            rotDef.directionType = type;
            rotDef.SetSketch(sketch);
            rotDef.SetSideParam(true,360);
            ksRotatedParam rotateParam = rotDef.RotatedParam();
            rotate.Create();
            //ОСЬ
            ksEntity axis = part.NewEntity((short)Obj3dType.o3d_axis2Planes);
            ksAxis2PlanesDefinition axisdef = axis.GetDefinition();
            axisdef.SetPlane(1, planeXOZ);
            axisdef.SetPlane(2, planeYOZ);
            axis.Create();
            //Массив по Кругу
            ksEntity circrotate = part.NewEntity((short)Obj3dType.o3d_circularCopy);
            ksCircularCopyDefinition cpyRotDef = circrotate.GetDefinition();
            cpyRotDef.SetAxis(axis);
            cpyRotDef.SetCopyParamAlongDir(8, 45, false, false);
            EntityCollection circcoll = (cpyRotDef.GetOperationArray());
            circcoll.Clear();
            circcoll.Add(rotate);
            circrotate.Create();
        }
        /// <summary>
        /// Выдавливание вращением
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="type"></param>
        private static void BossRotatedExtrusion(ksPart part, ksEntity sketch, short type)
        {
            ksEntity bossRotated = part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();
            bossRotatedDef.directionType = type;
            bossRotatedDef.SetSketch(sketch);
            bossRotatedDef.SetSideParam(true, 360);
            ksRotatedParam rotateParam = bossRotatedDef.RotatedParam();
            bossRotated.Create();
        }
        /// <summary>
        /// Рисуем эскиз внутреннего обода
        /// </summary>
        /// <param name="rimsDoc"></param>
        /// <param name="BearingWidth"></param>
        /// <param name="InnerRimDiam"></param>
        /// <param name="OuterRimDiam"></param>
        /// <param name="RimsThickness"></param>
        /// <param name="BallDiam"></param>
        private static void DrawInnerRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam, double OuterRimDiam, double RimsThickness, double BallDiam)
        {
            double bearingCenter = (OuterRimDiam - InnerRimDiam) / 4 + InnerRimDiam / 2;
            double gutterDepth = BallDiam / 2 - (bearingCenter - InnerRimDiam / 2 - RimsThickness);
            double gutterWidth = (Math.Sqrt((BallDiam / 2) * (BallDiam / 2) - (BallDiam / 2 - gutterDepth) * (BallDiam / 2 - gutterDepth)));
            rimsDoc.ksLineSeg(-BearingWidth/2, 0 , BearingWidth/2 , 0 , 3);
            //основание
            rimsDoc.ksLineSeg(-BearingWidth/2, InnerRimDiam/2, BearingWidth / 2, InnerRimDiam / 2, 1);
            //левая грань
            rimsDoc.ksLineSeg(-BearingWidth / 2, InnerRimDiam / 2, -BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 1);
            //правая грань
            rimsDoc.ksLineSeg(BearingWidth / 2, InnerRimDiam / 2, BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 1);
            //левая верхняя
            rimsDoc.ksLineSeg(-BearingWidth / 2, RimsThickness + InnerRimDiam / 2, -gutterWidth, RimsThickness + InnerRimDiam / 2, 1);
            //правая верхняя
            rimsDoc.ksLineSeg(BearingWidth / 2, RimsThickness + InnerRimDiam / 2, gutterWidth, RimsThickness + InnerRimDiam / 2, 1);
            // желоб
            rimsDoc.ksArcBy3Points(-gutterWidth, RimsThickness + InnerRimDiam / 2, 
                0 , RimsThickness + InnerRimDiam / 2 - gutterDepth, gutterWidth, RimsThickness + InnerRimDiam / 2, 1);

        }
        /// <summary>
        /// Рисуем эскиз внешнего обода
        /// </summary>
        /// <param name="rimsDoc"></param>
        /// <param name="BearingWidth"></param>
        /// <param name="InnerRimDiam"></param>
        /// <param name="OuterRimDiam"></param>
        /// <param name="RimsThickness"></param>
        /// <param name="BallDiam"></param>
        private static void DrawOuterRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam, double OuterRimDiam, double RimsThickness, double BallDiam)
        {
            double bearingCenter = (OuterRimDiam - InnerRimDiam) / 4 + InnerRimDiam / 2;
            double gutterDepth = BallDiam / 2 - (bearingCenter - InnerRimDiam / 2 - RimsThickness);
            double gutterWidth = (Math.Sqrt((BallDiam / 2) * (BallDiam / 2) - (BallDiam / 2 - gutterDepth) * (BallDiam / 2 - gutterDepth)));

            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2, 1);
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, -BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 1);
            rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 1);
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, -gutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
            rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, gutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
            rimsDoc.ksArcBy3Points(-gutterWidth ,OuterRimDiam / 2 - RimsThickness,
                0,  OuterRimDiam / 2 - RimsThickness + gutterDepth, gutterWidth, OuterRimDiam / 2 - RimsThickness, 1);


        }
        /// <summary>
        /// Рисуем эских шарика
        /// </summary>
        /// <param name="ballDoc"></param>
        /// <param name="BearingWidth"></param>
        /// <param name="InnerRimDiam"></param>
        /// <param name="OuterRimDiam"></param>
        /// <param name="RimsThickness"></param>
        /// <param name="BallDiam"></param>
        private static void DrawBalls(ksDocument2D ballDoc, double BearingWidth, double InnerRimDiam, double OuterRimDiam, double RimsThickness, double BallDiam)
        {
            double bearingCenter = (OuterRimDiam - InnerRimDiam) / 4 + InnerRimDiam / 2;
            double gutterDepth = BallDiam / 2 - (bearingCenter - InnerRimDiam / 2 - RimsThickness);
            double gutterWidth = (Math.Sqrt((BallDiam / 2) * (BallDiam / 2) - (BallDiam / 2 - gutterDepth) * (BallDiam / 2 - gutterDepth)));
            //Эскиз шариков
            ballDoc.ksArcByAngle(0, (OuterRimDiam - InnerRimDiam) / 4 + InnerRimDiam / 2, BallDiam / 2, -90, 90, 1, 1);
            ballDoc.ksLineSeg(0, 0, 0, 1, 3);
        }
    }
}
