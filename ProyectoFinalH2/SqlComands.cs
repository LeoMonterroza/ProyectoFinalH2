﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Policy;
using System.Net.Sockets;

namespace ProyectoFinalH2
{
    internal class SqlComands
    {
        public void funConexion(string server, string database)
        {
            using (SqlConnection con = new SqlConnection($"server = {server}; database = {database}; integrated security = true;"))
            {
                con.Open();
                con.Close();
            }
        }

        public int funLoginTest(string server, string database, string tClientes, string entradaNombe, string entradaContra)
        {
            int Bandera = 0;
            using (SqlConnection con = new SqlConnection($"server = {server}; database = {database}; integrated security = true;"))
            {
                con.Open();
                string Query2 = $"select * from {tClientes} where Nombre = '{entradaNombe}'";
                SqlCommand Coman2 = new SqlCommand(Query2, con);
                SqlDataReader RC2 = Coman2.ExecuteReader();
                if (RC2.Read())
                {
                    RC2.Close();
                    string Query3 = $"select * from {tClientes} where Contraseña = '{entradaContra}' and Nombre = '{entradaNombe}'";
                    SqlCommand Coman3 = new SqlCommand(Query3, con);
                    SqlDataReader RC3 = Coman3.ExecuteReader();
                    if (RC3.Read())
                    {
                        RC3.Close();
                        Bandera = 1;
                    }
                    else {RC3.Close();}
                }
                else {RC2.Close();}
                con.Close();
                return Bandera;
            }
        }

        public void funSingUp(string server, string database, string tClientes, string entradaNombe, string entradaContra)
        {
            using (SqlConnection con = new SqlConnection($"server = {server}; database = {database}; integrated security = true;"))
            {
                con.Open();
                string Query2 = $"insert into {tClientes} values('{entradaNombe}', '{entradaContra}',0)";
                SqlCommand Coman4 = new SqlCommand(Query2,con);
                int Value = Coman4.ExecuteNonQuery();
                if (Value > 0) { MessageBox.Show("User successfully registered",$"Hello! {entradaNombe}."); }
                else { MessageBox.Show("Error while registering, try again.", "Ups!."); }
                con.Close();
            }
        }

        public void funTableCars(string server, string database, DataGridView table)
        {
            using (SqlConnection con = new SqlConnection($"server = {server}; database = {database}; integrated security = true;"))
            {
                con.Open();
                string QuerYCars = "select	* from tCars where Disponible = 1";
                SqlDataAdapter tCars = new SqlDataAdapter(QuerYCars,con);
                DataTable dt = new DataTable();
                tCars.Fill(dt);
                table.DataSource = dt;
                con.Close();
            }
        }

        public int funReturIDClient(string server, string database, string tClientes, string entradaNombe, string entradaContra)
        {
            int IDClient;
            using (SqlConnection con = new SqlConnection($"server = {server}; database = {database}; integrated security = true;"))
            {
                con.Open();
                string Query = $"select ID from {tClientes} where Contraseña = '{entradaContra}' and Nombre = '{entradaNombe}' ";
                SqlCommand Coman4 = new SqlCommand(Query, con);
                object Result = Coman4.ExecuteScalar();
                IDClient = Convert.ToInt32(Result);
                con.Close();
            }
            return IDClient;
        }

        public void funTArriReser(string server, string database, int IDCliente, string IDCarro, string Modo, DateTime FechaIn, DateTime FechaFin)
        {
            using (SqlConnection con = new SqlConnection($"server={server}; database={database}; integrated security=true;"))
            {
                con.Open();
                string query = "INSERT INTO tArriendoReservas (ID_Cliente, ID_Carro, Modo, FechaIn, FehcaFn) VALUES (@IDCliente, @IDCarro, @Modo, @FechaIn, @FechaFin)";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                sqlCommand.Parameters.AddWithValue("@IDCliente", IDCliente);
                sqlCommand.Parameters.AddWithValue("@IDCarro", IDCarro);
                sqlCommand.Parameters.AddWithValue("@Modo", Modo);
                sqlCommand.Parameters.AddWithValue("@FechaIn", FechaIn);
                sqlCommand.Parameters.AddWithValue("@FechaFin", FechaFin);

                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
        }

        public void funDispoCars(string server, string database, int value, string placaCar)
        {
            using (SqlConnection con = new SqlConnection($"server={server}; database={database}; integrated security=true;"))
            {
                con.Open();
                string Query = $"update tCars set Disponible = {value} where Placa = '{placaCar}'";
                SqlCommand sqlCarsDispo = new SqlCommand(Query, con);
                sqlCarsDispo.ExecuteNonQuery();
                con.Close();
            }
        }   
    }
}
