﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using KAPITypes;
using KompasAPI7;
using reference = System.Int32;

namespace BearingPlugin
{
    class Kompas3D
    {
        public KompasObject _kompas = null;

        public Kompas3D()
        {

        }
        public void RunKompas3D()
        {
            if (_kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompas = (KompasObject)Activator.CreateInstance(t);
            }

            if (_kompas != null)
            {
                _kompas.Visible = true;
                _kompas.ActivateControllerAPI();
            }

            if (_kompas == null) throw new Exception("Нет связи с Kompas3D.");

        }
        public void BuildBearing(BearingParametrs bearing)
        {
            if (_kompas == null) throw new Exception("Не возможно построить деталь. Нет связи с Kompas3D.");

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
            DrawInnerRim(rimsDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam);
            DrawOuterRim(rimsDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam);
            //закончили редактировать эскиз
            rimsSketchDef.EndEdit(); 

            ksPart ball = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part);
            ksEntity planeXOY1 = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            ksEntity ballSketch = ball.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch);
            ksSketchDefinition ballSketchDef = ballSketch.GetDefinition();
            ballSketchDef.SetPlane(planeXOY1);
            ballSketch.Create();
            ksDocument2D ballDoc = ballSketchDef.BeginEdit();
            DrawBalls(ballDoc, bearing._bearingWidth, bearing._innerRimDiam, bearing._outerRimDiam);
            ballSketchDef.EndEdit();

            
            ksEntity planeXOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOZ);
            ksEntity planeYOZ = ball.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            

            BossRotatedExtrusion(rims, rimsSketch, (short)Direction_Type.dtNormal);
            BallsConcentricArray(ball, ballSketch, (short)Direction_Type.dtNormal, planeXOZ, planeYOZ);

            ksEntity mas = ball.NewEntity((short)Obj3dType.o3d_circularCopy);
        }
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
        private static void DrawInnerRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam, double OuterRimDiam)
        {
            float ballDiam = (float)BearingWidth / 2;
            float rimWidth = (float)((OuterRimDiam - InnerRimDiam) / 6);
            float gutterDepth = Math.Abs((float)((ballDiam - rimWidth)/2));
            float cutRad = (float)(Math.Sqrt(Math.Pow(ballDiam/2, 2) - Math.Pow(ballDiam/2 - gutterDepth, 2)));
            

            rimsDoc.ksLineSeg(-BearingWidth/2, 0 , BearingWidth/2 , 0 , 3);
            //основание
            rimsDoc.ksLineSeg(-BearingWidth/2, InnerRimDiam/2, BearingWidth / 2, InnerRimDiam / 2, 1);
            //левая грань
            rimsDoc.ksLineSeg(-BearingWidth / 2, InnerRimDiam / 2, -BearingWidth / 2, rimWidth + InnerRimDiam/2, 1);
            //правая грань
            rimsDoc.ksLineSeg(BearingWidth / 2, InnerRimDiam / 2, BearingWidth / 2, rimWidth + InnerRimDiam / 2, 1);
            //левое верхнее
            rimsDoc.ksLineSeg(-BearingWidth / 2, rimWidth + InnerRimDiam / 2, -cutRad, rimWidth + InnerRimDiam / 2, 1);
            //правое верхнее
            rimsDoc.ksLineSeg(BearingWidth / 2, rimWidth + InnerRimDiam / 2, cutRad, rimWidth + InnerRimDiam / 2, 1);
            //дуга
            rimsDoc.ksArcBy3Points(-cutRad , rimWidth + InnerRimDiam / 2, 0 , rimWidth + InnerRimDiam / 2  - gutterDepth, 
                cutRad, rimWidth + InnerRimDiam / 2, 1);

            
        }
        private static void DrawOuterRim(ksDocument2D rimsDoc, double BearingWidth, double InnerRimDiam, double OuterRimDiam)
        {
            float ballDiam = (float)BearingWidth / 2;
            float rimWidth = (float)((OuterRimDiam - InnerRimDiam) / 6);
            float gutterDepth = Math.Abs((float)((ballDiam - rimWidth) / 2));
            float cutRad = (float)(Math.Sqrt(Math.Pow(ballDiam / 2, 2) - Math.Pow(ballDiam / 2 - gutterDepth, 2)));
            // осевая
            //rimsDoc.ksLineSeg(-BearingWidth / 2, 0, BearingWidth / 2, 0, 3);
            // основание
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2, 1);
            // левая грань
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2, -BearingWidth / 2, OuterRimDiam / 2 - rimWidth, 1);
            // правая грань
            rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2, BearingWidth / 2, OuterRimDiam / 2 - rimWidth, 1);
            // левое верхнее
            rimsDoc.ksLineSeg(-BearingWidth / 2, OuterRimDiam / 2 - rimWidth, -cutRad, OuterRimDiam / 2 - rimWidth, 1);
            // правое верхнее 
            rimsDoc.ksLineSeg(BearingWidth / 2, OuterRimDiam / 2 - rimWidth, cutRad, OuterRimDiam / 2 - rimWidth, 1);
            // дуга
            rimsDoc.ksArcBy3Points(-cutRad, OuterRimDiam / 2 - rimWidth, 0, OuterRimDiam / 2 - rimWidth + gutterDepth, cutRad, OuterRimDiam / 2 - rimWidth, 1);

        }
        private static void DrawBalls(ksDocument2D doc2d,double BearingWidth, double InnerRimDiam, double OuterRimDiam)
        {
            float ballDiam = (float)BearingWidth / 2;
            float rimWidth = (float)((OuterRimDiam - InnerRimDiam) / 6);
            float gutterDepth = Math.Abs((float)((ballDiam - rimWidth) / 2));
            //Эскиз шариков
            doc2d.ksArcByAngle(0, rimWidth + InnerRimDiam / 2 - gutterDepth + ballDiam / 2, ballDiam / 2, -90, 90, 1, 1);
            doc2d.ksLineSeg(0, (OuterRimDiam - InnerRimDiam) / 6 - gutterDepth + InnerRimDiam / 2, 0, OuterRimDiam / 2 - (OuterRimDiam - InnerRimDiam) / 6 + gutterDepth, 3);
        }
    }
}
