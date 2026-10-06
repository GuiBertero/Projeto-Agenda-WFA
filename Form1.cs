using System;
using System.Windows.Forms;
using Agenda.Models;

namespace Agenda
{
    public partial class Form1 : Form
    {
        private Contatos contatos;

        public Form1()
        {
            InitializeComponent();

            contatos = new Contatos();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = txtNome.Text;
                string email = txtEmail.Text;

                int dia = int.Parse(txtDia.Text);
                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);

                string tipo = txtTipo.Text;
                string numero = txtTelefone.Text;

                Data data = new Data(dia, mes, ano);

                Contato contato = new Contato(
                    email,
                    nome,
                    data
                );

                Telefone telefone = new Telefone(
                    tipo,
                    numero,
                    chkPrincipal.Checked
                );

                contato.adicionarTelefone(telefone);

                if (contatos.adicionar(contato))
                {
                    MessageBox.Show(
                        "Contato adicionado com sucesso!"
                    );

                    limparCampos();
                    listarContatos();
                }
                else
                {
                    MessageBox.Show(
                        "Já existe um contato com esse e-mail."
                    );
                }
            }
            catch
            {
                MessageBox.Show(
                    "Verifique os dados informados."
                );
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            Contato contato = new Contato();
            contato.setEmail(email);

            Contato encontrado = contatos.pesquisar(contato);

            if (encontrado != null)
            {
                txtNome.Text = encontrado.getNome();

                txtEmail.Text = encontrado.getEmail();

                txtDia.Text =
                    encontrado.getDtNasc().getDia().ToString();

                txtMes.Text =
                    encontrado.getDtNasc().getMes().ToString();

                txtAno.Text =
                    encontrado.getDtNasc().getAno().ToString();

                if (encontrado.getTelefones().Count > 0)
                {
                    Telefone telefone =
                        encontrado.getTelefones()[0];

                    txtTipo.Text = telefone.getTipo();
                    txtTelefone.Text = telefone.getNumero();
                    chkPrincipal.Checked =
                        telefone.getPrincipal();
                }

                MessageBox.Show(
                    "Contato encontrado!"
                );
            }
            else
            {
                MessageBox.Show(
                    "Contato não encontrado."
                );
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text;

                Contato contato = new Contato();
                contato.setEmail(email);

                Contato encontrado = contatos.pesquisar(contato);

                if (encontrado == null)
                {
                    MessageBox.Show(
                        "Contato não encontrado."
                    );

                    return;
                }

                string nome = txtNome.Text;

                int dia = int.Parse(txtDia.Text);
                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);

                Data data = new Data(dia, mes, ano);

                encontrado.setNome(nome);
                encontrado.setDtNasc(data);

                if (contatos.alterar(encontrado))
                {
                    MessageBox.Show(
                        "Contato alterado com sucesso!"
                    );

                    listarContatos();
                }
            }
            catch
            {
                MessageBox.Show(
                    "Verifique os dados informados."
                );
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            Contato contato = new Contato();
            contato.setEmail(email);

            if (contatos.remover(contato))
            {
                MessageBox.Show(
                    "Contato removido com sucesso!"
                );

                limparCampos();
                listarContatos();
            }
            else
            {
                MessageBox.Show(
                    "Contato não encontrado."
                );
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            listarContatos();
        }

        private void listarContatos()
        {
            lstContatos.Items.Clear();

            for (int i = 0; i < contatos.getAgenda().Count; i++)
            {
                lstContatos.Items.Add(
                    contatos.getAgenda()[i].ToString()
                );
            }
        }

        private void limparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtDia.Clear();
            txtMes.Clear();
            txtAno.Clear();
            txtTipo.Clear();
            txtTelefone.Clear();
            chkPrincipal.Checked = false;
        }
    }
}
