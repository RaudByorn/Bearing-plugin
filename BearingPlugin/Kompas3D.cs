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
        public void BuildBearing(BearingParametrs bear)
        {
            if (_kompas == null) throw new Exception("Не возможно построить деталь. Нет связи с Kompas3D.");

            ksDocument3D doc = _kompas.Document3D();
            doc.Create();
            ksPart topStoolPart = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part); //указатель на деталь
            ksEntity planeXoy = topStoolPart.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY); //определим плоскость XY
            //---
            ksEntity planeXOZ = topStoolPart.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOZ);
            ksEntity planeYOZ = topStoolPart.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY);
            //---
            ksEntity sketch = topStoolPart.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition sd = sketch.GetDefinition(); //получим указатель на параметры эскиза
            sd.SetPlane(planeXoy); //зададим плоскость на которой создаем эскиз
            sketch.Create(); // создаем эскиз
            ksDocument2D doc2d = sd.BeginEdit(); //входим в режим редактирование эскиза
            DrawInnerRim(doc2d);
            DrawOuterRim(doc2d);
            sd.EndEdit(); //закончили редактировать эскиз
            //-------------------------------------
            ksPart topStoolPart1 = doc.GetPart((short)Kompas6Constants3D.Part_Type.pTop_Part); //указатель на деталь
            ksEntity planeXoy1 = topStoolPart1.GetDefaultEntity((short)Kompas6Constants3D.Obj3dType.o3d_planeXOY); //определим плоскость XY
            ksEntity sketch1 = topStoolPart1.NewEntity((short)Kompas6Constants3D.Obj3dType.o3d_sketch); //создаем переменную эскиза
            ksSketchDefinition sd1 = sketch1.GetDefinition(); //получим указатель на параметры эскиза
            sd1.SetPlane(planeXoy1); //зададим плоскость на которой создаем эскиз
            sketch1.Create(); // создаем эскиз
            ksDocument2D doc2d1 = sd1.BeginEdit(); //входим в режим редактирование эскиза
            DrawBalls(doc2d1);
            sd1.EndEdit(); //закончили редактировать эскиз


            ExtrudeRim(bear, topStoolPart, sketch, (short)Direction_Type.dtNormal);
            Extrude(bear, topStoolPart, sketch1, (short)Direction_Type.dtNormal, planeXOZ, planeYOZ);

            ksEntity mas = topStoolPart1.NewEntity((short)Obj3dType.o3d_circularCopy);
        }
        private static void Extrude(BearingParametrs bearing, ksPart part, ksEntity sketch, short type, ksEntity planeXOZ, ksEntity planeYOZ)
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
        private static void ExtrudeRim(BearingParametrs bearing, ksPart part, ksEntity sketch, short type)
        {
            ksEntity rotate = part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition rotDef = rotate.GetDefinition();
            rotDef.directionType = type;
            rotDef.SetSketch(sketch);
            rotDef.SetSideParam(true, 360);
            ksRotatedParam rotateParam = rotDef.RotatedParam();
            rotate.Create();
        }
            private static void DrawInnerRim(ksDocument2D doc2d)
        {
             doc2d.ksLineSeg(-2, 1, 2, 1, 1);
             doc2d.ksLineSeg(-2, 1, -2, 3, 1);
             doc2d.ksLineSeg(2, 3, 2, 1, 1);
             doc2d.ksLineSeg(-2, 3, -1, 3, 1);
             doc2d.ksLineSeg(2, 3, 1, 3, 1);
             doc2d.ksArcBy3Points(-1, 3, 0, 2.5, 1, 3, 1);
             doc2d.ksLineSeg(-2, 0, 2, 0, 3);
        }
        private static void DrawOuterRim(ksDocument2D doc2d)
        {
            doc2d.ksLineSeg(-2, 6, 2, 6, 1);
            doc2d.ksLineSeg(-2, 4, -2, 6, 1);
            doc2d.ksLineSeg(2, 6, 2, 4, 1);
            doc2d.ksLineSeg(-2, 4, -1, 4, 1);
            doc2d.ksLineSeg(2, 4, 1, 4, 1);
            doc2d.ksArcBy3Points(-1, 4, 0, 4.5, 1, 4, 1);
            doc2d.ksLineSeg(-2, 0, 2, 0, 3);
        }
        private static void DrawBalls(ksDocument2D doc2d)
        {
            doc2d.ksArcByAngle(0, 3.5, 1, -90, 90, 1, 1);
            doc2d.ksLineSeg(0, 4.5, 0, 2.5, 3);


        }
    }
}
