using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace beango.npoi
{
    public partial class DataImport : Form
    {
        public string file { get; set; }
        private int errnum { get; set; }
        private string fileNameExt;

        public DataImport()
        {
            file = "";
            errnum = 0;
            InitializeComponent();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                file = "";
                label1.Text = "";
                button2.Enabled = false;
                string resultFile = "";
                dataGridView1.DataSource = null;

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "All files (*.*)|*.*|excel files (*.xls,*.xlsx)|*.xls;*.xlsx";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    resultFile = openFileDialog1.FileName;
                //file = beango.util.UpLoadFile(resultFile, "fileupload/", true, label1);
                file = resultFile;
                if (file != "")
                {
                    fileNameExt = file.Substring(file.LastIndexOf(".") + 1);
                    BindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传文件异常：" + ex.Message);
            }
        }

        private bool c;
        /// <summary>
        /// 验证文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindData()
        {
            try
            {
                errnum = 0;
                c = false;
                InitializeWorkbook(file);
                if (fileNameExt == "xlsx")
                    ConvertToDataTable_xlsx();
                else// (fileNameExt == "xls")
                    ConvertToDataTable_xls();
                
                CheckCanSubmit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void CheckCanSubmit()
        {
            #region 验证
            bool isck = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                isck = validateData(row);
                if (!isck)
                    errnum++;
            }
            button2.Enabled = true;

            #endregion
        }

        IWorkbook workbook;

        void InitializeWorkbook(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                
                if (fileNameExt == "xlsx")
                    workbook = new XSSFWorkbook(file);
                else// (fileNameExt == "xls")
                    workbook = new HSSFWorkbook(file);
            }
        }

        void ConvertToDataTable_xls()
        {
            ISheet sheet = workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            int rowindex = 0;
            int cols = 0;
            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                if (rowindex == 0)
                {
                    cols = row.LastCellNum;
                    for (int i = 0; i < cols; i++)
                    {
                        ICell cell = row.GetCell(i);

                        if (cell == null)
                        {
                            dt.Columns.Add(Convert.ToChar(((int)'A') + i).ToString());
                        }
                        else
                        {
                            dt.Columns.Add(cell.ToString().Trim());
                        }
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < cols; i++)
                    {
                        ICell cell = row.GetCell(i);

                        #region 检查日期

                        DateTime _dt = DateTime.MinValue;
                        if (i == 1)
                        {
                            try
                            {
                                _dt = cell.DateCellValue;
                            }
                            catch (Exception)
                            {
                            }
                        }
                        #endregion
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            if (i == 1 && _dt != DateTime.MinValue)
                                dr[i] = _dt.ToString("yyyy-MM-dd");
                            else
                                dr[i] = cell.ToString().Trim();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                rowindex++;
            }
            bindgridview(dt);
        }

        void ConvertToDataTable_xlsx()
        {
            ISheet sheet = workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            int rowindex = 0;
            int cols = 0;
            while (rows.MoveNext())
            {
                IRow row = (XSSFRow)rows.Current;
                if (rowindex == 0)
                {
                    cols = row.LastCellNum;
                    for (int i = 0; i < cols; i++)
                    {
                        ICell cell = row.GetCell(i);

                        if (cell == null)
                        {
                            dt.Columns.Add(Convert.ToChar(((int)'A') + i).ToString());
                        }
                        else
                        {
                            dt.Columns.Add(cell.ToString().Trim());
                        }
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < cols; i++)
                    {
                        ICell cell = row.GetCell(i);

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                rowindex++;
            }
            bindgridview(dt);
        }

        private void bindgridview(DataTable dt)
        {
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                validateData(row);
            }
        }

        private bool validateData(DataGridViewRow row)
        {
            bool ischeck = true;
            try
            {
                if (row.Cells[0].Value.ToString().Trim() == "")//汇款银行
                {
                    row.Cells[0].Style.ForeColor = Color.Red;
                    row.Cells[0].ToolTipText = "汇款银行不能为空！";
                    ischeck = false;
                }
                else
                {
                    row.Cells[0].Style.ForeColor = Color.Black;
                }
                DateTime tmpdt;//日期
                if (!DateTime.TryParse(row.Cells[1].Value.ToString(), out tmpdt))
                {
                    row.Cells[1].Style.ForeColor = Color.Red;
                    ischeck = false;
                }
                else
                {
                    row.Cells[1].Style.ForeColor = Color.Black;
                }
                TimeSpan ts;//时间
                if (!TimeSpan.TryParse(row.Cells[2].Value.ToString(), out ts))
                {
                    row.Cells[2].Style.ForeColor = Color.Red;
                    ischeck = false;
                }
                else
                {
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
                decimal am;//金额
                if (!decimal.TryParse(row.Cells[4].Value.ToString().Trim(), out am))
                {
                    row.Cells[4].Style.ForeColor = Color.Red;
                    ischeck = false;
                }
                else
                {
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                if (row.Cells[6].Value.ToString().Trim() == "")//流水号
                {
                    row.Cells[6].Style.ForeColor = Color.Red;
                    row.Cells[6].ToolTipText = "流水号不能为空！";
                    ischeck = false;
                }
                else
                {
                    row.Cells[6].Style.ForeColor = Color.Black;
                }
            }
            catch (Exception)
            {
                ischeck = false;
            }
            foreach (DataGridViewCell subItem in row.Cells)
            {
                if (!ischeck)
                    subItem.Style.BackColor = Color.Yellow;
                else
                {
                    subItem.Style.BackColor = Color.White;
                }
            }
            return ischeck;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (errnum == 0)
                {
                    
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {

                    }
                    if (fileNameExt == "xls")
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                label1.Text = string.Format("数据保存失败：{0}", ex.Message + ex.StackTrace);
                label1.ForeColor = Color.Red;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            validateData(dataGridView1.Rows[e.RowIndex]);
            errnum = 0;
            CheckCanSubmit();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                validateData(row);
            }
        }
    }
}
