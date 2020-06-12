using ArcgisEngineApplication1.ToolClass;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolClass.Operation;

namespace ArcgisEngineApplication1
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IFeatureLayer pCurrentLayer;
        private IStyleGalleryItem pStyleGalleryItem;
        private INewEnvelopeFeedback pNewEnvelopeFeedback; 
        private EnumMapSurroundType _enumMapSurType = EnumMapSurroundType.None;
        private IPoint m_MovePt = null;
        private IPoint m_PointPt = null;     
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开mxd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openMxd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String MxdPath=GisClass.OpenMxd();
            axMapControl1.LoadMxFile(MxdPath);
        }

 

        private void openMdb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IWorkspace workSpace = GisClass.GetMDBWorkspace("aaa");
        }

        private void openGDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IWorkspace workSpace = GisClass.GetFGDBWorkspace("aaa");
        }

        private void shapeQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.Map.ClearSelection();
            IGraphicsContainer graphicsContainer = axMapControl1.Map as IGraphicsContainer;
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            //获取图形几何信息
            if (element == null) {
                MessageBox.Show("请在工具栏选择绘制矩形，多边形，或者圆");
                return;
            }
            IGeometry geometry = element.Geometry;
            axMapControl1.Map.SelectByShape(geometry,null,false);
            //进行部分刷新显示最新要素
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection,null,axMapControl1.ActiveView.Extent);


        }

        private void clearShape_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IGraphicsContainer graphicsContainer = axMapControl1.Map as IGraphicsContainer;
            graphicsContainer.DeleteAllElements();
            this.axMapControl1.Map.ClearSelection();
            this.axMapControl1.ActiveView.Refresh();
        }

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 1) //左键
            {
                esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap pMap = null; object unk = null;
                object data = null; 
                ILayer pLayer = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                pCurrentLayer = pLayer as IFeatureLayer;
                return;
            }    
             else if (e.button == 2)
             {
                 esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap pMap = null; object unk = null;
                object data = null; 
                 ILayer pLayer = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                pCurrentLayer = pLayer as IFeatureLayer;
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer && pCurrentLayer != null)
                {
                    contextMenuStrip1.Show(Control.MousePosition);
                }
             }
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurrentLayer != null)
            {
                AttrForm attrForm = new AttrForm(pCurrentLayer);
                attrForm.ShowDialog();
            }
            
        }

        private void selection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            SelectionForm selectionForm = new SelectionForm(this.axMapControl1.Map);
            selectionForm.ShowDialog();
        }

        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.axMapControl1.Map.DeleteLayer(pCurrentLayer);
        }

        private void addLegend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _enumMapSurType = EnumMapSurroundType.Legend;
        }

        /// <summary>
        /// 添加地图整饰要素
        /// </summary>
        /// <param name="pAV"></param>
        /// <param name="_enumMapSurroundType"></param>
        /// <param name="pEnvelope"></param>
        private void AddMapSurround(IActiveView pAV, EnumMapSurroundType _enumMapSurroundType, IEnvelope pEnvelope)
        {
            try
            {
                switch (_enumMapSurroundType)
                {
                    case EnumMapSurroundType.NorthArrow:
                        addNorthArrow(this.axPageLayoutControl1.PageLayout, pEnvelope, pAV);
                        break;
                    case EnumMapSurroundType.ScaleBar:
                        makeScaleBar(pAV, this.axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                    case EnumMapSurroundType.Legend:
                        MakeLegend(pAV, this.axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region 添加图例
        /// <summary>
        /// 添加图例
        /// </summary>
        /// <param name="activeView"></活动窗口>
        /// <param name="pageLayout"></布局窗口>
        /// <param name="pEnv"></包络线>
        private void MakeLegend(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {

            UID pID = new UID();
            pID.Value = "esriCarto.Legend";
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(pID, null);//根据唯一标示符，创建与之对应MapSurroundFrame
            IElement pDeletElement = this.axPageLayoutControl1.FindElementByName("Legend");//获取PageLayout中的图例元素
            if (pDeletElement != null)
            {
                pGraphicsContainer.DeleteElement(pDeletElement);  //如果已经存在图例，删除已经存在的图例
            }
            //设置MapSurroundFrame背景
            ISymbolBackground pSymbolBackground = new SymbolBackgroundClass();
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Color = GisClass.GetRgbColor(0, 0, 0);
            pFillSymbol.Color = GisClass.GetRgbColor(240, 240, 240);
            pFillSymbol.Outline = pLineSymbol;
            pSymbolBackground.FillSymbol = pFillSymbol;
            pMapSurroundFrame.Background = pSymbolBackground;
            //添加图例
            IElement pElement = pMapSurroundFrame as IElement;
            pElement.Geometry = pEnv as IGeometry;
            IMapSurround pMapSurround = pMapSurroundFrame.MapSurround;
            ILegend pLegend = pMapSurround as ILegend;
            pLegend.ClearItems();
            pLegend.Title = "图例";
            for (int i = 0; i < pActiveView.FocusMap.LayerCount; i++)
            {
                ILegendItem pLegendItem = new HorizontalLegendItemClass();
                pLegendItem.Layer = pActiveView.FocusMap.get_Layer(i);//获取添加图例关联图层             
                pLegendItem.ShowDescriptions = false;
                pLegendItem.Columns = 1;
                pLegendItem.ShowHeading = true;
                pLegendItem.ShowLabels = true;
                pLegend.AddItem(pLegendItem);//添加图例内容
            }
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);  
        }
        #endregion
         #region 指北针
        /// <summary>
        /// 插入指北针
        /// </summary>
        /// <param name="pPageLayout"></param>
        /// <param name="pEnv"></param>
        /// <param name="pActiveView"></param>
        void addNorthArrow(IPageLayout pPageLayout, IEnvelope pEnv, IActiveView pActiveView)
        {
            if (pPageLayout == null || pActiveView == null)
            {
                return;
            }
         

            ESRI.ArcGIS.esriSystem.IUID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = "esriCarto.MarkerNorthArrow";

            // Create a Surround. Set the geometry of the MapSurroundFrame to give it a location
            // Activate it and add it to the PageLayout's graphics container
            ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer = pPageLayout as ESRI.ArcGIS.Carto.IGraphicsContainer; // Dynamic Cast
            ESRI.ArcGIS.Carto.IActiveView activeView = pPageLayout as ESRI.ArcGIS.Carto.IActiveView; // Dynamic Cast
            ESRI.ArcGIS.Carto.IFrameElement frameElement = graphicsContainer.FindFrame(pActiveView.FocusMap);
            ESRI.ArcGIS.Carto.IMapFrame mapFrame = frameElement as ESRI.ArcGIS.Carto.IMapFrame; // Dynamic Cast
            ESRI.ArcGIS.Carto.IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uid as ESRI.ArcGIS.esriSystem.UID, null); // Dynamic Cast
            ESRI.ArcGIS.Carto.IElement element = mapSurroundFrame as ESRI.ArcGIS.Carto.IElement; // Dynamic Cast
            element.Geometry = pEnv;
            element.Activate(activeView.ScreenDisplay);
            graphicsContainer.AddElement(element, 0);
            ESRI.ArcGIS.Carto.IMapSurround mapSurround = mapSurroundFrame.MapSurround;

            // Change out the default north arrow
            ESRI.ArcGIS.Carto.IMarkerNorthArrow markerNorthArrow = mapSurround as ESRI.ArcGIS.Carto.IMarkerNorthArrow; // Dynamic Cast
            ESRI.ArcGIS.Display.IMarkerSymbol markerSymbol = markerNorthArrow.MarkerSymbol;
            ESRI.ArcGIS.Display.ICharacterMarkerSymbol characterMarkerSymbol = markerSymbol as ESRI.ArcGIS.Display.ICharacterMarkerSymbol; // Dynamic Cast
            characterMarkerSymbol.CharacterIndex = 200; // change the symbol for the North Arrow
            markerNorthArrow.MarkerSymbol = characterMarkerSymbol;
        }
        #endregion
        #region  比例尺
        /// <summary>
        /// 比例尺
        /// </summary>
        /// <param name="pActiveView"></param>
        /// <param name="pPageLayout"></param>
        /// <param name="pEnv"></param>
        public void makeScaleBar(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {
            IGraphicsContainer container = pPageLayout as IGraphicsContainer;
            // 获得MapFrame  
            IFrameElement frameElement = container.FindFrame(pActiveView.FocusMap);
            IMapFrame mapFrame = frameElement as IMapFrame;
            //根据MapSurround的uid，创建相应的MapSurroundFrame和MapSurround  
            UID uid = new UIDClass();
            uid.Value = "esriCarto.AlternatingScaleBar";
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uid, null);
            //设置MapSurroundFrame中比例尺的样式  
            IMapSurround mapSurround = mapSurroundFrame.MapSurround;
            IScaleBar markerScaleBar = ((IScaleBar)mapSurround);
            markerScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            markerScaleBar.UseMapSettings();
            //QI，确定mapSurroundFrame的位置  
            IElement element = mapSurroundFrame as IElement;

            element.Geometry = pEnv;
            //使用IGraphicsContainer接口添加显示  
            container.AddElement(element, 0);
            pActiveView.Refresh();  
        }
        #endregion      

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            try
            {
                if (_enumMapSurType != EnumMapSurroundType.None)
                {
                    IActiveView pActiveView = null;
                    pActiveView = this.axPageLayoutControl1.PageLayout as IActiveView;
                    m_PointPt = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    if (pNewEnvelopeFeedback == null)
                    {
                        pNewEnvelopeFeedback = new NewEnvelopeFeedbackClass();
                        pNewEnvelopeFeedback.Display = pActiveView.ScreenDisplay;
                        pNewEnvelopeFeedback.Start(m_PointPt);
                    }
                    else
                    {
                        pNewEnvelopeFeedback.MoveTo(m_PointPt);
                    }

                }
            }
            catch
            {
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _enumMapSurType = EnumMapSurroundType.NorthArrow;
        }
        private void copyToPageLayout()
        {
            //IObjectCopy接口提供Copy方法用于地图的复制
            IObjectCopy objectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;
            object copyMap = objectCopy.Copy(copyFromMap);
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            //Overwrite方法用于地图写入PageLayoutControl控件的视图中
            objectCopy.Overwrite(copyMap, ref copyToMap);
        }
        /// <summary>
        /// 地图控件与页面视图控件关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            copyToPageLayout();
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView activeView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;
            //根据MapControl的视图范围,确定PageLayoutControl的视图范围
            displayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh();
            copyToPageLayout();
        }

        private void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            try
            {
                if (_enumMapSurType != EnumMapSurroundType.None)
                {
                    if (pNewEnvelopeFeedback != null)
                    {
                        m_MovePt = (this.axPageLayoutControl1.PageLayout as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        pNewEnvelopeFeedback.MoveTo(m_MovePt);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void axPageLayoutControl1_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            if (_enumMapSurType != EnumMapSurroundType.None)
            {
                if (pNewEnvelopeFeedback != null)
                {
                    IActiveView pActiveView = null;
                    pActiveView = this.axPageLayoutControl1.PageLayout as IActiveView;
                    IEnvelope pEnvelope = pNewEnvelopeFeedback.Stop();
                    AddMapSurround(pActiveView, _enumMapSurType, pEnvelope);
                    pNewEnvelopeFeedback = null;
                    _enumMapSurType = EnumMapSurroundType.None;
                }
            }
        }

        private void addScaleBar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _enumMapSurType = EnumMapSurroundType.ScaleBar;
        }

        /// <summary>
        /// 导出为图片
        /// </summary>
        private void ExportMapToImage()
        {
            try
            {
                SaveFileDialog pSaveDialog = new SaveFileDialog();
                pSaveDialog.FileName = "";
                pSaveDialog.Filter = "JPG图片(*.JPG)|*.jpg|tif图片(*.tif)|*.tif|PDF文档(*.PDF)|*.pdf";
                if (pSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    double iScreenDispalyResolution =this.axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;// 获取屏幕分辨率的值
                    IExporter pExporter = null;
                    if (pSaveDialog.FilterIndex == 1)
                    {
                        pExporter = new JpegExporterClass();
                    }
                    else if (pSaveDialog.FilterIndex == 2)
                    {
                        pExporter = new TiffExporterClass();
                    }
                    else if (pSaveDialog.FilterIndex == 3)
                    {
                        pExporter = new PDFExporterClass();
                    }
                    pExporter.ExportFileName = pSaveDialog.FileName;
                    pExporter.Resolution = (short)iScreenDispalyResolution; //分辨率
                    tagRECT deviceRect = this.axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                    IEnvelope pDeviceEnvelope = new EnvelopeClass();
                    pDeviceEnvelope.PutCoords(deviceRect.left, deviceRect.bottom, deviceRect.right, deviceRect.top);
                    pExporter.PixelBounds = pDeviceEnvelope; // 输出图片的范围
                    ITrackCancel pCancle = new CancelTrackerClass();//可用ESC键取消操作
                    this.axPageLayoutControl1.ActiveView.Output(pExporter.StartExporting(), pExporter.Resolution, ref deviceRect, this.axPageLayoutControl1.ActiveView.Extent, pCancle);
                    Application.DoEvents();
                    pExporter.FinishExporting();
                }

            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "输出图片", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exportToImg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportMapToImage();
        }

        private void openSHp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String[] shpFile = GisClass.OpenShapeFile();
            if (shpFile != null)
            {
                this.axMapControl1.AddShapeFile(shpFile[0], shpFile[1]);
            }
        }


        //加载属性数据
        private void Form1_Load(object sender, EventArgs e)
        {
            ArrayList arr=new ArrayList();
            DataTable dt = OperateDatabase.select("data",arr);
            this.gridControl1.DataSource = dt;
        }

        /// <summary>
        /// 显示每日疫情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //查询每日疫情
            ArrayList arr = new ArrayList();
            //获取日期
            if(this.selectedDate.EditValue==null){
                MessageBox.Show("请选择日期");
                return;
            }
            arr.Add("YMD:'"+this.selectedDate.EditValue+"'");
            DataTable dt = OperateDatabase.select("data",arr);
            if (dt.Rows.Count==0) {
                MessageBox.Show("当日无数据");
                return;
            }
            //为当前图层添加字段
            IFeatureLayer layer=null;
            //遍历，寻找市域图层
            for (int i = 0; i < this.axMapControl1.Map.LayerCount; i++) {
                ILayer layer1 = this.axMapControl1.Map.get_Layer(i);
                if (layer1.Name == "市域")
                {
                    layer = layer1 as IFeatureLayer;
                    break;
                }
            }
            if (layer == null) {
                MessageBox.Show("请打开市域图层");
                return;
            }  
            //获取图层字段，没有则添加一个num字段
            IFeatureClass featureClass = layer.FeatureClass;
            int isExist=featureClass.FindField("num");
            if (isExist == -1) { 
                //添加一个字段
                IFields pFields = featureClass.Fields;
                IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
                IField fld = new FieldClass();
                IFieldEdit2 fldE = fld as IFieldEdit2;
                fldE.Name_2 = "num";
                fldE.AliasName_2 = "数量";
                fldE.Type_2 = esriFieldType.esriFieldTypeSingle;
                featureClass.AddField(fld);
            }
            //给字段赋值
            IFeatureCursor pFtCursor = featureClass.Search(null, false);
            IFeature pFt = pFtCursor.NextFeature();
            int index1 = pFt.Fields.FindField("num");
            IDataset dataset = (IDataset)featureClass;
            IWorkspace workspace = dataset.Workspace;
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();
            while (pFt != null) {
                int index = pFt.Fields.FindField("code");
                String code = pFt.get_Value(index).ToString();

                DataRow[] drs=dt.Select("CODE=" + code);
                DataTable dtNew = dt.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    dtNew.ImportRow(drs[i]);

                }
                String num = dtNew.Rows[0]["AllConfiemed"].ToString();
                if (num == "") {
                    num = "0";
                }

                pFt.set_Value(index1,  Convert.ToInt32(num));
                pFt.Store();
                pFt = pFtCursor.NextFeature();
            }

            GisClass.UniqueValueRender(this.axMapControl1.ActiveView,layer,10,"num");
        }

        private void 添加标注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IRgbColor rgb=GisClass.GetRgbColor(0,0,0);
            GisClass.EnableFeatureLayerLabel(pCurrentLayer, "NAME", rgb,10,"");
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null); 
        } 
       
    }
}
