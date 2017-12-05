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
            ksPart innerRim = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part); 
            ksEntity planeXOY = innerRim.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY); //определим плоскость XY
            ksEntity innerRimSketch = innerRim.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition innerRimSketchDef = innerRimSketch.GetDefinition(); //получим указатель на параметры эскиза
            innerRimSketchDef.SetPlane(planeXOY); //зададим плоскость на которой создаем эскиз
            innerRimSketch.Create(); // создаем эскиз
            ksDocument2D innerRimDoc = innerRimSketchDef.BeginEdit(); //входим в режим редактирование эскиза
            DrawInnerRim(innerRimDoc, bearing._bearingWidth, bearing._innerRimRad,bearing._innerRimWidth,bearing._gutterDepth, bearing._ballRad);
            DrawOuterRim(innerRimDoc, bearing._bearingWidth, bearing._innerRimRad, bearing._innerRimWidth, bearing._gutterDepth, bearing._ballRad);
            innerRimSketchDef.EndEdit(); //закончили редактировать эскиз
            //-------------------------------------
            ksPart topStoolPart1 = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part); //указатель на деталь
            ksEntity planeXoy1 = topStoolPart1.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY); //определим плоскость XY
            ksEntity sketch1 = topStoolPart1.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition sd1 = sketch1.GetDefinition(); //получим указатель на параметры эскиза
            sd1.SetPlane(planeXoy1); //зададим плоскость на которой создаем эскиз
            sketch1.Create(); // создаем эскиз
            ksDocument2D doc2d1 = sd1.BeginEdit(); //входим в режим редактирование эскиза
            DrawBalls(doc2d1, bearing._bearingWidth, bearing._innerRimRad, bearing._innerRimWidth, bearing._gutterDepth, bearing._ballRad);
            sd1.EndEdit(); //закончили редактировать эскиз

            
            ksEntity planeXOZ = topStoolPart1.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOZ);
            ksEntity planeYOZ = topStoolPart1.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            

            //BossRotatedExtrusion(innerRim, innerRimSketch, (short)Direction_Type.dtNormal);
            //Extrude(topStoolPart1, sketch1, (short)Direction_Type.dtNormal, planeXOZ, planeYOZ);

            ksEntity mas = topStoolPart1.NewEntity((short)Obj3dType.o3d_circularCopy);
        }
        private static void Extrude(ksPart part, ksEntity sketch, short type, ksEntity planeXOZ, ksEntity planeYOZ)
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
        private static void DrawInnerRim(ksDocument2D innerRimDoc, double BearingWidth, 
           double InnerRimRad, double InnerRimWidth,double GutterDepth, double BallRad)
        {
            /* float cutRad = (float)(Math.Sqrt(Math.Pow(BallRad, 2) - Math.Pow(BallRad - GutterDepth, 2)));
             innerRimDoc.ksLineSeg(-BearingWidth/2,InnerRimRad-InnerRimWidth, BearingWidth / 2, InnerRimRad - InnerRimWidth, 1);
             innerRimDoc.ksLineSeg(-BearingWidth / 2, InnerRimRad - InnerRimWidth, -BearingWidth / 2, InnerRimRad, 1);
             innerRimDoc.ksLineSeg(BearingWidth / 2, InnerRimRad - InnerRimWidth, BearingWidth / 2, InnerRimRad, 1);
             innerRimDoc.ksLineSeg(-BearingWidth / 2, InnerRimRad, -(BearingWidth /2) + cutRad, InnerRimRad, 1);
             innerRimDoc.ksLineSeg(BearingWidth / 2, InnerRimRad, (BearingWidth / 2) - cutRad, InnerRimRad, 1);
             innerRimDoc.ksArcBy3Points(-(BearingWidth / 2) + cutRad, InnerRimRad, 0, InnerRimRad - GutterDepth, (BearingWidth / 2) - cutRad, InnerRimRad, 1);
             innerRimDoc.ksLineSeg(-BearingWidth / 2, 0, BearingWidth / 2, 0, 3);*/
            
        }
        private static void DrawOuterRim(ksDocument2D innerRimDoc, double BearingWidth, double InnerRimRad, double InnerRimWidth, double GutterDepth, double BallRad)
        {
            /*float cutRad = (float)(Math.Sqrt(Math.Pow(BallRad, 2) - Math.Pow(BallRad - GutterDepth, 2)));
            innerRimDoc.ksLineSeg(-BearingWidth / 2, InnerRimRad + InnerRimWidth + BallRad, BearingWidth / 2, InnerRimRad + InnerRimWidth + BallRad, 1);
            innerRimDoc.ksLineSeg(-BearingWidth / 2, InnerRimRad + InnerRimWidth + BallRad, -BearingWidth / 2, InnerRimRad + BallRad, 1);
            innerRimDoc.ksLineSeg(BearingWidth / 2, InnerRimRad + InnerRimWidth + BallRad, BearingWidth / 2, InnerRimRad + BallRad, 1);
            innerRimDoc.ksLineSeg(-BearingWidth / 2, InnerRimRad + BallRad, -(BearingWidth / 2) + cutRad, InnerRimRad + BallRad, 1);
            innerRimDoc.ksLineSeg(BearingWidth / 2, InnerRimRad + BallRad, (BearingWidth / 2) - cutRad, InnerRimRad + BallRad, 1);
            innerRimDoc.ksArcBy3Points(-(BearingWidth / 2) + cutRad, InnerRimRad + BallRad, 0, InnerRimRad + BallRad + GutterDepth, (BearingWidth / 2) - cutRad, InnerRimRad + BallRad, 1);
            innerRimDoc.ksLineSeg(-BearingWidth / 2, 0, BearingWidth / 2, 0, 3);*/
        }
        private static void DrawBalls(ksDocument2D doc2d,double BearingWidth, double InnerRimRad, double InnerRimWidth, double GutterDepth, double BallRad)
        {
            doc2d.ksArcByAngle(0, InnerRimRad-GutterDepth+BallRad, BallRad, -90, 90, 1, 1);
            doc2d.ksLineSeg(0, InnerRimRad - GutterDepth, 0, InnerRimRad - GutterDepth + 2 * BallRad, 3);
        }
    }
}
