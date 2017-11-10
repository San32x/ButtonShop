﻿using System;
using System.Windows.Forms;
using PHPShop.Products;

namespace PHPShop
{
    public partial class Acceptance : Form
    {
        Product currentProduct = new Product();
        DatabaseMethods second = new DatabaseMethods();

        const String login = "San32";

        public Acceptance(PictureBox get)
        {
            InitializeComponent();
            currentProduct.ID = get.Name;
            currentProduct.Name = second.GetConnect("name", "products", "id", currentProduct.ID);
            currentProduct.Price = Convert.ToDecimal(second.GetConnect("price", "products", "id", currentProduct.ID));
            currentProduct.Image = get.Image;
            amount.Maximum = Convert.ToInt32(second.GetConnect("amount", "products", "id", currentProduct.ID));
        }

        private void Acceptance_Load(object sender, EventArgs e)
        {
            productName.Text = currentProduct.Name;
            priceLabel.Text = "По цене - " + currentProduct.Price + " крышек от бутылки";
            balanceLabel.Text = " У вас на счете " + second.GetConnect("balance", "users", "login", login) + " пробок";
            pictureBox1.Image = currentProduct.Image;
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            int productAmount = Convert.ToInt32(this.amount.Value);
            decimal balance = Convert.ToDecimal(second.GetConnect("balance", "users", "login", login));
            decimal ost;
            String result;

            decimal price = currentProduct.Price * productAmount;

            if (price <= balance)
            {
                ost = balance - price;
                second.SetConnect("balance", "users", "login", login, Convert.ToString(ost));
                balanceLabel.Text = " У вас на счете " + second.GetConnect("balance", "users", "login", login) + " пробок";
                result = "Ура! Вы приобрели товар: \"" + currentProduct.Name + "\" в количестве " + productAmount + " штук.\nВаш баланс " + ost + " пробок";
            }
            else
            {
                result = "Не так быстро, парниша! У тебя недостаточно пробок!";
            }

            Dialog a = new Dialog(result);
            a.Visible = true;
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AmountChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(amount.Value) != "")
            {
                priceLabel.Text = "По цене - " + Convert.ToString(currentProduct.Price * amount.Value + " крышек от бутылки");
            }
            else
            {
                priceLabel.Text = "По цене - " + currentProduct.Price + " крышек от бутылки";
            }
        }

        private void Acceptance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
