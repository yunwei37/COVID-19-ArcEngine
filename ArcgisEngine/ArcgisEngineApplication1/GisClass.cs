
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using stdole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcgisEngineApplication1
{
    static class GisClass
    {
        /// <summary>
        /// 打开mxd
        /// </summary>
        /// <returns></returns>
        public static string OpenMxd()
        {
            string MxdPath = "";
            OpenFileDialog OpenMXD = new OpenFileDialog();
            OpenMXD.Title = "打开地图";
            OpenMXD.InitialDirectory = "E:";

            OpenMXD.Filter = "Map Documents (*.mxd)|*.mxd";
            if (OpenMXD.ShowDialog() == DialogResult.OK)
            {
                MxdPath = OpenMXD.FileName;
            }
            return MxdPath;
        }

        /// <summary>
        /// 打开shapefile
        /// </summary>
        /// <returns></returns>
        public static string[] OpenShapeFile()
        {
            string[] ShpFile = new string[2];
            OpenFileDialog OpenShpFile = new OpenFileDialog();
            OpenShpFile.Title = "打开Shape文件";
            OpenShpFile.InitialDirectory = "D:";
            OpenShpFile.Filter = "Shape文件(*.shp)|*.shp";

            if (OpenShpFile.ShowDialog() == DialogResult.OK)
            {
                string ShapPath = OpenShpFile.FileName;
                //利用"\\"将文件路径分成两部分
                int Position = ShapPath.LastIndexOf("\\");

                string FilePath = ShapPath.Substring(0, Position);
                string ShpName = ShapPath.Substring(Position + 1);
                ShpFile[0] = FilePath;

                ShpFile[1] = ShpName;

            }
            else
            {
                return null;
            }
            return ShpFile;
        }

        /// <summary>
        /// 打开mdb
        /// </summary>
        /// <param name="pGDBName"></param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Geodatabase.IWorkspace GetMDBWorkspace(String _pGDBName)
        {
            IWorkspaceFactory pWsFac = new AccessWorkspaceFactoryClass();
            IWorkspace pWs = pWsFac.OpenFromFile(_pGDBName, 0);
            return pWs;

        }

        /// <summary>
        /// 打开文件地理数据库
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Geodatabase.IWorkspace GetFGDBWorkspace (String _pGDBName)
        {
          IWorkspaceFactory pWsFac = new FileGDBWorkspaceFactoryClass ();
          ESRI.ArcGIS.Geodatabase.IWorkspace pWs = pWsFac.OpenFromFile (_pGDBName, 0);
          return pWs;
        }

        /// <summary>
        /// 打开栅格
        /// </summary>
        /// <param name="pWsName"></param>
        /// <returns></returns>
        public static IRasterWorkspace GetRasterWorkspace(string pWsName)
        {
            try
            {
                IWorkspaceFactory pWorkFact = new RasterWorkspaceFactoryClass();
                return pWorkFact.OpenFromFile(pWsName, 0) as IRasterWorkspace;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 打开栅格数据集
        /// </summary>
        /// <param name="pFolderName"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static IRasterDataset OpenFileRasterDataset(string pFolderName, string pFileName)
        {
            IRasterWorkspace pRasterWorkspace = GetRasterWorkspace(pFolderName);
            IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pFileName);
            return pRasterDataset;
        }

        /// <summary>
        /// 根据图层创建属性表
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="taleName"></param>
        /// <returns></returns>
        public static DataTable createTable(ILayer pLayer, string taleName)
        {
            DataTable datatable = new DataTable(taleName);
            ITable pTable = pLayer as ITable;
            DataColumn dataColum;
            for (int i = 0; i < pTable.Fields.FieldCount; i++)
            {
                IField pField = pTable.Fields.get_Field(i);
                dataColum = new DataColumn(pField.Name);
                if (pField.Name == pTable.OIDFieldName)
                {
                    dataColum.Unique = true;
                }
                dataColum.AllowDBNull = pField.IsNullable;
                dataColum.Caption = pField.AliasName;
                dataColum.DataType = System.Type.GetType(ParseFieldType(pField.Type));
                dataColum.DefaultValue = pField.DefaultValue;
                if (pField.VarType == 8)
                {
                    dataColum.MaxLength = pField.Length;
                }
                datatable.Columns.Add(dataColum);

            }

            return datatable;
        }

        /// <summary>
        /// 获取图层类型
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        private static string GetShapeType(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            switch (pFeatureLayer.FeatureClass.ShapeType)
            {

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    return "Point";

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    return
                           "Polygon";

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    return "Polyline";

                default:
                    return "UNkonw";
            }
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        /// <param name="esriFieldType"></param>
        /// <returns></returns>
        public static string ParseFieldType(esriFieldType esriFieldType)//由于ersi中得类型和。net中的类型不一样，所以需要转化
        {
            switch (esriFieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    return "System.String";
                case esriFieldType.esriFieldTypeDate:
                    return "System.DateTime";
                case esriFieldType.esriFieldTypeDouble:
                    return "System.Double";
                case esriFieldType.esriFieldTypeGUID:
                    return "System.String";
                case esriFieldType.esriFieldTypeGeometry:
                    return "System.String";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "System.String";
                case esriFieldType.esriFieldTypeInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeOID:
                    return "System.Int32";

                case esriFieldType.esriFieldTypeSingle:
                    return "System.Single";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeString:
                    return "System.String";

                default:
                    return "";
            }

        }

        /// <summary>
        /// 填充图表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable fillTable(ILayer player,string tableName)
        {
            DataTable datable = createTable(player, tableName);
            ITable pTable = player as ITable;
            string shape = GetShapeType(player);
            ICursor pCursor = pTable.Search(null, false);
            IRow pRow = pCursor.NextRow();
            while (pRow != null)
            {
                DataRow dataRow = datable.NewRow();
                for (int i = 0; i < pRow.Fields.FieldCount; i++)
                {
                    if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        dataRow[i] = shape;
                    }
                    else if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        dataRow[i] = "element";
                    }
                    else
                    {
                        dataRow[i] = pRow.get_Value(i);
                    }
                }
                datable.Rows.Add(dataRow);
                if (datable.Rows.Count > 200)
                {
                    break;
                }
                else
                {
                    pRow = pCursor.NextRow();//Pcursor先当于链表中移动的那个针，生成是头指针；
                }

            }
            return datable;
        }

        public static void  UniqueValueRender (IActiveView activeView, IFeatureLayer pFtLayer, int pCount, string pFieldName)

        {

          IGeoFeatureLayer pGeoFeaturelayer = pFtLayer as IGeoFeatureLayer;
          IUniqueValueRenderer pUnique = new UniqueValueRendererClass ();
          pUnique.FieldCount = 1;
           
            //遍历，根据code获取当前确诊人数
            //先获取图层的要素个数

          IFeatureCursor pFtCursor = pFtLayer.FeatureClass.Search(null, false);
          IFeature pFt = pFtCursor.NextFeature();
          pUnique.set_Field(0, pFieldName);
          //while (pFt != null) {
          //    int index = pFt.Fields.FindField(pFieldName);
          //    String code = pFt.get_Value(index).ToString();
          //    DataRow[] drs=dt.Select("code='" + code+"'");
          //    String num = drs[0]["CurConfirmeed"].ToString();
          //    pUnique.set_Value(0,num);
          //}
          

         
         

          ISimpleFillSymbol pSimFill = new SimpleFillSymbolClass ();

          //给颜色

          //IFeatureCursor pFtCursor = pFtLayer.FeatureClass.Search (null, false);
          //IFeature pFt = pFtCursor.NextFeature ();

          IFillSymbol pFillSymbol1;

          ////添加第一个符号

          //pFillSymbol1 = new SimpleFillSymbolClass();

          //pFillSymbol1.Color = GetRGBColor(103, 252, 179) as IColor;

          ////添加第二个符号

          //IFillSymbol pFillSymbol2 = new SimpleFillSymbolClass();

          //pFillSymbol2.Color = GetRGBColor(125, 155, 251) as IColor;

          //创建并设置随机色谱从上面的的图可以看出我们要给每一个值定义一种颜色,我 们可以创建色谱,但是色谱的这些参数

          IRandomColorRamp pColorRamp = new RandomColorRampClass ();
          pColorRamp.StartHue = 0;

          pColorRamp.MinValue = 0;

          pColorRamp.MinSaturation = 0;

          pColorRamp.EndHue = 360;

          pColorRamp.MaxValue = 100;

          pColorRamp.MaxSaturation = 100;
          pColorRamp.Size = pCount;

          //pColorRamp.Size = pUniqueValueRenderer.ValueCount; bool ok = true;
          bool ok = true;
          pColorRamp.CreateRamp (out ok);
          IEnumColors pEnumRamp = pColorRamp.Colors;

          //IColor pColor = pEnumRamp.Next();

          int pIndex = pFt.Fields.FindField (pFieldName);

          //因为我只有24条记录,所以改变这些,这些都不会超过255或者为负数.求余 

          int i = 0;

          while (pFt != null)

          {

            IColor pColor = pEnumRamp.Next ();
            if (pColor == null)

            {

              pEnumRamp.Reset ();

              pColor = pEnumRamp.Next ();

            }

            //以下注释代码为自定义的两种颜色 ,如果不使用随机的颜色,可以采用这样的

            //if (i % 2 == 0)

            //{

            // pUnique.AddValue(Convert.ToString(pFt.get\_Value(pIndex)) , pFieldName, pFillSymbol1 as ISymbol);

            //}

            //else

            //{

            // pUnique.AddValue(Convert.ToString(pFt.get\_Value(pIndex)) , pFieldName, pFillSymbol2 as ISymbol);

            //}

            //i++;

            pFillSymbol1 = new SimpleFillSymbolClass ();
            int R=Convert.ToInt32(pFt.get_Value(pIndex).ToString());
            if(R>0&&R<10){
                pFillSymbol1.Color=GetRgbColor(255,255,255);
            }else if(R>=10&&R<=100){
                pFillSymbol1.Color = GetRgbColor(255, 200, 200);
            }else if(R>=100&&R<=1000){
                pFillSymbol1.Color = GetRgbColor(255, 155, 155);
            }else if(R>=1000&&R<=10000){
                pFillSymbol1.Color=GetRgbColor(255,100,100);
            }
            else if (R >= 10000 && R <= 50000)
            {
                pFillSymbol1.Color = GetRgbColor(255, 55, 55);
            } else
            {
                pFillSymbol1.Color = GetRgbColor(255, 0, 0);
            }

            pUnique.AddValue(Convert.ToString(pFt.get_Value(pIndex)), pFieldName, pFillSymbol1 as ISymbol);

            pFt = pFtCursor.NextFeature ();

            // pColor = pEnumRamp.Next();

          }

          pGeoFeaturelayer.Renderer = pUnique as IFeatureRenderer;

            activeView.PartialRefresh (esriViewDrawPhase.esriViewGeography, null, null);

        }

        //添加图层标注
        public static void EnableFeatureLayerLabel(IFeatureLayer pFeaturelayer, string sLableField, IRgbColor pRGB, int size, string angleField)
        {
            //判断图层是否为空
            if (pFeaturelayer == null)
                return;
            IGeoFeatureLayer pGeoFeaturelayer = (IGeoFeatureLayer)pFeaturelayer;
            IAnnotateLayerPropertiesCollection pAnnoLayerPropsCollection;
            pAnnoLayerPropsCollection = pGeoFeaturelayer.AnnotationProperties;
            pAnnoLayerPropsCollection.Clear();

            //stdole.IFontDisp  pFont; //字体
            ITextSymbol pTextSymbol;

            //pFont.Name = "新宋体";
            //pFont.Size = 9;
            //未指定字体颜色则默认为黑色
            if (pRGB == null)
            {
                pRGB = new RgbColorClass();
                pRGB.Red = 0;
                pRGB.Green = 0;
                pRGB.Blue = 0;
            }

            pTextSymbol = new TextSymbolClass();
            pTextSymbol.Color = (IColor)pRGB;
            pTextSymbol.Size = size; //标注大小

            IBasicOverposterLayerProperties4 pBasicOverposterlayerProps4 = new BasicOverposterLayerPropertiesClass();
            switch (pFeaturelayer.FeatureClass.ShapeType)//判断图层类型
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPoint;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
                    break;
            }
            pBasicOverposterlayerProps4.PointPlacementMethod = esriOverposterPointPlacementMethod.esriRotationField;
            pBasicOverposterlayerProps4.RotationField = angleField;

            ILabelEngineLayerProperties pLabelEnginelayerProps = new LabelEngineLayerPropertiesClass();
            pLabelEnginelayerProps.Expression = "[" + sLableField + "]";
            pLabelEnginelayerProps.Symbol = pTextSymbol;
            pLabelEnginelayerProps.BasicOverposterLayerProperties = pBasicOverposterlayerProps4 as IBasicOverposterLayerProperties;
            pAnnoLayerPropsCollection.Add((IAnnotateLayerProperties)pLabelEnginelayerProps);
            pGeoFeaturelayer.DisplayAnnotation = true;//很重要，必须设置 
            //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null); }
        }


        #region RGB颜色
        /// <summary>
        /// RGB颜色设置
        /// </summary>
        /// <param name="intR"></param>
        /// <param name="intG"></param>
        /// <param name="intB"></param>
        /// <returns></returns>
        public static IRgbColor GetRgbColor(int intR, int intG, int intB)
        {
            IRgbColor pRgbColor = null;
            if (intR < 0 || intR > 255 || intG < 0 || intG > 255 || intB < 0 || intB > 255)
            {
                return pRgbColor;
            }
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = intR;
            pRgbColor.Green = intG;
            pRgbColor.Blue = intB;
            return pRgbColor;
        }
        #endregion


    }
}
