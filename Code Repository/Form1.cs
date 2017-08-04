using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System;

namespace Code_Repository
{
    public partial class frmCdeRep : Form
    {

        String mainpath,FiLeNme,ProjNme;
        
        public frmCdeRep()
        {
            InitializeComponent();
        }

        private void frmCdeRep_Load(object sender, EventArgs e)
        {

            TSSLocation.Text = @"c:\temp\old\Proj\";//Application.StartupPath.ToString();
            mainpath = @"c:\temp\old\Proj\";
            rdbNet.Checked = true;

        }

        private void cmdLoc_Click(object sender, EventArgs e)
        {

            txtCode.Clear();
            txtProjNme.Clear();
                        rdbCS.Checked = true;

        }

        private void cmdAdd_Click(object sender, System.EventArgs e)
        {




            if ( txtProjNme.Text!="" && txtCode.Text!="")
            {

                ProjNme = txtProjNme.Text;
                


                if (rdbCS.Checked)
                {
                    FiLeNme = ProjNme + @"\" + txtProjNme.Text + ".cs";
                }
                else
                {
                    FiLeNme = ProjNme + @"\" + txtProjNme.Text + ".vb";
                }


                try
                {

                    if (txtProjNme.Text != "")
                    {
                        if (!Directory.Exists(mainpath + @"\" + ProjNme))
                        {
                            Directory.CreateDirectory(mainpath  + ProjNme);
                        }

                        if (!File.Exists(mainpath + @"\" + ProjNme))
                        {
                            using (StreamWriter sw = File.CreateText(mainpath + @"\" + FiLeNme))
                            {
                                sw.Write(txtCode.Text);

                            }
                        }
                        else {

                                using (StreamWriter sw = File.CreateText(mainpath + @"\parts" + FiLeNme))
                                {
                                    sw.Write(txtCode.Text);

                                }
                        
                        
                            }
                        
                        
                        MessageBox.Show("new file created in " + mainpath  + FiLeNme);

                    }

                }
                catch (System.Exception)
                {

                    throw;
                }

                
            }
        }


    }
}
