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

namespace UC11_PRODUTOS
{
    public partial class Form1 : Form
    {

        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;
        public Form1()
        {
            InitializeComponent();
            servidor = "Server=localhost;Database=bd_produtos;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            bool duplicado = false;

            try
            {
                conexao.Open();
                comando.CommandText = " SELECT descricao FROM tbl_produtos WHERE descricao = '" + textBoxDescricao.Text + "'";
                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                   duplicado = true;
                   MessageBox.Show("Registro Duplicado! Verifique os valores digitados");
                }          
            }
            catch (Exception erro_mysql)
            {
                
            }
            finally
            {
                conexao.Close();   
            }

            if (duplicado == false)
            {
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO tbl_produtos(Descricao, Categoria, Preco) VALUES ('" + textBoxDescricao.Text + "', '" + textBoxCategoria.Text + "'," + textBoxPreco.Text.Replace(",", ".") + ");";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com sucesso!");
                }
                catch (Exception erro_mysql)
                {
                    MessageBox.Show(erro_mysql.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void textBoxPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelCategoria_Click(object sender, EventArgs e)
        {

        }

        private void labelPreco_Click(object sender, EventArgs e)
        {

        }

        private void labelProduto_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxPreco.Text = "";
            textBoxCategoria.Text = "";
            textBoxDescricao.Text = "";
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            try
            {

                conexao.Open();
                comando.CommandText = "DELETE FROM tbl_produtos WHERE id = '" + textBoxID.Text + "';";
                comando.ExecuteNonQuery();
                MessageBox.Show("Produto excluido com sucesso!");


            }
            catch (Exception erro)
            {
                //MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao excluir produto. Tente Novamente");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT * FROM tbl_produtos;";

                MySqlDataAdapter adaptadorProdutos = new MySqlDataAdapter(comando);

                DataTable tabelaProdutos  = new DataTable();
                adaptadorProdutos.Fill(tabelaProdutos);
               
                dataGridViewProdutos.DataSource = tabelaProdutos;
                dataGridViewProdutos.Columns["id"].HeaderText = "Codigo";
                dataGridViewProdutos.Columns["descricao"].HeaderText = "Descrição";
                dataGridViewProdutos.Columns["categoria"].HeaderText = "Categoria";
                dataGridViewProdutos.Columns["preco"].HeaderText = "Preço";
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void dataGridViewProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridViewProdutos.CurrentRow.Cells[0].Value.ToString();
            textBoxDescricao.Text = dataGridViewProdutos.CurrentRow.Cells[1].Value.ToString();
            textBoxCategoria.Text = dataGridViewProdutos.CurrentRow.Cells[2].Value.ToString();
            textBoxPreco.Text = dataGridViewProdutos.CurrentRow.Cells[3].Value.ToString();
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "UPDATE tbl_produtos SET descricao = '" + textBoxDescricao.Text + "', categoria = '" + textBoxCategoria.Text + "', valor = " + textBoxPreco.Text.Replace(",", ".") + " WHERE id = " + textBoxID.Text + ";";
                comando.ExecuteNonQuery();
                MessageBox.Show("Resgistro alterado com sucesso!!");

            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
