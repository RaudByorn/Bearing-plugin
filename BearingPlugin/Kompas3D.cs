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

namespace BearingPlugin
{
    public class Kompas3D
    {
        /// <summary>
        /// Объект компаса
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Метод для запуска КОМПАС-3D
        /// </summary>
        private void StartKompas()
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
                    //Возвращает тип, связанный с указанным идентификатором ProgID
                    var t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(t);

                    StartKompas();

                    if (_kompas == null)
                    { 
                        throw new Exception("Нет связи с Kompas3D.");
                    }
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartKompas();
            }
        }

        /// <summary>
        /// Метод для построения подшпника
        /// </summary>
        /// <param name="bearing">Параметры подшипника</param>
        public void BuildBearing(BearingParametrs bearing)
        {
            StartKompas();

            if (_kompas == null)
            {
                throw new Exception("Не возможно построить деталь. Нет связи с Kompas3D.");
            }

            if (bearing == null)
            {
                throw new ArgumentNullException(nameof(bearing));
            }

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
            DrawInnerRim(rimsDoc, bearing.BearingWidth, bearing.InnerRimDiam, bearing.OuterRimDiam,
                bearing.RimsThickness, bearing.RollingElementDiam, bearing.RollingElementForm, 
                bearing.BearingAxis, bearing.GutterDepth, bearing.GutterWidth);
            DrawOuterRim(rimsDoc, bearing.BearingWidth, bearing.InnerRimDiam, bearing.OuterRimDiam,
                bearing.RimsThickness, bearing.RollingElementDiam, bearing.RollingElementForm, 
                bearing.BearingAxis, bearing.GutterDepth, bearing.GutterWidth);
            //закончили редактировать эскиз
            rimsSketchDef.EndEdit(); 

            ksPart ball = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part);
            ksEntity planeXOY1 = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            ksEntity ballSketch = ball.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch);
            ksSketchDefinition ballSketchDef = ballSketch.GetDefinition();
            ballSketchDef.SetPlane(planeXOY1);
            ballSketch.Create();
            ksDocument2D ballDoc = ballSketchDef.BeginEdit();
            DrawBalls(ballDoc, bearing.BearingWidth, bearing.InnerRimDiam, bearing.OuterRimDiam, 
                bearing.RimsThickness, bearing.RollingElementDiam, bearing.RollingElementForm, 
                bearing.BearingAxis, bearing.GutterDepth, bearing.GutterWidth);
            ballSketchDef.EndEdit();
        
            ksEntity planeXOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOZ);
            ksEntity planeYOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
 
            BossRotatedExtrusion(rims, rimsSketch, (short)Direction_Type.dtNormal);
            BallsConcentricArray(ball, ballSketch, (short)Direction_Type.dtNormal, planeXOZ, planeYOZ);

            ksEntity mas = ball.NewEntity((short)Obj3dType.o3d_circularCopy);
        }

        /// <summary>
        /// Метод для создание массива шариков по концентрической сетке
        /// </summary>
        /// <param name="part">деталь</param>
        /// <param name="sketch">Эскиз</param>
        /// <param name="type">направлние</param>
        /// <param name="planeXOZ">плоскость XOZ</param>
        /// <param name="planeYOZ">плоскость YOZ</param>
        private static void BallsConcentricArray(ksPart part, ksEntity sketch, short type, 
            ksEntity planeXOZ, ksEntity planeYOZ)
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
        /// Метод для выдавливание вращением в КОМПАС-3D
        /// </summary>
        /// <param name="part">деталь</param>
        /// <param name="sketch">эскиз</param>
        /// <param name="type">направление</param>
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
        /// Метод для отрисовки эскиза внутреннего обода в КОМПАС-3D
        /// </summary>
        /// <param name="rimsDoc">эскиз</param>
        /// <param name="BearingWidth">Ширина подшипника</param>
        /// <param name="InnerRimDiam">Диаметр внутреннего обода</param>
        /// <param name="OuterRimDiam">Диаметр внешнего обода</param>
        /// <param name="RimsThickness">Толщина подшипника</param>
        /// <param name="BallDiam">Диаметр шарика</param>
        private static void DrawInnerRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam, 
            double OuterRimDiam, double RimsThickness, double RollingElementDiam, 
            RollingElementForm rollingElementForm, double BearingAxis, double GutterDepth, double GutterWidth)
        {
            //ось вращения
            rimsDoc.ksLineSeg(-BearingWidth/2, 0 , BearingWidth/2 , 0 , 3);
            //основание
            rimsDoc.ksLineSeg(-BearingWidth/2, InnerRimDiam/2, BearingWidth / 2, InnerRimDiam / 2, 1);
            //левая грань
            rimsDoc.ksLineSeg(-BearingWidth / 2, InnerRimDiam / 2, -BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 1);
            //правая грань
            rimsDoc.ksLineSeg(BearingWidth / 2, InnerRimDiam / 2, BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 1);
            if (rollingElementForm is RollingElementForm.Ball)
            {
                //левая верхняя
                rimsDoc.ksLineSeg(-BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 
                    -GutterWidth, RimsThickness + InnerRimDiam / 2, 1);
                //правая верхняя
                rimsDoc.ksLineSeg(BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 
                    GutterWidth, RimsThickness + InnerRimDiam / 2, 1);
                // желоб
                rimsDoc.ksArcBy3Points(-GutterWidth, RimsThickness + InnerRimDiam / 2,
                    0, RimsThickness + InnerRimDiam / 2 - GutterDepth, GutterWidth, RimsThickness + InnerRimDiam / 2, 1);
            }
            else
            {
                rimsDoc.ksLineSeg(-BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 
                    -GutterWidth, RimsThickness + InnerRimDiam / 2, 1);
                rimsDoc.ksLineSeg(BearingWidth / 2, RimsThickness + InnerRimDiam / 2, 
                    GutterWidth, RimsThickness + InnerRimDiam / 2, 1);
                rimsDoc.ksLineSeg(-GutterWidth, RimsThickness + InnerRimDiam / 2, 
                    -GutterWidth, RimsThickness + InnerRimDiam / 2 - GutterDepth, 1);
                rimsDoc.ksLineSeg(GutterWidth, RimsThickness + InnerRimDiam / 2,
                    GutterWidth, RimsThickness + InnerRimDiam / 2 - GutterDepth, 1);
                rimsDoc.ksLineSeg(-GutterWidth, RimsThickness + InnerRimDiam / 2 - GutterDepth,
                    GutterWidth, RimsThickness + InnerRimDiam / 2 - GutterDepth, 1);
            }
        }

        /// <summary>
        /// Метод для отрисовки эскиза внешнего обода в КОМПАС-3D
        /// </summary>
        /// <param name="rimsDoc">эскиз</param>
        /// <param name="BearingWidth">Ширина подшипника</param>
        /// <param name="InnerRimDiam">Диаметр внутреннего обода</param>
        /// <param name="OuterRimDiam">Диаметр внешнего обода</param>
        /// <param name="RimsThickness">Толщина подшипника</param>
        /// <param name="BallDiam">Диаметр шарика</param>
        private static void DrawOuterRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam,
            double OuterRimDiam, double RimsThickness, double BallDiam, 
             RollingElementForm rollingElementForm, double BearingAxis, double GutterDepth, double GutterWidth)
        {
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2, 1);
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, -BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 1);
            rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 1);
            if (rollingElementForm is RollingElementForm.Ball)
            {
                rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 
                    -GutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
                rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 
                    GutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
                rimsDoc.ksArcBy3Points(-GutterWidth, OuterRimDiam / 2 - RimsThickness,
                    0, OuterRimDiam / 2 - RimsThickness + GutterDepth, GutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
            }
            else
            {
                rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 
                    -GutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
                rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2 - RimsThickness, 
                    GutterWidth, OuterRimDiam / 2 - RimsThickness, 1);
                rimsDoc.ksLineSeg(-GutterWidth, OuterRimDiam / 2 - RimsThickness,
                    -GutterWidth, OuterRimDiam / 2 - RimsThickness + GutterDepth, 1);
                rimsDoc.ksLineSeg(GutterWidth, OuterRimDiam / 2 - RimsThickness,
                    GutterWidth, OuterRimDiam / 2 - RimsThickness + GutterDepth, 1);
                rimsDoc.ksLineSeg(-GutterWidth, OuterRimDiam / 2 - RimsThickness + GutterDepth,
                    GutterWidth, OuterRimDiam / 2 - RimsThickness + GutterDepth, 1);
            }
        }

        /// <summary>
        /// Метод для отрисовки эскиза элемена качения в КОМПАС-3D
        /// </summary>
        /// <param name="ballDoc">эскиз</param>
        /// <param name="BearingWidth">Ширина подшипника</param>
        /// <param name="InnerRimDiam">Диаметр внутреннего обода</param>
        /// <param name="OuterRimDiam">Диаметр внешнего обода</param>
        /// <param name="RimsThickness">Толщина подшипника</param>
        /// <param name="BallDiam">Диаметр шарика</param>
        private static void DrawBalls(ksDocument2D ballDoc, double BearingWidth, double InnerRimDiam, 
                double OuterRimDiam, double RimsThickness, double BallDiam, 
                RollingElementForm rollingElementForm, double BearingAxis, double GutterDepth, double GutterWidth)
            {
                if (rollingElementForm is RollingElementForm.Ball)
                {
                    ballDoc.ksArcByAngle(0, BearingAxis, BallDiam / 2, -90, 90, 1, 1);
                    ballDoc.ksLineSeg(0, 0, 0, 1, 3);
                }
                else
                {
                    ballDoc.ksLineSeg(-GutterWidth, BearingAxis, -GutterWidth, BearingAxis - BallDiam / 2, 1);
                    ballDoc.ksLineSeg(-GutterWidth, BearingAxis - BallDiam / 2, GutterWidth, 
                        BearingAxis - BallDiam / 2, 1);
                    ballDoc.ksLineSeg(GutterWidth, BearingAxis - BallDiam / 2, GutterWidth, BearingAxis, 1);
                    ballDoc.ksLineSeg(-GutterWidth, BearingAxis, GutterWidth, BearingAxis, 3);
                }
            }
    }
}
