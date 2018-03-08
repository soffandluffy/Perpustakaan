using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace Perpustakaan
{
    public partial class Form1 : Form
    {

        MySqlConnection koneksi = new MySqlConnection("server = localhost; database = perpus_soffan; uid = root; pwd = ;");
        MySqlCommand cmd;
        MySqlCommand cmd2;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;
        int idterpilih = 1;


        public Form1()
        {
            InitializeComponent();

            DGVB();
            DGVS();
            DGVP();
            DGVUS();
            CBSTOK();
            CBPINJAM();

            //GenerateBukuCode();
            DateTime today = System.DateTime.Now;
            label15.Text = today.ToShortDateString();
        }

        public void DGVB()
        {
            try
            {
                DataTable dt = new DataTable();
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM daftar_buku", koneksi);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvBuku.DataSource = dt.DefaultView;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                koneksi.Close();
            }
        }

        public void DGVS()
        {
            try
            {
                DataTable dt = new DataTable();
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM stok_buku", koneksi);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvStokBuku.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        public void DGVP()
        {
            try
            {
                DataTable dt = new DataTable();
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM peminjaman", koneksi);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvPeminjaman.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        public void DGVUS()
        {
            try
            {
                DataTable dt = new DataTable();
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM user", koneksi);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvUser.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        public void CBSTOK()
        {
            try
            {
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM daftar_buku", koneksi);
                reader = cmd.ExecuteReader();
                cbJudulBuku.Items.Clear();
                while (reader.Read())
                {

                    cbJudulBuku.Items.Add(reader["judul_buku"].ToString());

                }
                
                
            } catch
            {

            } finally
            {
                reader.Close();
                koneksi.Close();
            }
        }



        public void CBPINJAM()
        {
            try
            {
                koneksi.Open();
                cmd = new MySqlCommand("SELECT *FROM daftar_buku", koneksi);
                reader = cmd.ExecuteReader();
                cbJudulBukuP.Items.Clear();
                while (reader.Read())
                {

                    cbJudulBukuP.Items.Add(reader["judul_buku"].ToString());

                }


            }
            catch
            {

            }
            finally
            {
                reader.Close();
                koneksi.Close();
            }
        }

        public void clear()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is MaskedTextBox)
                        (control as MaskedTextBox).Clear();
                    else if (control is ComboBox)
                        (control as ComboBox).Text = "";
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("INSERT INTO daftar_buku(kode_buku,judul_buku,pengarang,kategori) VALUES(@kode_buku,@judul_buku,@pengarang,@kategori)", koneksi);
                cmd.Parameters.Add("@kode_buku", tbKodeBuku.Text);
                cmd.Parameters.Add("@judul_buku", tbJudulBuku.Text);
                cmd.Parameters.Add("@pengarang", tbPengarang.Text);
                cmd.Parameters.Add("@kategori", tbKategori.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Book added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                clear();
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                koneksi.Close();
                DGVB();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("UPDATE daftar_buku SET kode_buku = @kode_buku, judul_buku = @judul_buku, pengarang = @pengarang , kategori = @kategori  WHERE id = '"+ idterpilih +"' ", koneksi);
                cmd.Parameters.Add("@kode_buku", tbKodeBuku.Text);
                cmd.Parameters.Add("@judul_buku", tbJudulBuku.Text);
                cmd.Parameters.Add("@pengarang", tbPengarang.Text);
                cmd.Parameters.Add("@kategori", tbKategori.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Book updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                clear();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVB();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("DELETE FROM daftar_buku  WHERE id = '" + idterpilih + "' ", koneksi);
                if (MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Book deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                } else
                {

                }
                
                clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVB();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DGVB();
            clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (tbUsername.Text == "" || tbPassword.Text == "")
                {
                    MessageBox.Show("Please fill all fields", "Warning" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    koneksi.Open();
                    cmd = new MySqlCommand("SELECT *FROM user WHERE username = '" + tbUsername.Text + "' AND password = '" + tbPassword.Text + "'", koneksi);
                    DataSet ds = new DataSet();
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (reader["status"].ToString() == "admin")
                        {
                            panelLogin.Width = 0;
                            btnLogout.Visible = true;
                        }
                        else
                        {
                            tabControl1.TabPages.Remove(tabPage1);
                            tabControl1.TabPages.Remove(tabPage2);
                            tabControl1.TabPages.Remove(tabPage4);
                            panelLogin.Width = 0;
                            btnLogout.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is inccorect!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    }
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                koneksi.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("INSERT INTO stok_buku(judul_buku,nomor_rak,jumlah_buku) VALUES(@judul_buku,@nomor_rak,@jumlah_buku)", koneksi);
                cmd.Parameters.Add("@judul_buku", cbJudulBuku.Text);
                cmd.Parameters.Add("@nomor_rak", Convert.ToInt32(tbNoRak.Text));
                cmd.Parameters.Add("@jumlah_buku", Convert.ToInt32(tbJumlahBuku.Text));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Stock added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVS();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("UPDATE stok_buku SET nomor_rak = @nomor_rak,jumlah_buku= @jumlah_buku WHERE judul_buku = @judul_buku", koneksi);
                cmd.Parameters.Add("@judul_buku", cbJudulBuku.Text);
                cmd.Parameters.Add("@nomor_rak", tbNoRak.Text);
                cmd.Parameters.Add("@jumlah_buku", tbJumlahBuku.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Stock updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVS();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DGVS();
            clear();
        }

        private void dgvBuku_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvStokBuku_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvPeminjaman_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBuku_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvBuku.Rows[e.RowIndex];

            idterpilih = Convert.ToInt32(dr.Cells[0].Value.ToString());
            tbKodeBuku.Text = dr.Cells[1].Value.ToString();
            tbJudulBuku.Text = dr.Cells[2].Value.ToString();
            tbPengarang.Text = dr.Cells[3].Value.ToString();
            tbKategori.Text = dr.Cells[4].Value.ToString();
        }

        private void dgvStokBuku_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvStokBuku.Rows[e.RowIndex];
            
            cbJudulBuku.Text = dr.Cells[0].Value.ToString();
            tbNoRak.Text = dr.Cells[1].Value.ToString();
            tbJumlahBuku.Text = dr.Cells[2].Value.ToString();
        }

        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvPeminjaman.Rows[e.RowIndex];

            

            idterpilih = Convert.ToInt32(dr.Cells[0].Value.ToString());
            tbNamaPeminjam.Text = dr.Cells[1].Value.ToString();
            tbAlamatPeminjam.Text = dr.Cells[2].Value.ToString();
            cbJudulBukuP.Text = dr.Cells[3].Value.ToString();
            DateTime clicked = Convert.ToDateTime(dr.Cells[4].Value.ToString());
            label15.Text = clicked.ToShortDateString();
            cbStatus.Text = "Kembali";
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (cbStatus.Text == "Pinjam")
                {
                    koneksi.Open();
                    cmd = new MySqlCommand("INSERT INTO peminjaman(nama_peminjam,alamat_peminjam,judul_buku,tanggal_pinjam,status_peminjaman) VALUES(@nama_peminjam,@alamat_peminjam,@judul_buku,@tanggal_pinjam,@status_peminjaman)", koneksi);
                    cmd.Parameters.Add("@nama_peminjam", tbNamaPeminjam.Text);
                    cmd.Parameters.Add("@alamat_peminjam", tbAlamatPeminjam.Text);
                    cmd.Parameters.Add("@judul_buku", cbJudulBukuP.Text);
                    cmd.Parameters.Add("@tanggal_pinjam", Convert.ToDateTime(label15.Text));
                    cmd.Parameters.Add("@status_peminjaman", cbStatus.Text);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Success !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else if (cbStatus.Text == "Kembali")
                {
                    koneksi.Open();
                    cmd = new MySqlCommand("UPDATE peminjaman SET tanggal_kembali = @tanggal_kembali,status_peminjaman = @status_peminjaman WHERE id = '" + idterpilih +"'", koneksi);
                    cmd.Parameters.Add("@tanggal_kembali", Convert.ToDateTime(label15.Text));
                    cmd.Parameters.Add("@status_peminjaman", cbStatus.Text);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Success !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVP();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DGVP();
            clear();
        }

        private void cbJudulBuku_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            koneksi.Open();
            cmd = new MySqlCommand("select * from stok_buku", koneksi);
            // query = string.Format("select * from pegawai");
            //perintah = new MySqlCommand(query, koneksi);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            // adapter = new MySqlDataAdapter(perintah);
            cmd.ExecuteNonQuery();
            ds.Clear();
            da.Fill(ds);
            koneksi.Close();

            ReportDocument rd = new ReportDocument();
            rd.Load("../../CrystalReport1.rpt"); // sesuaikan dengan nama dan lokasi file report yang ente buat
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            Form2 l = new Form2(); //sesuaikan dengan nama form yang ente jadikan report viewer
            l.crystalReportViewer1.ReportSource = rd;
            l.ShowDialog();
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            koneksi.Open();
            cmd = new MySqlCommand("select * from peminjaman", koneksi);
            // query = string.Format("select * from pegawai");
            //perintah = new MySqlCommand(query, koneksi);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            // adapter = new MySqlDataAdapter(perintah);
            cmd.ExecuteNonQuery();
            ds.Clear();
            da.Fill(ds);
            koneksi.Close();

            ReportDocument rd = new ReportDocument();
            rd.Load("../../CrystalReport2.rpt"); // sesuaikan dengan nama dan lokasi file report yang ente buat
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            Form2 l = new Form2(); //sesuaikan dengan nama form yang ente jadikan report viewer
            l.crystalReportViewer1.ReportSource = rd;
            l.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("INSERT INTO user(username,password,status) VALUES(@username,@password,@status)", koneksi);
                cmd.Parameters.Add("@username", username.Text);
                cmd.Parameters.Add("@password", password.Text);
                cmd.Parameters.Add("@status", status.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("User added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVUS();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("UPDATE user SET password = @password, status = @status WHERE username = @username ", koneksi);
                cmd.Parameters.Add("@username", username.Text);
                cmd.Parameters.Add("@password", password.Text);
                cmd.Parameters.Add("@status", status.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("User updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                clear();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVUS();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {

                koneksi.Open();
                cmd = new MySqlCommand("DELETE FROM user  WHERE username = '" + username.Text + "' ", koneksi);
                if (MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("User deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {

                }

                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
                DGVUS();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DGVUS();
            clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panelLogin.Width = 976;
            btnLogout.Visible = false;
        }
    }
}
